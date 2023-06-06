// -----------------------------------------------------------------------
// <copyright file="GetCustomerCompanyProfile.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Profiles
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Models.Customers;

    /// <summary>
    /// Show cases getting a customer's company profile.
    /// </summary>
    internal class GetCustomerCompanyProfile : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Customer Company Profile"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the customer from the application state
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerKey] as string;

            var companyProfile = partnerOperations.Customers.ById(selectedCustomerId).Profiles.Company.Get();

            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Company Name: {0}", companyProfile.CompanyName));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Domain: {0}", companyProfile.Domain));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Tenant Id: {0}", companyProfile.TenantId));
        }
    }
}
