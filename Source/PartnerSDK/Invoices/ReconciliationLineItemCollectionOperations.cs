// -----------------------------------------------------------------------
// <copyright file="ReconciliationLineItemCollectionOperations.cs" company="Microsoft Corporation">
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
    using Microsoft.Store.PartnerCenter.Models.Query;
    using Models;
    using Models.Invoices;
    using Network;
    using Utilities;

    /// <summary>
    /// The operations available for the partner's reconciliation line item collection.
    /// </summary>
    internal class ReconciliationLineItemCollectionOperations : BasePartnerComponent, IReconciliationLineItemCollection
    {  
        /// <summary>
        /// The type of provider.
        /// </summary>
        private readonly string provider;

        /// <summary>
        /// The provider of invoice line item type information.
        /// </summary>
        private readonly string invoiceLineItemType;

        /// <summary>
        /// The type of currency code.
        /// </summary>
        private readonly string currencyCode;

        /// <summary>
        /// The provider of period information.
        /// </summary>
        private readonly string period;

        /// <summary>
        /// The page size.
        /// </summary>
        private readonly int pageSize;

        /// <summary>
        /// The maximum page size for reconciliation line items.
        /// </summary>
        private int pageMaxSizeReconciliationLineItems = 2000;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReconciliationLineItemCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The partner operations.</param>
        /// <param name="invoiceId">The invoice Id.</param>
        /// <param name="billingProvider">The billing provider type. example: All, Marketplace.</param>
        /// <param name="invoiceLineItemType">The invoice line item. example: BillingLineItems, UsageLineItems.</param>
        /// <param name="currencyCode">The currency code.</param>
        /// <param name="period">The period for unbilled reconciliation. example: current, previous.</param>
        /// <param name="size">The page size.</param>
        public ReconciliationLineItemCollectionOperations(IPartner rootPartnerOperations, string invoiceId, string billingProvider, string invoiceLineItemType, string currencyCode, string period, int? size)
            : base(rootPartnerOperations, invoiceId)
        {
            ParameterValidator.Required(invoiceId, "invoice Id has to be set.");
            ParameterValidator.Required(billingProvider, "billing provider has to be set.");
            ParameterValidator.Required(invoiceLineItemType, "invoice line item type has to be set.");

            this.provider = billingProvider;
            this.invoiceLineItemType = invoiceLineItemType;
            this.currencyCode = currencyCode;
            this.period = period;
            this.pageSize = size.HasValue ? size.Value : this.pageMaxSizeReconciliationLineItems;
        }

        /// <summary>
        ///  Retrieves reconciliation line items for a specific billing provider and recon line item type 
        /// </summary>
        /// <returns>The collection of reconciliation line items.</returns>
        public SeekBasedResourceCollection<InvoiceLineItem> Get()
        {
            return PartnerService.Instance.SynchronousExecute<SeekBasedResourceCollection<InvoiceLineItem>>(() => this.GetAsync());
        }

        /// <summary>
        ///  Asynchronously retrieves reconciliation line items collection of the partner
        /// </summary>
        /// <returns>The collection of reconciliation line items.</returns>
        public async Task<SeekBasedResourceCollection<InvoiceLineItem>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<InvoiceLineItem, SeekBasedResourceCollection<InvoiceLineItem>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetReconciliationLineItems.Path, this.Context));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
            PartnerService.Instance.Configuration.Apis.GetReconciliationLineItems.Parameters.Provider, this.provider));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
            PartnerService.Instance.Configuration.Apis.GetReconciliationLineItems.Parameters.InvoiceLineItemType, this.invoiceLineItemType));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
            PartnerService.Instance.Configuration.Apis.GetReconciliationLineItems.Parameters.CurrencyCode, this.currencyCode));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
            PartnerService.Instance.Configuration.Apis.GetReconciliationLineItems.Parameters.Period, this.period));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
            PartnerService.Instance.Configuration.Apis.GetReconciliationLineItems.Parameters.Size, this.pageSize.ToString()));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Seeks pages of reconciliation line items collection of the partner
        /// </summary>
        /// <param name="continuationToken">The continuation token from the previous results.</param>
        /// <param name="seekOperation">The seek operation to perform. Next is only supported.</param>
        /// <returns>The next page of reconciliation line items.</returns>
        public SeekBasedResourceCollection<InvoiceLineItem> Seek(string continuationToken, SeekOperation seekOperation)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.SeekAsync(continuationToken, seekOperation));
        }

        /// <summary>
        /// Asynchronously seeks pages of reconciliation line items collection of the partner
        /// </summary>
        /// <param name="continuationToken">The continuation token from the previous results.</param>
        /// <param name="seekOperation">The seek operation to perform. Next is only supported.</param>
        /// <returns>A task that returns the next page of reconciliation line items.</returns>
        public async Task<SeekBasedResourceCollection<InvoiceLineItem>> SeekAsync(string continuationToken, SeekOperation seekOperation = SeekOperation.Next)
        {
            if (string.IsNullOrWhiteSpace(continuationToken))
            {
                throw new ArgumentException("continuationToken must be non empty");
            }

            var partnerServiceProxy = new PartnerServiceProxy<InvoiceLineItem, SeekBasedResourceCollection<InvoiceLineItem>>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetReconciliationLineItems.Path, this.Context));

            partnerServiceProxy.AdditionalRequestHeaders.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetReconciliationLineItems.AdditionalHeaders.ContinuationToken,
                continuationToken));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
            PartnerService.Instance.Configuration.Apis.GetReconciliationLineItems.Parameters.Provider, this.provider));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
            PartnerService.Instance.Configuration.Apis.GetReconciliationLineItems.Parameters.InvoiceLineItemType, this.invoiceLineItemType));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
            PartnerService.Instance.Configuration.Apis.GetReconciliationLineItems.Parameters.CurrencyCode, this.currencyCode));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
            PartnerService.Instance.Configuration.Apis.GetReconciliationLineItems.Parameters.Period, this.period));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
            PartnerService.Instance.Configuration.Apis.GetReconciliationLineItems.Parameters.Size, this.pageSize.ToString()));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
            PartnerService.Instance.Configuration.Apis.GetReconciliationLineItems.Parameters.SeekOperation, seekOperation.ToString()));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
