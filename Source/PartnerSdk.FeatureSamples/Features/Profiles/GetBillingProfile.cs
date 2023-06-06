// -----------------------------------------------------------------------
// <copyright file="GetBillingProfile.cs" company="Microsoft Corporation">
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
    /// Showcases the partner billing profile.
    /// </summary>
    internal class GetBillingProfile : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Billing Profile"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            BillingProfile billingProfile = partnerOperations.Profiles.BillingProfile.Get();
            Console.Out.WriteLine("Company Name: " + billingProfile.CompanyName);
            Console.Out.WriteLine("Address Line 1: " + billingProfile.Address.AddressLine1);
            Console.Out.WriteLine("Address Line 2: " + billingProfile.Address.AddressLine2);
            Console.Out.WriteLine("City: " + billingProfile.Address.City);
            Console.Out.WriteLine("State: " + billingProfile.Address.State);
            Console.Out.WriteLine("Country: " + billingProfile.Address.Country);
            Console.Out.WriteLine("Postal Code: " + billingProfile.Address.PostalCode);
            Console.Out.WriteLine("Primary Contact First Name: " + billingProfile.PrimaryContact.FirstName);
            Console.Out.WriteLine("Primary Contact Last Name: " + billingProfile.PrimaryContact.LastName);
            Console.Out.WriteLine("Primary Contact Email: " + billingProfile.PrimaryContact.Email);
            Console.Out.WriteLine("Primary Contact Phone Number: " + billingProfile.PrimaryContact.PhoneNumber);
            Console.Out.WriteLine("PO number: " + billingProfile.PurchaseOrderNumber);
            Console.Out.WriteLine("Tax Id: " + billingProfile.TaxId);
            Console.Out.WriteLine("Billing Day: " + (billingProfile.BillingDay.HasValue ? billingProfile.BillingDay.Value.ToString() : string.Empty));
            Console.Out.WriteLine("Billing currency: " + billingProfile.BillingCurrency);
        }
    }
}
