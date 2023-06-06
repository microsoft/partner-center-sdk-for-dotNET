// -----------------------------------------------------------------------
// <copyright file="CustomerProfileCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.Profiles
{
    using System;
    using Models.Customers;

    /// <summary>
    /// Implements customer profile collection operations.
    /// </summary>
    internal class CustomerProfileCollectionOperations : BasePartnerComponent, ICustomerProfileCollection
    {
        /// <summary>
        /// A lazy reference to a customer billing operations instance.
        /// </summary>
        private readonly Lazy<ICustomerProfile<CustomerBillingProfile>> billingProfileOperations;

        /// <summary>
        /// A lazy reference to a customer company operations instance.
        /// </summary>
        private readonly Lazy<ICustomerReadonlyProfile<CustomerCompanyProfile>> companyProfileOperations;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerProfileCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant Id.</param>
        public CustomerProfileCollectionOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId can't be null");
            }

            this.billingProfileOperations =
                new Lazy<ICustomerProfile<CustomerBillingProfile>>(() => new CustomerBillingProfileOperations(this.Partner, this.Context));
            this.companyProfileOperations =
                new Lazy<ICustomerReadonlyProfile<CustomerCompanyProfile>>(() => new CustomerCompanyProfileOperations(this.Partner, this.Context));
        }

        /// <summary>
        /// Gets the customer's billing profile operations.
        /// </summary>
        public ICustomerProfile<CustomerBillingProfile> Billing
        {
            get
            {
                return this.billingProfileOperations.Value;
            }
        }

        /// <summary>
        /// Gets the customer's company profile operations.
        /// </summary>
        public ICustomerReadonlyProfile<CustomerCompanyProfile> Company
        {
            get
            {
                return this.companyProfileOperations.Value;
            }
        }
    }
}
