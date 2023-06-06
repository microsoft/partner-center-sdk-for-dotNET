// <copyright file="CustomerCreation.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Customers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Models;
    using Models.Customers;

    /// <summary>
    /// Showcases customer creation.
    /// </summary>
    internal class CustomerCreation : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Customer Creation"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var newCustomer = partnerOperations.Customers.Create(new Customer()
            {
                CompanyProfile = new CustomerCompanyProfile()
                {
                    Domain = string.Format(CultureInfo.InvariantCulture, "SampleApplication{0}.ccsctp.net", new Random().Next())
                },
                BillingProfile = new CustomerBillingProfile()
                {
                    Culture = "EN-US",
                    Email = "SomeEmail@MS.com",
                    Language = "En",
                    CompanyName = "Some Company" + new Random().Next(),
                    DefaultAddress = new Address()
                    {
                        FirstName = "Engineer",
                        MiddleName = "Middle",
                        LastName = "In Test",
                        AddressLine1 = "4001 156th Ave",
                        City = "Redmond",
                        State = "WA",
                        Country = "US",
                        PostalCode = "98052",
                        PhoneNumber = "4257778899"
                    }
                }
            });

            Console.WriteLine("Created new Customer: ");
            Console.WriteLine("Domain: " + newCustomer.CompanyProfile.Domain);
            Console.WriteLine("Email: " + newCustomer.BillingProfile.Email);
            
            Console.WriteLine(
                string.Format(
                CultureInfo.InvariantCulture,
                "Address: {0}, {1}, {2}, {3}, {4}",
                newCustomer.BillingProfile.DefaultAddress.AddressLine1,
                newCustomer.BillingProfile.DefaultAddress.City,
                newCustomer.BillingProfile.DefaultAddress.State,
                newCustomer.BillingProfile.DefaultAddress.Country,
                newCustomer.BillingProfile.DefaultAddress.PostalCode));

            Console.WriteLine("Email: " + newCustomer.BillingProfile.Email);
            Console.WriteLine($"Phone: {newCustomer.BillingProfile.DefaultAddress.PhoneNumber}");
            Console.WriteLine($"Name: First: {newCustomer.BillingProfile.DefaultAddress.FirstName} Middle: {newCustomer.BillingProfile.DefaultAddress.MiddleName} Last: {newCustomer.BillingProfile.DefaultAddress.LastName}");

            // Retrieve default qualification
            var defaultCustomerQualification = partnerOperations.Customers.ById(newCustomer.Id).Qualification.GetQualifications();
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Customer default qualification:             {0}", defaultCustomerQualification.FirstOrDefault()?.Qualification.ToString() ?? "None."));

            // Get Customer Offer Categories / Offers
            var defaultCustomerOfferCategories = partnerOperations.Customers.ById(newCustomer.Id).OfferCategories.Get();
            var defaultCustomerOffers = partnerOperations.Customers.ById(newCustomer.Id).Offers.Get();

            // Set Customer qualification to Education
            var eduRequestBody = new Models.Customers.V2.CustomerQualificationRequest { Qualification = CustomerQualification.Education.ToString(), EducationSegment = "K12", Website = "example.edu" };
            var educationCustomerQualification = partnerOperations.Customers.ById(newCustomer.Id).Qualification.CreateQualifications(eduRequestBody);
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Customer qualification created for:         {0}", educationCustomerQualification.Qualification.ToString()));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Customer qualification creation status:     {0}", educationCustomerQualification.VettingStatus?.ToString() ?? "No status found."));

            // Retrieve set qualification
            var setCustomerQualification = partnerOperations.Customers.ById(newCustomer.Id).Qualification.GetQualifications();
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Education customer qualification:           {0}", setCustomerQualification.FirstOrDefault()?.Qualification.ToString() ?? "None."));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Education customer qualification status:    {0}", setCustomerQualification.FirstOrDefault()?.VettingStatus.ToString() ?? "No status found."));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Education customer qualification reason:    {0}", setCustomerQualification.FirstOrDefault()?.VettingReason.ToString() ?? "No reason found."));

            // Get Customer Offer Categories
            var educationCustomerOfferCategories = partnerOperations.Customers.ById(newCustomer.Id).OfferCategories.Get();
            var educationCustomerOffers = partnerOperations.Customers.ById(newCustomer.Id).Offers.Get();

            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Customer offer categories:           {0}", string.Join(", ", defaultCustomerOfferCategories.Items.OrderBy(x => x.Rank).Select(x => x.Name))));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Customer offers count:               {0}", string.Join(", ", defaultCustomerOffers.Items.Count())));

            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Education customer offer categories: {0}", string.Join(", ", educationCustomerOfferCategories.Items.OrderBy(x => x.Rank).Select(x => x.Name))));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Education customer offers count:     {0}", string.Join(", ", educationCustomerOffers.Items.Count())));
        }
    }
}
