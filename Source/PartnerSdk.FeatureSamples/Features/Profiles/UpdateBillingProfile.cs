// -----------------------------------------------------------------------
// <copyright file="UpdateBillingProfile.cs" company="Microsoft Corporation">
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
    /// Showcases Update Partner Billing Profile.
    /// </summary>
    internal class UpdateBillingProfile : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Update Billing Profile";
            }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            BillingProfile existingBillingProfile = partnerOperations.Profiles.BillingProfile.Get();
            Console.Out.WriteLine("========================== Existing Partner Billing Profile ==========================");
            DisplayProfile(existingBillingProfile);

            existingBillingProfile.PurchaseOrderNumber = new Random().Next(9000, 10000).ToString(CultureInfo.InvariantCulture);

            Console.Out.WriteLine("New purchase order number: " + existingBillingProfile.PurchaseOrderNumber);

            BillingProfile updatedPartnerBillingProfile = partnerOperations.Profiles.BillingProfile.Update(existingBillingProfile);
            Console.Out.WriteLine("========================== Updated Partner Billing Profile ==========================");
            DisplayProfile(updatedPartnerBillingProfile);
        }

        /// <summary>
        /// Display Billing Profile to console.
        /// </summary>
        /// <param name="billingProfile">The billing profile to be displayed.</param>
        private static void DisplayProfile(BillingProfile billingProfile)
        {
            Console.Out.WriteLine("Company Name: " + billingProfile.CompanyName);
            Console.Out.WriteLine("Address Line 1: " + billingProfile.Address.AddressLine1);
            Console.Out.WriteLine("Address Line 2: " + billingProfile.Address.AddressLine2);
            Console.Out.WriteLine("City: " + billingProfile.Address.City);
            Console.Out.WriteLine("State: " + billingProfile.Address.State);
            Console.Out.WriteLine("Country: " + billingProfile.Address.Country);
            Console.Out.WriteLine("Postal Code: " + billingProfile.Address.PostalCode);
            Console.Out.WriteLine("Primary Contact First Name: " + billingProfile.PrimaryContact.FirstName);
            Console.Out.WriteLine("Primary Contact Last Name: " + billingProfile.PrimaryContact.LastName);
            Console.Out.WriteLine("Primary Contact Phone Number: " + billingProfile.PrimaryContact.PhoneNumber);
            Console.Out.WriteLine("PO number: " + billingProfile.PurchaseOrderNumber);
            Console.Out.WriteLine("Tax Id: " + billingProfile.TaxId);
            Console.Out.WriteLine("Billing currency: " + billingProfile.BillingCurrency);
        }
    }
}
