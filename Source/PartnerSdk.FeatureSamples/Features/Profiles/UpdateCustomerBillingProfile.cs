// -----------------------------------------------------------------------
// <copyright file="UpdateCustomerBillingProfile.cs" company="Microsoft Corporation">
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
    /// Showcases updating a customer's billing profile.
    /// </summary>
    internal class UpdateCustomerBillingProfile : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Update Customer Billing Profile"; }
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

            var billingProfile = partnerOperations.Customers.ById(selectedCustomerId).Profiles.Billing.Get();

            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "First name: {0}", billingProfile.FirstName));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Last name: {0}", billingProfile.LastName));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Company name: {0}", billingProfile.CompanyName));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Culture: {0}", billingProfile.Culture));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Email: {0}", billingProfile.Email));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Id: {0}", billingProfile.Id));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Language: {0}", billingProfile.Language));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Phone Number: {0}", billingProfile.DefaultAddress.PhoneNumber));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Country: {0}", billingProfile.DefaultAddress.Country));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "City: {0}", billingProfile.DefaultAddress.City));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Address Line: {0}", billingProfile.DefaultAddress.AddressLine1));

            Console.WriteLine("Updating customer billing profile...");
            Console.WriteLine();

            billingProfile.FirstName = billingProfile.FirstName + "A";
            billingProfile.LastName = billingProfile.LastName + "A";
            billingProfile.CompanyName = billingProfile.CompanyName + "A";

            billingProfile = partnerOperations.Customers.ById(selectedCustomerId).Profiles.Billing.Update(billingProfile);

            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "First name: {0}", billingProfile.FirstName));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Last name: {0}", billingProfile.LastName));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Company name: {0}", billingProfile.CompanyName));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Culture: {0}", billingProfile.Culture));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Email: {0}", billingProfile.Email));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Id: {0}", billingProfile.Id));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Language: {0}", billingProfile.Language));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Phone Number: {0}", billingProfile.DefaultAddress.PhoneNumber));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Country: {0}", billingProfile.DefaultAddress.Country));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "City: {0}", billingProfile.DefaultAddress.City));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Address Line: {0}", billingProfile.DefaultAddress.AddressLine1));
        }
    }
}
