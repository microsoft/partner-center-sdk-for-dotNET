// -----------------------------------------------------------------------
// <copyright file="OrderAttachmentOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Orders
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.Orders;
    using Microsoft.Store.PartnerCenter.Network;

    /// <summary>
    /// Implements operations that apply to order attachments.
    /// </summary>
    internal class OrderAttachmentOperations : BasePartnerComponent<Tuple<string, string, string>>, IOrderAttachment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderAttachmentOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        /// <param name="orderId">The order Id.</param>
        /// <param name="attachmentId">The attachment Id.</param>
        public OrderAttachmentOperations(IPartner rootPartnerOperations, string customerId, string orderId, string attachmentId)
            : base(rootPartnerOperations, new Tuple<string, string, string>(customerId, orderId, attachmentId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            if (string.IsNullOrWhiteSpace(orderId))
            {
                throw new ArgumentException("orderId must be set.");
            }

            if (string.IsNullOrWhiteSpace(attachmentId))
            {
                throw new ArgumentException("attachmentId must be set.");
            }
        }

        /// <summary>
        /// Retrieves the order attachment.
        /// </summary>
        /// <returns>The order attachment stream.</returns>
        public Stream Download()
        {
            return PartnerService.Instance.SynchronousExecute(() => this.DownloadAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the order attachment.
        /// </summary>
        /// <returns>The order attachment stream.</returns>
        public async Task<Stream> DownloadAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<string, OrderAttachment>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.DownloadOrderAttachment.Path, this.Context.Item1, this.Context.Item2, this.Context.Item3));

            return await partnerApiServiceProxy.GetFileContentAsync();
        }
    }
}