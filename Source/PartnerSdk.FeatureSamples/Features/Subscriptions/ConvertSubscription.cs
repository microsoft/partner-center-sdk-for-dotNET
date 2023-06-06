// <copyright file="ConvertSubscription.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Subscriptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Store.PartnerCenter.FeatureSamples;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Offers;
    using Microsoft.Store.PartnerCenter.Models.Orders;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;

    /// <summary>
    /// Showcases converting a trial subscription.
    /// </summary>
    internal class ConvertSubscription : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title => "Subscription Conversion";

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // Read the customer from the application state.
            var selectedCustomerId = state[FeatureSamplesApplication.CustomerWithTrialOffer] as string;

            if (selectedCustomerId == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Out.WriteLine("Customer ID cannot be null");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Out.WriteLine();
                return;
            }
                        
            var offers = state[FeatureSamplesApplication.OffersKey] as List<Offer>;

            // Retrieve the trial offer.
            var trialOffer = offers.Where(o => o.Id.Equals(state[FeatureSamplesApplication.TrialOfferId].ToString(), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (trialOffer == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Out.WriteLine(string.Format("Trial offer {0} cannot be found.", state[FeatureSamplesApplication.TrialOfferId].ToString()));
                Console.ForegroundColor = ConsoleColor.White;
                Console.Out.WriteLine();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Out.WriteLine(string.Format("Found Trial offer {0}.", state[FeatureSamplesApplication.TrialOfferId].ToString()));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Out.WriteLine();

            // Purchase the trial offer.
            var order = new Order()
            {
                ReferenceCustomerId = selectedCustomerId,
                LineItems = new List<OrderLineItem>()
                {
                    new OrderLineItem()
                    {
                        LineItemNumber = 0,
                        OfferId = trialOffer.Id,
                        FriendlyName = trialOffer.Name,
                        Quantity = trialOffer.MinimumQuantity
                    }
                },
                BillingCycle = BillingCycleType.None
            };

            try
            {
                order = partnerOperations.Customers.ById(selectedCustomerId).Orders.Create(order);
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals(string.Format("Use limit is exceeded for Offer id '{0}'", trialOffer.Id), StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(string.Format("The custeomer {0} has already purchased trial offer {1}, please use another customer.", selectedCustomerId, trialOffer.Id));
                    Console.Out.WriteLine();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Out.WriteLine("Purchase order failed.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Out.WriteLine();
                }

                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Out.WriteLine("Succesfully purchased order: {0}", order.Id);
            Console.Out.WriteLine("Creation Date: {0}", order.CreationDate);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Out.WriteLine();

            // Get the subscriptions for the customer.
            ResourceCollection<Subscription> customerSubscriptions = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.Get();
            if (customerSubscriptions?.Items == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Out.WriteLine("Subscriptions cannot be null");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            // Retrieve the trial subscription.
            var trialSubscription = customerSubscriptions.Items.First(s => s.OfferId.Equals(trialOffer.Id, StringComparison.OrdinalIgnoreCase));
            if (trialSubscription == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Out.WriteLine("Cannot find trial subscription.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Out.WriteLine(string.Format("Found Trial Subscription {0}.", trialSubscription.Id));
            Console.Out.WriteLine(string.Format("Trial Subscription name {0}.", trialSubscription.FriendlyName));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Out.WriteLine();

            // Get the conversions for the trial subscription.
            var conversions = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(trialSubscription.Id).Conversions.Get();
            if (conversions?.Items == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Out.WriteLine("Conversions cannot be null");
                Console.ForegroundColor = ConsoleColor.White;
            }

            var taretConversion = conversions.Items.ToList()[0];
            Console.Out.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Out.WriteLine("Converting trial subscription {0} for Customer {1} to paid offer {2}", trialSubscription.Id, selectedCustomerId, taretConversion.TargetOfferId);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Out.WriteLine();

            // Perform the conversion.
            ConversionResult conversionResult = partnerOperations.Customers.ById(selectedCustomerId).Subscriptions.ById(trialSubscription.Id).Conversions.Create(taretConversion);
            if (conversionResult.Error != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Out.WriteLine("Errors encountered during conversion: ");
                Console.Out.WriteLine("{0} - {1}", conversionResult.Error.Code, conversionResult.Error.Description);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Out.WriteLine();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Out.WriteLine("Conversion completed successfully. Source SubscriptionId {0}, Original Offer Id {1}, Target Offer Id {2}", conversionResult.SubscriptionId, conversionResult.OfferId, conversionResult.TargetOfferId);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Out.WriteLine();
            Console.Out.WriteLine();
        }
    }
}
