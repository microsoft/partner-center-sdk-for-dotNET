// -----------------------------------------------------------------------
// <copyright file="IServiceCostSummary.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.ServiceCosts
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.ServiceCosts;

    /// <summary>
    /// This interface defines the operations available on a customer's service cost summary.
    /// </summary>
    public interface IServiceCostSummary : IPartnerComponent<Tuple<string, string>>, IEntityGetOperations<ServiceCostsSummary>
    {
        /// <summary>
        /// Retrieves the customer's service cost summary.
        /// </summary>
        /// <returns>The customer's service cost summary.</returns>
        new ServiceCostsSummary Get();

        /// <summary>
        /// Asynchronously retrieves the service cost summary.
        /// </summary>
        /// <returns>The customer's service cost summary.</returns>
        new Task<ServiceCostsSummary> GetAsync();
    }
}
