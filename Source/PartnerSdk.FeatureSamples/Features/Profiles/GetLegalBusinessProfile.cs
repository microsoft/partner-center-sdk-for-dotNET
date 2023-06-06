// -----------------------------------------------------------------------
// <copyright file="GetLegalBusinessProfile.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Profiles
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Store.PartnerCenter.Exceptions;
    using Models.Partners;

    /// <summary>
    /// Showcases the legal business profile.
    /// </summary>
    internal class GetLegalBusinessProfile : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Legal Business Profile"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            LegalBusinessProfile legalBusinessProfile = partnerOperations.Profiles.LegalBusinessProfile.Get();
            Console.WriteLine("Current legal business profile: ");
            DisplayLegalBusinessProfile(legalBusinessProfile);

            try
            {
                legalBusinessProfile = partnerOperations.Profiles.LegalBusinessProfile.Get(VettingVersion.LastFinalized);
                Console.WriteLine("Last Finalized legal business profile: ");
                DisplayLegalBusinessProfile(legalBusinessProfile);
            }
            catch (PartnerException ex)
            {
                if (ex.ServiceErrorPayload.ErrorCode == "1000")
                {
                    Console.WriteLine("Last Finalized legal business profile was not found.");
                }
            }
        }

        /// <summary>
        /// Display the legal business profile.
        /// </summary>
        /// <param name="legalBusinessProfile">The legal business profile to display.</param>
        private static void DisplayLegalBusinessProfile(LegalBusinessProfile legalBusinessProfile)
        {
            Console.WriteLine("Company Name: " + legalBusinessProfile.CompanyName);
            Console.WriteLine("Address Line 1: " + legalBusinessProfile.Address.AddressLine1);
            Console.WriteLine("Address Line 2: " + legalBusinessProfile.Address.AddressLine2);
            Console.WriteLine("City: " + legalBusinessProfile.Address.City);
            Console.WriteLine("State: " + legalBusinessProfile.Address.State);
            Console.WriteLine("Postal Code: " + legalBusinessProfile.Address.PostalCode);
            Console.WriteLine("Primary Contact First Name: " + legalBusinessProfile.PrimaryContact.FirstName);
            Console.WriteLine("Primary Contact Last Name: " + legalBusinessProfile.PrimaryContact.LastName);
            Console.WriteLine("Primary Contact Email: " + legalBusinessProfile.PrimaryContact.Email);
            Console.WriteLine("Primary Contact Phone Number: " + legalBusinessProfile.PrimaryContact.PhoneNumber);
            Console.WriteLine("Company Approver Address Line 1: " + legalBusinessProfile.CompanyApproverAddress.AddressLine1);
            Console.WriteLine("Company Approver Address Line 2: " + legalBusinessProfile.CompanyApproverAddress.AddressLine2);
            Console.WriteLine("Company Approver City: " + legalBusinessProfile.CompanyApproverAddress.City);
            Console.WriteLine("Company Approver State: " + legalBusinessProfile.CompanyApproverAddress.State);
            Console.WriteLine("Company Approver Postal Code: " + legalBusinessProfile.CompanyApproverAddress.PostalCode);
            Console.WriteLine("Company Approver Email: " + legalBusinessProfile.CompanyApproverEmail);
            Console.WriteLine("Vetting status: " + legalBusinessProfile.VettingStatus);
            Console.WriteLine("Vetting sub status: " + legalBusinessProfile.VettingSubStatus + "\n");
        }
    }
}
