// -----------------------------------------------------------------------
// <copyright file="CreateOrderWithCustomerUserUpn.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Store.PartnerCenter.Models.Products;
    using Models.Orders;
    using BillingCycleType = Models.Offers.BillingCycleType;

    /// <summary>
    /// Showcases creating an order with a customer user UPN for license assignment.
    /// </summary>
    internal class CreateOrderWithCustomerUserUpn : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Create Order with Customer User UPN"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerKey] as string;

            var productId = "CFQ7TTC0LH16"; // Modern Office Product.

            var modernOfficeAvailability = this.GetAvailability(partnerOperations, productId);

            var order = new Order()
            {
                ReferenceCustomerId = selectedCustomerId,
                BillingCycle = (BillingCycleType)modernOfficeAvailability.Sku.SupportedBillingCycles.First(),
                LineItems = new List<OrderLineItem>()
                {
                    new OrderLineItem()
                    {
                        OfferId = modernOfficeAvailability.CatalogItemId,
                        Quantity = modernOfficeAvailability.Sku.MinimumQuantity,
                        TermDuration = modernOfficeAvailability.Terms.FirstOrDefault().Duration,
                    }
                }
            };

            Console.Out.WriteLine("Please enter the customer user UPN for license assignment");
            var customerUserUpn = Console.ReadLine();
            Console.Out.WriteLine();

            order = partnerOperations.Customers.ById(selectedCustomerId).Orders.Create(order, customerUserUpn);

            Console.Out.WriteLine("Id: {0}", order.Id);
            Console.Out.WriteLine("Creation Date: {0}", order.CreationDate);
            Console.Out.WriteLine();
        }

        /// <summary>
        /// Retrieve an availability from the given productId.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="productId">A productId.</param>
        /// <returns>Availability item.</returns>
        private Availability GetAvailability(IAggregatePartner partnerOperations, string productId)
        {
            var skuId = partnerOperations.Products.ByCountry("US").ById(productId).Skus.ByTargetSegment("commercial").Get().Items?.FirstOrDefault()?.Id;
            var availabilities = partnerOperations.Products.ByCountry("US").ById(productId).Skus.ById(skuId).Availabilities.ByTargetSegment("commercial").Get();
            return availabilities?.Items?.FirstOrDefault();
        }
    }
}