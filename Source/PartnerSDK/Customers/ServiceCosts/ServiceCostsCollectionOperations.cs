// -----------------------------------------------------------------------
// <copyright file="ServiceCostsCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.ServiceCosts
{
    using System;
    using Models.ServiceCosts;   

    /// <summary>
    ///  Holds the operations related to a customer's service costs.
    /// </summary>
    internal class ServiceCostsCollectionOperations : BasePartnerComponent<Tuple<string, string>>, IServiceCostsCollection
    {
        /// <summary>
        /// A lazy reference to the current customer's service cost summary operations.
        /// </summary>
        private Lazy<IServiceCostSummary> summary;

        /// <summary>
        /// A lazy reference to the current customer's service cost line items operations.
        /// </summary>
        private Lazy<IServiceCostLineItemsCollection> lineItems;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceCostsCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer id.</param>
        /// <param name="billingPeriod">The service cost billing period.</param>
        public ServiceCostsCollectionOperations(IPartner rootPartnerOperations, string customerId, ServiceCostsBillingPeriod billingPeriod)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, billingPeriod.ToString()))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            this.summary = new Lazy<IServiceCostSummary>(() => new ServiceCostSummaryOperations(this.Partner, this.Context));
            this.lineItems = new Lazy<IServiceCostLineItemsCollection>(() => new ServiceCostLineItemsOperations(this.Partner, this.Context));
        }

        /// <summary>
        /// Obtains the customer service cost summary behavior for the partner.
        /// </summary>
        public IServiceCostSummary Summary
        {
            get
            {
                return this.summary.Value;
            }
        }

        /// <summary>
        ///  Obtains the customer service cost line items behavior for the partner.
        /// </summary>
        public IServiceCostLineItemsCollection LineItems
        {
            get
            {
                return this.lineItems.Value;
            }
        }
    }
}
