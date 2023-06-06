// -----------------------------------------------------------------------
// <copyright file="InvoiceLineItemCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.Invoices;
    using Network;
    using Utilities;

    /// <summary>
    /// The operations available for the partner's invoice line item collection.
    /// </summary>
    internal class InvoiceLineItemCollectionOperations : BasePartnerComponent, IInvoiceLineItemCollection
    {
        /// <summary>
        /// The provider of billing information.
        /// </summary>
        private readonly BillingProvider billingProvider;

        /// <summary>
        /// The type of invoice line item.
        /// </summary>
        private readonly InvoiceLineItemType invoiceLineItemType;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceLineItemCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The partner operations.</param>
        /// <param name="invoiceId">The invoice Id.</param>
        /// <param name="billingProvider">The provider of billing information,</param>
        /// <param name="invoiceLineItemType">The type of invoice line item.</param>
        public InvoiceLineItemCollectionOperations(IPartner rootPartnerOperations, string invoiceId, BillingProvider billingProvider, InvoiceLineItemType invoiceLineItemType)
            : base(rootPartnerOperations, invoiceId)
        {
            ParameterValidator.Required(invoiceId, "invoiceId has to be set.");
            
            if (billingProvider == BillingProvider.None)
            {
                throw new ArgumentException("The billing provider is not valid.");
            }

            if (invoiceLineItemType == InvoiceLineItemType.None)
            {
                throw new ArgumentException("The invoice line item type is not valid.");
            }

            this.billingProvider = billingProvider;
            this.invoiceLineItemType = invoiceLineItemType;
        }

        /// <summary>
        ///  Retrieves invoice line items for a specific billing provider and invoice line item type 
        /// </summary>
        /// <returns>The collection of invoice line items.</returns>
        public ResourceCollection<InvoiceLineItem> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<InvoiceLineItem>>(() => this.GetAsync());
        }

        /// <summary>
        ///  Asynchronously retrieves invoice line items for a specific billing provider and invoice line item type 
        /// </summary>
        /// <returns>The collection of invoice line items.</returns>
        public async Task<ResourceCollection<InvoiceLineItem>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<InvoiceLineItem, ResourceCollection<InvoiceLineItem>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetInvoiceLineItems.Path, this.Context, this.billingProvider, this.invoiceLineItemType));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        ///  Retrieves a subset of invoice line items for a specific billing provider and invoice line item type 
        /// </summary>
        /// <param name="size">The maximum number of invoice line items to return.</param>
        /// <param name="offset">The page offset.</param>
        /// <returns>The subset of invoice line items.</returns>
        public ResourceCollection<InvoiceLineItem> Get(int size, int offset)
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<InvoiceLineItem>>(() => this.GetAsync(size, offset));
        }

        /// <summary>
        ///  Asynchronously retrieves a subset of invoice line items for a specific billing provider and invoice line item type 
        /// </summary>
        /// <param name="size">The maximum number of invoice line items to return.</param>
        /// <param name="offset">The page offset.</param>
        /// <returns>The subset of invoice line items.</returns>
        public async Task<ResourceCollection<InvoiceLineItem>> GetAsync(int size, int offset)
        {
            var partnerServiceProxy = new PartnerServiceProxy<InvoiceLineItem, ResourceCollection<InvoiceLineItem>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetInvoiceLineItems.Path, this.Context, this.billingProvider, this.invoiceLineItemType));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
            PartnerService.Instance.Configuration.Apis.GetInvoiceLineItems.Parameters.Size, size.ToString()));
            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
            PartnerService.Instance.Configuration.Apis.GetInvoiceLineItems.Parameters.Offset, offset.ToString()));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
