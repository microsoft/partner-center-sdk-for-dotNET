// -----------------------------------------------------------------------
// <copyright file="ServiceCostSummaryOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.ServiceCosts
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models.ServiceCosts;
    using Network;

    /// <summary>
    /// This class implements the operations for a customer's service costs summary.
    /// </summary>
    internal class ServiceCostSummaryOperations : BasePartnerComponent<Tuple<string, string>>, IServiceCostSummary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceCostSummaryOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="context">The context, including customer id and billing period.</param>
        public ServiceCostSummaryOperations(IPartner rootPartnerOperations, Tuple<string, string> context)
            : base(rootPartnerOperations, context)
        {
            if (context == null)
            {
                throw new ArgumentException("context must be set.");
            }
        }

        /// <summary>
        /// Retrieves the customer's service cost summary.
        /// </summary>
        /// <returns>The customer's service cost summary.</returns>
        public ServiceCostsSummary Get()
        {
            return PartnerService.Instance.SynchronousExecute<ServiceCostsSummary>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the service cost summary.
        /// </summary>
        /// <returns>The customer's service cost summary.</returns>
        public async Task<ServiceCostsSummary> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<ServiceCostsSummary, ServiceCostsSummary>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerServiceCostsSummary.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
