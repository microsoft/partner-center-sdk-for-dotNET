// <copyright file="Entitlements.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Entitlements
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Entitlements;

    /// <summary>
    /// Entitlement collection test
    /// </summary>
    internal class Entitlements : IUnitOfWork
    {
        /// <summary>
        /// Represents reserved instance string
        /// </summary>
        public readonly string ReservedInstance = "reservedinstance";

        /// <summary>
        /// Represents software string
        /// </summary>
        public readonly string Software = "software";

        /// <summary>
        /// Gets Title
        /// </summary>
        public string Title => "Get entitlements.";

        /// <summary>
        /// Gets the entitlement collection 
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the selected customer id from config.
            var selectedCustomerId = ConfigurationManager.AppSettings["defaultCustomerId"];
            
            // get the entitlement collection.
            ResourceCollection<Entitlement> entitlements = partnerOperations.Customers.ById(selectedCustomerId).Entitlements.Get();
            
            // display the entitlements
            Console.Out.WriteLine("Entitlement count: " + entitlements.TotalCount);

            Console.Out.WriteLine("Software entitlement count: " + entitlements.Items.Count(x => string.Equals(x.EntitlementType, this.Software, StringComparison.OrdinalIgnoreCase)));
            Console.Out.WriteLine();

            foreach (var customerEntitlement in entitlements.Items.Where(x => string.Equals(x.EntitlementType, this.Software, StringComparison.OrdinalIgnoreCase)))
            {
                Console.Out.WriteLine("Entitlement:");
                Console.Out.WriteLine("Product-Sku: {0}-{1}", customerEntitlement.ProductId, customerEntitlement.SkuId);
                Console.Out.WriteLine("ReferenceOrder: {0} : {1} : {2}", customerEntitlement.ReferenceOrder.Id, customerEntitlement.ReferenceOrder.LineItemId, customerEntitlement.ReferenceOrder.AlternateId);
                Console.Out.WriteLine("Quantity: {0}", customerEntitlement.Quantity);
            }

            Console.Out.WriteLine("Virtual machine reservation entitlement count: " + entitlements.Items.Count(x => string.Equals(x.EntitlementType, this.ReservedInstance, StringComparison.OrdinalIgnoreCase)));
            Console.Out.WriteLine();
            
            foreach (var customerEntitlement in entitlements.Items.Where(x => string.Equals(x.EntitlementType, this.ReservedInstance, StringComparison.OrdinalIgnoreCase)))
            {
                Console.Out.WriteLine("Entitlement:");
                Console.Out.WriteLine("Product-Sku: {0}-{1}", customerEntitlement.ProductId, customerEntitlement.SkuId);
                Console.Out.WriteLine("ReferenceOrder: {0} : {1}", customerEntitlement.ReferenceOrder.Id, customerEntitlement.ReferenceOrder.LineItemId);
                Console.Out.WriteLine("Quantity: {0}", customerEntitlement.Quantity);
                Console.Out.WriteLine("QuantityDetails: {0}", string.Join(", ", customerEntitlement.QuantityDetails.Select(x => $"{x.Quantity} - {x.Status}")));

                var reservedInstanceArtifactDetails = ((ReservedInstanceArtifact)customerEntitlement.EntitledArtifacts.First(x => string.Equals(x.ArtifactType, this.ReservedInstance, StringComparison.OrdinalIgnoreCase))).Link.InvokeAsync<ReservedInstanceArtifactDetails>(partnerOperations).Result;
                Console.Out.WriteLine("ReservationType: {0}", customerEntitlement.EntitledArtifacts.First(x => string.Equals(x.ArtifactType, this.ReservedInstance, StringComparison.OrdinalIgnoreCase)).DynamicAttributes["reservationType"]);
                foreach (var reservation in reservedInstanceArtifactDetails.Reservations)
                {
                    Console.Out.WriteLine("Display Name: {0}", reservation.DisplayName);
                    Console.Out.WriteLine("ReservationId: {0}", reservation.ReservationId);
                    Console.Out.WriteLine("ScopeType: {0}", reservation.ScopeType);
                    Console.Out.WriteLine("Quantity: {0}", reservation.Quantity);
                    Console.Out.WriteLine("ExpiryDateTime: {0}", reservation.ExpiryDateTime);
                    Console.Out.WriteLine("EffectiveDateTime: {0}", reservation.EffectiveDateTime);
                    Console.Out.WriteLine("ProvisioningState: {0}", reservation.ProvisioningState);
                }

                Console.Out.WriteLine();
            }
        }
    }
}
