// -----------------------------------------------------------------------
// <copyright file="UpdateLegalBusinessProfile.cs" company="Microsoft Corporation">
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
    /// Showcases Update LegalBusiness Profile.
    /// </summary>
    internal class UpdateLegalBusinessProfile : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Update Legal Business Profile";
            }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            LegalBusinessProfile legalBusinessProfile = partnerOperations.Profiles.LegalBusinessProfile.Get();
            Console.Out.WriteLine("========================== Existing Legal Business Profile ==========================");
            DisplayProfile(legalBusinessProfile);

            legalBusinessProfile.CompanyApproverAddress.PhoneNumber = ((long)(new Random().NextDouble() * 9000000000) + 1000000000).ToString(CultureInfo.InvariantCulture);

            Console.Out.WriteLine("New phone number: " + legalBusinessProfile.CompanyApproverAddress.PhoneNumber);

            LegalBusinessProfile updatedLegalBusinessProfile = partnerOperations.Profiles.LegalBusinessProfile.Update(legalBusinessProfile);
            Console.Out.WriteLine("========================== Updated Legal Business Profile ==========================");
            DisplayProfile(updatedLegalBusinessProfile);
        }

        /// <summary>
        /// Display Legal Business Profile to console.
        /// </summary>
        /// <param name="profile">The legal business profile to be displayed.</param>
        private static void DisplayProfile(LegalBusinessProfile profile)
        {
            Console.Out.WriteLine("Company Name: " + profile.CompanyName);
            Console.Out.WriteLine("Address Line 1: " + profile.Address.AddressLine1);
            Console.Out.WriteLine("Address Line 2: " + profile.Address.AddressLine2);
            Console.Out.WriteLine("City: " + profile.Address.City);
            Console.Out.WriteLine("State: " + profile.Address.State);
            Console.Out.WriteLine("Postal Code: " + profile.Address.PostalCode);
            Console.Out.WriteLine("Primary Contact First Name: " + profile.PrimaryContact.FirstName);
            Console.Out.WriteLine("Primary Contact Last Name: " + profile.PrimaryContact.LastName);
            Console.Out.WriteLine("Primary Contact Email: " + profile.PrimaryContact.Email);
            Console.Out.WriteLine("Primary Contact Phone Number: " + profile.PrimaryContact.PhoneNumber);
            Console.Out.WriteLine("Company Approver Address Line 1: " + profile.CompanyApproverAddress.AddressLine1);
            Console.Out.WriteLine("Company Approver Address Line 2: " + profile.CompanyApproverAddress.AddressLine2);
            Console.Out.WriteLine("Company Approver City: " + profile.CompanyApproverAddress.City);
            Console.Out.WriteLine("Company Approver State: " + profile.CompanyApproverAddress.State);
            Console.Out.WriteLine("Company Approver Postal Code: " + profile.CompanyApproverAddress.PostalCode);
            Console.Out.WriteLine("Company Approver Email: " + profile.CompanyApproverEmail);
            Console.Out.WriteLine("Vetting status: " + profile.VettingStatus);
            Console.Out.WriteLine("Vetting sub status: " + profile.VettingSubStatus);
        }
    }
}
