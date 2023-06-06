// -----------------------------------------------------------------------
// <copyright file="CustomerServiceCostsCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.ServiceCosts
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Models.ServiceCosts;

    /// <summary>
    /// Holds customer service costs behavior.
    /// </summary>
    internal class CustomerServiceCostsCollectionOperations : BasePartnerComponent, ICustomerServiceCostsCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerServiceCostsCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer id.</param>
        public CustomerServiceCostsCollectionOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }
        }

        /// <summary>
        /// Obtains the service cost operations by billing period.
        /// </summary>
        /// <param name="billingPeriod">The billing period.</param>
        /// <returns>The service cost operations.</returns>
        public IServiceCostsCollection ByBillingPeriod(ServiceCostsBillingPeriod billingPeriod)
        {
            return new ServiceCostsCollectionOperations(this.Partner, this.Context, billingPeriod);
        }
    }
}
