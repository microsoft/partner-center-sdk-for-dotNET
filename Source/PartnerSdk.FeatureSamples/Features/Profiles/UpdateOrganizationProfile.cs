// -----------------------------------------------------------------------
// <copyright file="UpdateOrganizationProfile.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Models.Partners;

    /// <summary>
    /// Showcases Update Organization Profile.
    /// </summary>
    internal class UpdateOrganizationProfile : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Update Organization Profile";
            }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            OrganizationProfile organizationProfile = partnerOperations.Profiles.OrganizationProfile.Get();
            Console.Out.WriteLine("========================== Existing Organization Profile ==========================");
            DisplayProfile(organizationProfile);

            organizationProfile.DefaultAddress.PhoneNumber = ((long)(new Random().NextDouble() * 9000000000) + 1000000000).ToString(CultureInfo.InvariantCulture);

            Console.Out.WriteLine("New phone number: " + organizationProfile.DefaultAddress.PhoneNumber);

            OrganizationProfile updatedOrganizationProfile = partnerOperations.Profiles.OrganizationProfile.Update(organizationProfile);
            Console.Out.WriteLine("========================== Updated Organization Profile ==========================");
            DisplayProfile(updatedOrganizationProfile);
        }

        /// <summary>
        /// Display Organization Profile to console.
        /// </summary>
        /// <param name="profile">The organization profile to be displayed.</param>
        private static void DisplayProfile(OrganizationProfile profile)
        {
            Console.Out.WriteLine("Company Name: " + profile.CompanyName);
            Console.Out.WriteLine("Address Line 1: " + profile.DefaultAddress.AddressLine1);
            Console.Out.WriteLine("Address Line 2: " + profile.DefaultAddress.AddressLine2);
            Console.Out.WriteLine("City: " + profile.DefaultAddress.City);
            Console.Out.WriteLine("State: " + profile.DefaultAddress.State);
            Console.Out.WriteLine("Postal Code: " + profile.DefaultAddress.PostalCode);
            Console.Out.WriteLine("Primary Contact Email: " + profile.Email);
            Console.Out.WriteLine("Primary Contact Phone Number: " + profile.DefaultAddress.PhoneNumber);
            Console.Out.WriteLine("Culture: " + profile.Culture);
            Console.Out.WriteLine("Language: " + profile.Language);
            Console.Out.WriteLine("Tenant ID: " + profile.TenantId);
            Console.Out.WriteLine("Domain: " + profile.Domain);
        }
    }
}
