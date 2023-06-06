// -----------------------------------------------------------------------
// <copyright file="CustomerBillingProfileOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.Profiles
{
    using System;
    using System.Threading.Tasks;
    using Models.Customers;
    using Network;

    /// <summary>
    /// Implements the customer billing profile operations.
    /// </summary>
    internal class CustomerBillingProfileOperations : BasePartnerComponent, ICustomerProfile<CustomerBillingProfile>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerBillingProfileOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        public CustomerBillingProfileOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId can't be null");
            }
        }

        /// <summary>
        /// Gets the customer's billing profile.
        /// </summary>
        /// <returns>The customer's billing profile.</returns>
        public CustomerBillingProfile Get()
        {
            return PartnerService.Instance.SynchronousExecute<CustomerBillingProfile>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets the customer's billing profile.
        /// </summary>
        /// <returns>The customer's billing profile.</returns>
        public async Task<CustomerBillingProfile> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<CustomerBillingProfile, CustomerBillingProfile>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetCustomerBillingProfile.Path, this.Context));

            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Updates the customer's billing profile.
        /// </summary>
        /// <param name="billingProfile">A customer billing profile with updated fields.</param>
        /// <returns>The updated customer billing profile.</returns>
        public CustomerBillingProfile Update(CustomerBillingProfile billingProfile)
        {
            return PartnerService.Instance.SynchronousExecute<CustomerBillingProfile>(() => this.UpdateAsync(billingProfile));
        }

        /// <summary>
        /// Asynchronously updates the customer's billing profile.
        /// </summary>
        /// <param name="billingProfile">A customer billing profile with updated fields.</param>
        /// <returns>The updated customer billing profile.</returns>
        public async Task<CustomerBillingProfile> UpdateAsync(CustomerBillingProfile billingProfile)
        {
            if (billingProfile == null)
            {
                throw new ArgumentNullException("billingProfile");
            }

            var partnerServiceProxy = new PartnerServiceProxy<CustomerBillingProfile, CustomerBillingProfile>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.UpdateCustomerBillingProfile.Path, this.Context));

            return await partnerServiceProxy.PutAsync(billingProfile);
        }
    }
}
