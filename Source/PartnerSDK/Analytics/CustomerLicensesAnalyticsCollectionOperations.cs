// -----------------------------------------------------------------------
// <copyright file="CustomerLicensesAnalyticsCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------
namespace Microsoft.Store.PartnerCenter.Analytics
{
    using System;

    /// <summary>
    /// Implements the operations on a customer licenses analytics collection.
    /// </summary>
    internal class CustomerLicensesAnalyticsCollectionOperations : BasePartnerComponent, ICustomerLicensesAnalyticsCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerLicensesAnalyticsCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer id of the customer</param>
        public CustomerLicensesAnalyticsCollectionOperations(IPartner rootPartnerOperations, string customerId) 
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentNullException("customerId");
            }

            this.Deployment = new CustomerLicensesDeploymentInsightsCollectionOperations(rootPartnerOperations, customerId);
            this.Usage = new CustomerLicensesUsageInsightsCollectionOperations(rootPartnerOperations, customerId);
        }

        /// <summary>
        /// Gets the operations on a customer's licenses' deployment insights collection.
        /// </summary>
        public ICustomerLicensesDeploymentInsightsCollection Deployment { get; private set; }

        /// <summary>
        /// Gets the operations on a customer's licenses' usage insights collection.
        /// </summary>
        public ICustomerLicensesUsageInsightsCollection Usage { get; private set; }
    }
}
