// -----------------------------------------------------------------------
// <copyright file="DeletePartnerCustomerRelationship.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Relationships
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Customers;
    using Microsoft.Store.PartnerCenter.Models.Query;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Showcases Delete Partner Customer Relationship.
    /// </summary>    
    internal class DeletePartnerCustomerRelationship : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Delete Partner Customer Relationship"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // Create a Customer.
            var someCustomer = partnerOperations.Customers.Create(new Customer()
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
                    CompanyName = "Prime" + new Random().Next(),
                    DefaultAddress = new Address()
                    {
                        FirstName = "Engineer",
                        LastName = "In Test",
                        AddressLine1 = "4001 156th Ave",
                        City = "Redmond",
                        State = "WA",
                        Country = "US",
                        PostalCode = "98052",
                        PhoneNumber = "4253260000"
                    }
                },
                RelationshipToPartner = CustomerPartnerRelationship.Reseller
            });

            // Waiting for changes to propagate
            System.Threading.Thread.Sleep(2 * 60000);

            Customer customer = partnerOperations.Customers.ById(someCustomer.Id).Get();
            CustomerPartnerRelationship currentreleationshiptopartner = customer.RelationshipToPartner;

            // Verify No Subscription is in active state, Relationship cannot be deleted if subscriptions are active state.
            ResourceCollection<Subscription> customerSubscriptions = partnerOperations.Customers.ById(someCustomer.Id).Subscriptions.Get();
            IList<Subscription> subscriptions = new List<Subscription>(customerSubscriptions.Items);
            foreach (Subscription customerSubscription in subscriptions)
            {
                if (customerSubscription.Status == SubscriptionStatus.Active)
                {
                    customerSubscription.Status = SubscriptionStatus.Suspended;
                    var suspendedSubscription = partnerOperations.Customers.ById(someCustomer.Id).Subscriptions.ById(customerSubscription.Id).Patch(customerSubscription);
                    Console.Out.WriteLine("Subscription with ID :{0}  OfferName: {1} cannot be in active state, suspending it because no Subscription should be in active state before calling delete partner relationship. ", customerSubscription.Id, customerSubscription.OfferName);
                }
            }
            
            // Delete the partnership
            customer.RelationshipToPartner = CustomerPartnerRelationship.None;
            customer = partnerOperations.Customers.ById(customer.Id).Patch(customer);
            Console.Out.WriteLine("Customer.Patch Executed");

            // Partner customer relationship is removed and waiting for changes to propagate.
            System.Threading.Thread.Sleep(2 * 60000);
            try
            {
                var customersdelete = partnerOperations.Customers.Query(QueryFactory.Instance.BuildSimpleQuery(new SimpleFieldFilter(CustomerSearchField.CompanyName.ToString(), FieldFilterOperation.StartsWith, someCustomer.CompanyProfile.CompanyName)));
                Assert.IsTrue(customersdelete.TotalCount == 0);
                try
                {
                    Customer nullcustomer = partnerOperations.Customers.ById(someCustomer.Id).Get();
                    Console.Out.WriteLine("Customer found Test failed:");
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("Customer not found as expected :" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }           

            Console.Out.WriteLine("DeletePartnerCustomerRelationship Executed successfully");
        }
    }
}