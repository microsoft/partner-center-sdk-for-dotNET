// -----------------------------------------------------------------------
// <copyright file="ICustomerUsageSpendingBudget.cs" company="Microsoft Corporation">
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
    /// Defines the operations available on a customer's usage spending budget.
    /// </summary>
    public interface ICustomerUsageSpendingBudget : IPartnerComponent, IEntityGetOperations<SpendingBudget>, IEntityPatchOperations<SpendingBudget>
    {
        /// <summary>
        /// Retrieves the customer's usage spending budget.
        /// </summary>
        /// <returns>The customer's usage spending budget.</returns>
        new SpendingBudget Get();

        /// <summary>
        /// Asynchronously retrieves the customer's usage spending budget.
        /// </summary>
        /// <returns>The customer's usage spending budget.</returns>
        new Task<SpendingBudget> GetAsync();

        /// <summary>
        /// Patches the customer's usage spending budget.
        /// </summary>
        /// <param name="spendingBudget">The new customer's usage spending budget.</param>
        /// <returns>The updated customer's usage spending budget.</returns>
        new SpendingBudget Patch(SpendingBudget spendingBudget);

        /// <summary>
        /// Asynchronously patches the customer's usage spending budget.
        /// </summary>
        /// <param name="spendingBudget">The new customer's usage spending budget.</param>
        /// <returns>The updated customer's usage spending budget.</returns>
        new Task<SpendingBudget> PatchAsync(SpendingBudget spendingBudget);
    }
}