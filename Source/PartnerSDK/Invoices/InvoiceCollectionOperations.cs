// -----------------------------------------------------------------------
// <copyright file="InvoiceCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Models.Invoices;
    using Models.JsonConverters;
    using Models.Query;
    using Network;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>
    /// Represents the operations that can be done on Partner's invoices.
    /// </summary>
    internal class InvoiceCollectionOperations : BasePartnerComponent, IInvoiceCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public InvoiceCollectionOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }
    
        /// <summary>
        /// Gets a single invoice summary operations.
        /// </summary>
        /// <returns>The invoice summary operations.</returns>
        public IInvoiceSummary Summary
        {
            get { return new InvoiceSummaryOperations(this.Partner); }
        }

        /// <summary>
        /// Gets invoice summaries operations.
        /// </summary>
        /// <returns>The invoice summary operations.</returns>
        public IInvoiceSummaryCollection Summaries
        {
            get { return new InvoiceSummaryCollectionOperations(this.Partner); }
        }

        /// <summary>
        /// Gets estimates operations.
        /// </summary>
        /// <returns>The estimates operations.</returns>
        public IEstimateCollection Estimates
        {
            get { return new EstimateCollectionOperations(this.Partner); }
        }

        /// <summary>
        /// Gets a single invoice operations.
        /// </summary>
        /// <param name="invoiceId">The invoice id.</param>
        /// <returns>The invoice operations.</returns>
        public IInvoice this[string invoiceId]
        {
            get
            {
                return this.ById(invoiceId);
            }
        }

        /// <summary>
        /// Gets a single invoice operations.
        /// </summary>
        /// <param name="invoiceId">The invoice id.</param>
        /// <returns>The invoice operations.</returns>
        public IInvoice ById(string invoiceId)
        {
            ParameterValidator.Required(invoiceId, "invoiceId has to be set.");
            return new InvoiceOperations(this.Partner, invoiceId);
        }

        /// <summary>
        /// Retrieves all invoices associated to the partner.
        /// </summary>
        /// <returns>The collection of invoices.</returns>
        public ResourceCollection<Invoice> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Invoice>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves all invoices associated to the partner.
        /// </summary>
        /// <returns>The collection of invoices.</returns>
        public async Task<ResourceCollection<Invoice>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Invoice, ResourceCollection<Invoice>>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetInvoices.Path);

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Retrieves all invoices associated to the partner.
        /// </summary>
        /// <param name="query">The query parameter.</param>
        /// <returns>The subset of invoices.</returns>
        public ResourceCollection<Invoice> Query(IQuery query)
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Invoice>>(() => this.QueryAsync(query));
        }

        /// <summary>
        /// Asynchronously retrieves all invoices associated to the partner.
        /// </summary>
        /// <param name="query">The query parameter.</param>
        /// <returns>The subset of invoices.</returns>
        public async Task<ResourceCollection<Invoice>> QueryAsync(IQuery query)
        {
            ParameterValidator.Required(query, "Query paramter must not be null");

            if (query.Type != QueryType.Indexed && query.Type != QueryType.Simple)
            {
                throw new ArgumentException("This type of query is not supported.");
            }

            var partnerServiceProxy = new PartnerServiceProxy<Invoice, ResourceCollection<Invoice>>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetInvoices.Path,
                jsonConverter: new ResourceCollectionConverter<Invoice>());

            if (query.Type == QueryType.Indexed)
            {
                partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetInvoices.Parameters.Size, query.PageSize.ToString()));

                partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetInvoices.Parameters.Offset, query.Index.ToString()));
            }

            if (query.Filter != null)
            {
                partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetInvoices.Parameters.Filter, JsonConvert.SerializeObject(query.Filter)));
            }
            
            return await partnerServiceProxy.GetAsync();
        }
    }
}
