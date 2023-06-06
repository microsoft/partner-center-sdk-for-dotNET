// -----------------------------------------------------------------------
// <copyright file="CreateOrderWithCustomTermEndDate.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Products;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;
    using Models.Orders;

    /// <summary>
    /// Showcases creating an order with a custom term end date.
    /// </summary>
    internal class CreateOrderWithCustomTermEndDate : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Create Order With Custom Term End Date"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerKey] as string;
            
            // Get the subscriptions for the customer.
            ResourceCollection<Subscription> customerSubscriptions = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.Get();
            IList<Subscription> subscriptions = new List<Subscription>(customerSubscriptions.Items);

            // Non-trial OnlineServicesNCE product (Office 365 E5).
            string productId = "CFQ7TTC0LF8S";
            string skuId = "0002";
            var availability = this.GetAvailability(partnerOperations, productId, skuId);
            string termDuration = availability.Terms.Last().Duration;

            var subToAlignWith = subscriptions.Where(sub => this.IsCoterminousSubscription(sub, termDuration)).FirstOrDefault();

            DateTime? customTermEndDate = subToAlignWith?.CommitmentEndDate;

            var order = new Order()
            {
                ReferenceCustomerId = selectedCustomerId,
                BillingCycle = Models.Offers.BillingCycleType.Monthly,
                LineItems = new List<OrderLineItem>()
                {
                    new OrderLineItem()
                    {
                        OfferId = availability.CatalogItemId,
                        Quantity = availability.Sku.MinimumQuantity,
                        LineItemNumber = 0,
                        TermDuration = termDuration,
                        CustomTermEndDate = customTermEndDate
                    }
                }
            };

            order = partnerOperations.Customers.ById(selectedCustomerId).Orders.Create(order);

            Console.Out.WriteLine("Id: {0}", order.Id);
            Console.Out.WriteLine("Creation Date: {0}", order.CreationDate);
            Console.Out.WriteLine("Offer Id: {0}", order.LineItems.First().OfferId);
            Console.Out.WriteLine("Term Duration: {0}", order.LineItems.First().TermDuration);
            Console.Out.WriteLine("Custom Term End Date: {0}", order.LineItems.First().CustomTermEndDate?.ToString("u"));
            Console.Out.WriteLine();
        }

        /// <summary>
        /// Retrieve an availability from the given productId.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="productId">A productId.</param>
        /// <param name="skuId">A skuId.</param>
        /// <returns>Availability item.</returns>
        private Availability GetAvailability(IAggregatePartner partnerOperations, string productId, string skuId)
        {
            var availabilities = partnerOperations.Products.ByCountry("US").ById(productId).Skus.ById(skuId).Availabilities.ByTargetSegment("commercial").Get();
            return availabilities?.Items?.FirstOrDefault();
        }

        /// <summary>
        /// Determines whether subscription is a valid coterminous option for the given purchase term duration.
        /// </summary>
        /// <param name="subscription">The subscription.</param>
        /// <param name="purchaseTermDuration">The purchase term duration.</param>
        /// <returns>Whether the subscription is valid to be aligned with.</returns>
        private bool IsCoterminousSubscription(Subscription subscription, string purchaseTermDuration)
        {
            var isModernOffice = string.Equals("OnlineServicesNCE", subscription.ProductType?.Id);
            return isModernOffice && string.Equals(SubscriptionStatus.Active, subscription.Status) && !subscription.IsTrial && string.Equals(purchaseTermDuration, subscription.TermDuration);
        }
    }
}
