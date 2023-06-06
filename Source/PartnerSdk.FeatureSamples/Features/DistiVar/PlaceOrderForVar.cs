// -----------------------------------------------------------------------
// <copyright file="PlaceOrderForVar.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.DistiVar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Customers;
    using Models.Orders;
    using Models.Query;

    /// <summary>
    /// Test distributor creating a customer for a VAR
    /// </summary>
    internal class PlaceOrderForVar : IUnitOfWork
    {
        /// <summary>
        /// Enables running the tests.
        /// </summary>
        private readonly bool testMultiTier;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceOrderForVar"/> class.
        /// </summary>
        /// <param name="testMultiTier">true, runs the tests</param>
        public PlaceOrderForVar(bool testMultiTier)
        {
            this.testMultiTier = testMultiTier;
        }

        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Distributor places an order for a customer of a VAR"; }
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

            Console.WriteLine("Get list of VARs");

            FieldFilter filter = new SimpleFieldFilter(CustomerSearchField.CompanyName.ToString(), FieldFilterOperation.StartsWith, "VAR");
            var customers = partnerOperations.Customers.Query(QueryFactory.Instance.BuildSimpleQuery(filter));
            var selectedCustomer = customers.Items.FirstOrDefault();

            if (selectedCustomer != null)
            {
                Console.WriteLine("Id: {0}", selectedCustomer.Id);
                Console.WriteLine("Name: {0}", selectedCustomer.CompanyProfile.CompanyName);
                Console.WriteLine();

                Console.WriteLine("Get list of VARs for a customer : {0}", selectedCustomer.Id);
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();

                var valueAddedResellers = partnerOperations.Customers.ById(selectedCustomer.Id).Relationships.Get();

                foreach (var reseller in valueAddedResellers.Items)
                {
                    Console.WriteLine("Reseller Id: {0}", reseller.Id);
                    Console.WriteLine("Reseller Name: {0}", reseller.Name);
                    Console.WriteLine("Reseller MPN Id: {0}", reseller.MpnId);
                    Console.WriteLine("Relationship type: {0}", reseller.RelationshipType);
                    Console.WriteLine();
                }

                var selectedVar = valueAddedResellers.Items.FirstOrDefault();

                if (selectedVar != null)
                {
                    Console.WriteLine("Create order for customer: {0} with Value Added Reseller : {1}", selectedCustomer.CompanyProfile.CompanyName, selectedVar.Name);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadLine();

                    var offers = partnerOperations.Offers.ByCountry("US").Get();
                    var selectedOffer = offers.Items.FirstOrDefault(offer => offer.Name.Contains("Office 365 Enterprise E1"));

                    var order = new Order()
                    {
                        ReferenceCustomerId = selectedCustomer.Id,
                        LineItems = new List<OrderLineItem>()
                        {
                            new OrderLineItem()
                            {
                                OfferId = "91FD106F-4B2C-4938-95AC-F54F74E9A239",
                                FriendlyName = selectedOffer.Name + "My offer purchase",
                                Quantity = selectedOffer.MinimumQuantity + 1,
                                PartnerIdOnRecord = selectedVar.MpnId,
                                AdditionalPartnerIdsOnRecord = new List<string>() { "5462014", "6362907" },
                            }
                        }
                    };

                    var newOrder = order = partnerOperations.Customers.ById(selectedCustomer.Id).Orders.Create(order);

                    Console.Out.WriteLine("Id: {0}", order.Id);
                    Console.Out.WriteLine("Creation Date: {0}", order.CreationDate);
                    Console.Out.WriteLine("PartnerOnRecord: {0}", order.LineItems.FirstOrDefault().PartnerIdOnRecord);
                    Console.Out.WriteLine("AdditionalPartnerOnRecords: {0}", order.LineItems.FirstOrDefault().AdditionalPartnerIdsOnRecord != null ? string.Join(",", order.LineItems.FirstOrDefault().AdditionalPartnerIdsOnRecord) : "N/A");
                    Console.Out.WriteLine();
                }
            }
        }
    }
}
