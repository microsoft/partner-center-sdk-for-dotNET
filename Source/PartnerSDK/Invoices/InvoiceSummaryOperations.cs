// -----------------------------------------------------------------------
// <copyright file="InvoiceSummaryOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using System.Globalization;
    using System.Threading.Tasks;
    using Models.Invoices;
    using Network;

    /// <summary>
    /// Represents the operations that can be done on an invoice summary.
    /// </summary>
    internal class InvoiceSummaryOperations : BasePartnerComponent, IInvoiceSummary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceSummaryOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public InvoiceSummaryOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Retrieves summary of the partner's billing information.
        /// </summary>
        /// <returns>The invoice summary.</returns>
        public InvoiceSummary Get()
        {
            return PartnerService.Instance.SynchronousExecute<InvoiceSummary>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves summary of the partner's billing information.
        /// </summary>
        /// <returns>The invoice summary.</returns>
        public async Task<InvoiceSummary> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<InvoiceSummary, InvoiceSummary>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetInvoiceSummary.Path, this.Context));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
