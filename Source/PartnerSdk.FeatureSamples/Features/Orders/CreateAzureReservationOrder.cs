// -----------------------------------------------------------------------
// <copyright file="CreateAzureReservationOrder.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Orders
{
    using System;
    using System.Collections.Generic;
    using Models.Customers;
    using Models.Offers;
    using Models.Orders;

    /// <summary>
    /// Showcases creating an Azure reservation order.
    /// </summary>
    internal class CreateAzureReservationOrder : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Create Azure Reservation Order"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerKey] as string;
            string offerId = "DZH318Z0BQ3P:000S:DZH318Z0DDQ1";

            var order = new Order()
            {
                PartnerOnRecordAttestationAccepted = true,
                ReferenceCustomerId = selectedCustomerId,
                BillingCycle = BillingCycleType.OneTime,
                LineItems = new List<OrderLineItem>()
                {
                    new OrderLineItem()
                    {
                        OfferId = offerId,
                        FriendlyName = "ASampleAzureRI",
                        Quantity = 1,
                        LineItemNumber = 0,
                        ProvisioningContext = new Dictionary<string, string>()
                        {
                            { "subscriptionId", "5198C069-3DAA-403A-8660-5BE11BFD12EE" },
                            { "scope", "shared" },
                            { "duration", "3years" }
                        }
                    }
                }
            };

            order = partnerOperations.Customers.ById(selectedCustomerId).Orders.Create(order);

            Console.Out.WriteLine("Id: {0}", order.Id);
            Console.Out.WriteLine("Creation Date: {0}", order.CreationDate);
            Console.Out.WriteLine();
        }
    }
}
