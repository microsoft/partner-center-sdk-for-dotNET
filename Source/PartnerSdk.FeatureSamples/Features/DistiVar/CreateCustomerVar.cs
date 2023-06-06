// -----------------------------------------------------------------------
// <copyright file="CreateCustomerVar.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.DistiVar
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Models;
    using Models.Customers;
    using Models.Query;
    using Models.Relationships;

    /// <summary>
    /// Test distributor creating a customer for a VAR
    /// </summary>
    internal class CreateCustomerVar : IUnitOfWork
    {
        /// <summary>
        /// Enables running the tests.
        /// </summary>
        private readonly bool testMultiTier;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCustomerVar"/> class.
        /// </summary>
        /// <param name="testMultiTier">true, runs the tests</param>
        public CreateCustomerVar(bool testMultiTier)
        {
            this.testMultiTier = testMultiTier;
        }

        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Distributor creates a customer for a VAR"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            if (!this.testMultiTier)
            {
                Console.WriteLine("Skipping tests.");
                return;
            }

            FieldFilter filter = new SimpleFieldFilter(PartnerRelationshipSearchField.Name.ToString(), FieldFilterOperation.Substring, "ChrisWantedATestOrg1");
            var valueAddedResellers = partnerOperations.Relationships.Query(PartnerRelationshipType.IsIndirectCloudSolutionProviderOf, QueryFactory.Instance.BuildSimpleQuery(filter));

            foreach (var reseller in valueAddedResellers.Items)
            {
                Console.WriteLine("Reseller Id: {0}", reseller.Id);
                Console.WriteLine("Reseller Name: {0}", reseller.Name);
                Console.WriteLine("Reseller MPN Id: {0}", reseller.MpnId);
                Console.WriteLine("Reseller Location: {0}", reseller.Location);
                Console.WriteLine("Relationship type: {0}", reseller.RelationshipType);
                Console.WriteLine();
            }

            var selectedVar = valueAddedResellers.Items.FirstOrDefault();

            if (selectedVar != null)
            {
                Console.WriteLine("Creating customer for Value Added Reseller : {0}", selectedVar.Name);
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();

                var newCustomer = new Customer()
                {
                    CompanyProfile = new CustomerCompanyProfile()
                    {
                        Domain = string.Format(CultureInfo.InvariantCulture, "customerforvar{0}.ccsctp.net", new Random().Next())
                    },
                    BillingProfile = new CustomerBillingProfile()
                    {
                        Culture = "EN-US",
                        Email = "SomeEmail@MS.com",
                        Language = "En",
                        CompanyName = "Customer for VAR" + new Random().Next(),
                        DefaultAddress = new Address()
                        {
                            FirstName = "Engineer",
                            LastName = "In Test",
                            AddressLine1 = "4001 156th Ave",
                            City = "Redmond",
                            State = "WA",
                            Country = "US",
                            PostalCode = "98052",
                            PhoneNumber = "4257778899"
                        }
                    },
                    AssociatedPartnerId = selectedVar.Id
                };

                var createdCustomer = partnerOperations.Customers.Create(newCustomer);

                Console.WriteLine("Get list of customers for indirect reseller : {0}", selectedVar.Name);
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();

                filter = new SimpleFieldFilter(CustomerSearchField.IndirectReseller.ToString(), FieldFilterOperation.StartsWith, selectedVar.Id);
                var customers = partnerOperations.Customers.Query(QueryFactory.Instance.BuildSimpleQuery(filter));

                foreach (var customer in customers.Items)
                {
                    Console.WriteLine("Id: {0}", customer.Id);
                    Console.WriteLine("Name: {0}", customer.CompanyProfile.CompanyName);
                    Console.WriteLine("Domain: {0}", customer.CompanyProfile.Domain);
                    Console.WriteLine();
                }
            }
        }
    }
}
