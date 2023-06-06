// <copyright file="CustomerServiceCostLineItems.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.CustomerServiceCosts
{   
    using System;
    using System.Collections.Generic;
    using Models.ServiceCosts;

    /// <summary>
    /// Showcases customer service cost line items.
    /// </summary>
    internal class CustomerServiceCostLineItems : IUnitOfWork
    { 
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Customer Serivce Cost Line Items"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var selectedCustomerId = state[FeatureSamplesApplication.CustomerWithServiceCosts] as string;

            var serviceCostsLineItems = partnerOperations.Customers.ById(selectedCustomerId).ServiceCosts.ByBillingPeriod(ServiceCostsBillingPeriod.MostRecent).LineItems.Get();

            // Display service cost line items.
            foreach (var item in serviceCostsLineItems.Items)
            {
                Console.Out.WriteLine("Customer Id: {0} ", item.CustomerId);
                Console.Out.WriteLine("Customer Name: {0} ", item.CustomerName);
                Console.Out.WriteLine("Billing Start Date: {0} ", item.StartDate);
                Console.Out.WriteLine("Billing End Date: {0} ", item.EndDate);
                Console.Out.WriteLine("Pretax Total: {0} ", item.PretaxTotal);
                Console.Out.WriteLine("Tax: {0} ", item.Tax);
                Console.Out.WriteLine("After Tax Total: {0} ", item.AfterTaxTotal);
                Console.Out.WriteLine("Currency Code: {0} ", item.CurrencyCode);
                Console.Out.WriteLine("Currency Symbol: {0} ", item.CurrencySymbol);
                Console.Out.WriteLine("Charge Type: {0} ", item.ChargeType);
                Console.Out.WriteLine("Offer Id: {0} ", item.OfferId);
                Console.Out.WriteLine("Offer Name: {0} ", item.OfferName);
                Console.Out.WriteLine("Order Id: {0} ", item.OrderId);
                Console.Out.WriteLine("Quantity: {0} ", item.Quantity);
                Console.Out.WriteLine("Reseller MPN Id: {0} ", item.ResellerMPNId);
                Console.Out.WriteLine("Subscription Friendly Name: {0} ", item.SubscriptionFriendlyName);
                Console.Out.WriteLine("Subscription Id: {0} ", item.SubscriptionId);
                Console.Out.WriteLine("Unit Price: {0} ", item.UnitPrice);
                Console.Out.WriteLine("Invoice Number: {0}", item.InvoiceNumber);
                Console.Out.WriteLine("Product Id: {0}", item.ProductId);
                Console.Out.WriteLine("Sku Id: {0}", item.SkuId);
                Console.Out.WriteLine("Availability Id: {0}", item.AvailabilityId);
                Console.Out.WriteLine("Product Name: {0}", item.ProductName);
                Console.Out.WriteLine("Sku Name: {0}", item.SkuName);
                Console.Out.WriteLine("Publisher Name: {0}", item.PublisherName);
                Console.Out.WriteLine("Publisher Id: {0}", item.PublisherId);
                Console.Out.WriteLine("TermAndBillingCycle: {0}", item.TermAndBillingCycle);
                Console.Out.WriteLine("DiscountDetails: {0}", item.DiscountDetails);
                Console.Out.WriteLine();
            }
        }
    }
}
