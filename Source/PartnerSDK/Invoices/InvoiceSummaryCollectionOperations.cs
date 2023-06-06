// -----------------------------------------------------------------------
// <copyright file="InvoiceSummaryCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models;
    using Models.Invoices;
    using Network;

    /// <summary>
    /// Represents the operations that can be done on invoice summary collection.
    /// </summary>
    internal class InvoiceSummaryCollectionOperations : BasePartnerComponent, IInvoiceSummaryCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceSummaryCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public InvoiceSummaryCollectionOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Retrieves invoice summary collection of the partner's billing information.
        /// </summary>
        /// <returns>The invoice summary collection.</returns>
        public ResourceCollection<InvoiceSummary> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<InvoiceSummary>>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves invoice summary collection of the partner's billing information.
        /// </summary>
        /// <returns>The invoice summary collection.</returns>
        public async Task<ResourceCollection<InvoiceSummary>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<ResourceCollection<InvoiceSummary>, ResourceCollection<InvoiceSummary>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetInvoiceSummaries.Path, this.Context));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
