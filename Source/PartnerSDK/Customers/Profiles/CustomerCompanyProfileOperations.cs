// -----------------------------------------------------------------------
// <copyright file="CustomerCompanyProfileOperations.cs" company="Microsoft Corporation">
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
    /// Implements the customer company profile operations.
    /// </summary>
    internal class CustomerCompanyProfileOperations : BasePartnerComponent, ICustomerReadonlyProfile<CustomerCompanyProfile>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerCompanyProfileOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        public CustomerCompanyProfileOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId can't be null");
            }
        }

        /// <summary>
        /// Gets the customer's company profile.
        /// </summary>
        /// <returns>The customer's company profile.</returns>
        public CustomerCompanyProfile Get()
        {
            return PartnerService.Instance.SynchronousExecute<CustomerCompanyProfile>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets the customer's company profile.
        /// </summary>
        /// <returns>The customer's company profile.</returns>
        public async Task<CustomerCompanyProfile> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<CustomerCompanyProfile, CustomerCompanyProfile>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetCustomerCompanyProfile.Path, this.Context));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
