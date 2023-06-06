// -----------------------------------------------------------------------
// <copyright file="OrderAttachmentCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Orders
{
    using System;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Orders;
    using Microsoft.Store.PartnerCenter.Network;
    using Microsoft.Store.PartnerCenter.Utilities;

    /// <summary>
    /// Order attachment collection operations implementation class.
    /// </summary>
    internal class OrderAttachmentCollectionOperations : BasePartnerComponent<Tuple<string, string>>, IOrderAttachmentCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderAttachmentCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        /// <param name="orderId">The order Id.</param>
        public OrderAttachmentCollectionOperations(IPartner rootPartnerOperations, string customerId, string orderId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, orderId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            if (string.IsNullOrWhiteSpace(orderId))
            {
                throw new ArgumentException("orderId must be set.");
            }
        }

        /// <summary>
        /// Obtains a specific order attachment behavior.
        /// </summary>
        /// <param name="attachmentId">The order attachment id.</param>
        /// <returns>The order attachment operations.</returns>
        public IOrderAttachment this[string attachmentId]
        {
            get
            {
                return this.ById(attachmentId);
            }
        }

        /// <summary>
        /// Obtains a specific order attachment behavior.
        /// </summary>
        /// <param name="attachmentId">The attachment id.</param>
        /// <returns>The order attachment operations.</returns>
        public IOrderAttachment ById(string attachmentId)
        {
            return new OrderAttachmentOperations(this.Partner, this.Context.Item1, this.Context.Item2, attachmentId);
        }

        /// <summary>
        /// Uploads and submits purchase order evidence for an order for the customer.
        /// </summary>
        /// <param name="attachmentDetails">The attachments and the metadata.</param>
        /// <returns>The attachment collection.</returns>
        public ResourceCollection<OrderAttachment> Upload(MultipartFormDataContent attachmentDetails)
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<OrderAttachment>>(() => this.UploadAsync(attachmentDetails));
        }

        /// <summary>
        /// Asynchronously uploads and submits purchase order evidence for an order for the customer.
        /// </summary>
        /// <param name="attachmentDetails">The new order.</param>
        /// <returns>The attachment collection.</returns>
        public async Task<ResourceCollection<OrderAttachment>> UploadAsync(MultipartFormDataContent attachmentDetails)
        {
            ParameterValidator.Required(attachmentDetails, "newOrder can't be null");

            var partnerApiServiceProxy = new PartnerServiceProxy<MultipartFormDataContent, ResourceCollection<OrderAttachment>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.UploadOrderAttachments.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.PostFormDataAsync(attachmentDetails);
        }

        /// <summary>
        /// Retrieves all the order attachments.
        /// </summary>
        /// <returns>All the customer order attachments.</returns>
        public ResourceCollection<OrderAttachment> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<OrderAttachment>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves all the orders attachments.
        /// </summary>
        /// <returns>All the customer order attachments.</returns>
        public async Task<ResourceCollection<OrderAttachment>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<ResourceCollection<OrderAttachment>, ResourceCollection<OrderAttachment>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetOrderAttachments.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
