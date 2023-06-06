// -----------------------------------------------------------------------
// <copyright file="GetOrganizationProfile.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Profiles
{
    using System;
    using System.Collections.Generic;
    using Models.Partners;

    /// <summary>
    /// Showcases the organization profile.
    /// </summary>
    internal class GetOrganizationProfile : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Organization Profile"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            OrganizationProfile organizationProfile = partnerOperations.Profiles.OrganizationProfile.Get();
            Console.Out.WriteLine("Company Name: " + organizationProfile.CompanyName);
            Console.Out.WriteLine("Address Line 1: " + organizationProfile.DefaultAddress.AddressLine1);
            Console.Out.WriteLine("Address Line 2: " + organizationProfile.DefaultAddress.AddressLine2);
            Console.Out.WriteLine("City: " + organizationProfile.DefaultAddress.City);
            Console.Out.WriteLine("State: " + organizationProfile.DefaultAddress.State);
            Console.Out.WriteLine("Postal Code: " + organizationProfile.DefaultAddress.PostalCode);
            Console.Out.WriteLine("Primary Contact Email: " + organizationProfile.Email);
            Console.Out.WriteLine("Primary Contact Phone Number: " + organizationProfile.DefaultAddress.PhoneNumber);
            Console.Out.WriteLine("Culture: " + organizationProfile.Culture);
            Console.Out.WriteLine("Language: " + organizationProfile.Language);
            Console.Out.WriteLine("Tenant ID: " + organizationProfile.TenantId);
            Console.Out.WriteLine("Domain " + organizationProfile.Domain);
        }
    }
}
