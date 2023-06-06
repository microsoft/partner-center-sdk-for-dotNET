// -----------------------------------------------------------------------
// <copyright file="CustomerUsageSummaryOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Usage
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models.Usage;
    using Network;    

    /// <summary>
    /// This class implements the operations for a customer's summary of usage-based subscriptions.
    /// </summary>
    internal class CustomerUsageSummaryOperations : BasePartnerComponent, ICustomerUsageSummary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerUsageSummaryOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        public CustomerUsageSummaryOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }
        }

        /// <summary>
        /// Gets the customer usage summary.
        /// </summary>
        /// <returns>The customer usage summary.</returns>
        public CustomerUsageSummary Get()
        {
            return PartnerService.Instance.SynchronousExecute<CustomerUsageSummary>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets the customer usage summary.
        /// </summary>
        /// <returns>The customer usage summary.</returns>
        public async Task<CustomerUsageSummary> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<CustomerUsageSummary, CustomerUsageSummary>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerUsageSummary.Path, this.Context));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
