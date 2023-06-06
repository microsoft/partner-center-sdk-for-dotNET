// -----------------------------------------------------------------------
// <copyright file="ICustomerServiceCostsCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.ServiceCosts
{
    using System;
    using Models.ServiceCosts;

    /// <summary>
    /// Holds customer service costs behavior.
    /// </summary>
    public interface ICustomerServiceCostsCollection : IPartnerComponent
    {
        /// <summary>
        /// Obtains the service cost operations by billing period.
        /// </summary>
        /// <param name="billingPeriod">The billing period.</param>
        /// <returns>The service cost operations.</returns>
        IServiceCostsCollection ByBillingPeriod(ServiceCostsBillingPeriod billingPeriod);
    }
}
