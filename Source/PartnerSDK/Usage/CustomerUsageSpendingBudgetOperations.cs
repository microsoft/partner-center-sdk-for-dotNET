// -----------------------------------------------------------------------
// <copyright file="CustomerUsageSpendingBudgetOperations.cs" company="Microsoft Corporation">
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
    using Utilities;

    /// <summary>
    /// This class implements the operations available on a customer's usage spending budget.
    /// </summary>
    internal class CustomerUsageSpendingBudgetOperations : BasePartnerComponent, ICustomerUsageSpendingBudget
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerUsageSpendingBudgetOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        public CustomerUsageSpendingBudgetOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }
        }

        /// <summary>
        /// Gets the usage spending budget allocated to a customer by the partner.
        /// </summary>
        /// <returns>The customer usage spending budget.</returns>
        public SpendingBudget Get()
        {
            return PartnerService.Instance.SynchronousExecute<SpendingBudget>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets the usage spending budget allocated to a customer by the partner.
        /// </summary>
        /// <returns>The customer usage spending budget.</returns>
        public async Task<SpendingBudget> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<SpendingBudget, SpendingBudget>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetCustomerUsageSpendingBudget.Path, this.Context));

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <summary>
        /// Updates the usage spending budget allocated to a customer by the partner.
        /// </summary>
        /// <param name="usageSpendingBudget">The new customer usage spending budget.</param>
        /// <returns>The updated customer usage spending budget.</returns>
        public SpendingBudget Patch(SpendingBudget usageSpendingBudget)
        {
            return PartnerService.Instance.SynchronousExecute<SpendingBudget>(() => this.PatchAsync(usageSpendingBudget));
        }

        /// <summary>
        /// Asynchronously updates the usage spending budget allocated to a customer by the partner.
        /// </summary>
        /// <param name="usageSpendingBudget">The new customer usage spending budget.</param>
        /// <returns>The updated customer usage spending budget.</returns>
        public async Task<SpendingBudget> PatchAsync(SpendingBudget usageSpendingBudget)
        {
            ParameterValidator.Required(usageSpendingBudget, "usage spending budget is required.");

            var partnerApiServiceProxy = new PartnerServiceProxy<SpendingBudget, SpendingBudget>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.PatchCustomerUsageSpendingBudget.Path, this.Context));

            return await partnerApiServiceProxy.PatchAsync(usageSpendingBudget);
        }
    }
}