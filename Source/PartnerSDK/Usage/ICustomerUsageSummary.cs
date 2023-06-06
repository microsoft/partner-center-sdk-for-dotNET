// -----------------------------------------------------------------------
// <copyright file="ICustomerUsageSummary.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Usage
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.Usage;

    /// <summary>
    /// Defines the operations available on a customer's usage summary.
    /// </summary>
    public interface ICustomerUsageSummary : IPartnerComponent, IEntityGetOperations<CustomerUsageSummary>
    {
        /// <summary>
        /// Retrieves the customer usage summary.
        /// </summary>
        /// <returns>The customer usage summary.</returns>
        new CustomerUsageSummary Get();

        /// <summary>
        /// Asynchronously retrieves the customer usage summary.
        /// </summary>
        /// <returns>The customer usage summary.</returns>
        new Task<CustomerUsageSummary> GetAsync();
    }
}
