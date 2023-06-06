// -----------------------------------------------------------------------
// <copyright file="ConsoleHelper.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.SubscribedSkus;
    using Models;
    using Models.Licenses;

    /// <summary>
    /// Helper class to write common properties to console.
    /// </summary>
    public static class ConsoleHelper
    {
        /// <summary>
        /// Write subscribed SKU details to console.
        /// </summary>
        /// <param name="subscribedSkus">Subscribed SKU list.</param>
        public static void WriteSkusToConsole(IEnumerable<SubscribedSku> subscribedSkus)
        {
            Console.Out.WriteLine("Subscribed Skus Count: " + subscribedSkus.Count());

            foreach (SubscribedSku subscribedSku in subscribedSkus)
            {
                Console.Out.WriteLine("Subscribed Sku Id: {0}", subscribedSku.ProductSku.Id);
                Console.Out.WriteLine("Subscribed Sku License Group Id: {0}", subscribedSku.ProductSku.LicenseGroupId);
                Console.Out.WriteLine("Subscribed Sku Name: {0}", subscribedSku.ProductSku.Name);
                Console.Out.WriteLine("Subscribed Sku Target type: {0}", subscribedSku.ProductSku.TargetType);

                Console.Out.WriteLine("Subscribed Sku ActiveUnits: {0}", subscribedSku.ActiveUnits);
                Console.Out.WriteLine("Subscribed Sku AvailableUnits: {0}", subscribedSku.AvailableUnits);
                Console.Out.WriteLine("Subscribed Sku ConsumedUnits: {0}", subscribedSku.ConsumedUnits);
                Console.Out.WriteLine("Subscribed Sku SuspendedUnits: {0}", subscribedSku.SuspendedUnits);
                Console.Out.WriteLine("Subscribed Sku TotalUnits: {0}", subscribedSku.TotalUnits);
                Console.Out.WriteLine("Subscribed Sku WarningUnits: {0}", subscribedSku.WarningUnits);
                Console.Out.WriteLine("Subscribed Sku capability status: {0}", subscribedSku.CapabilityStatus);

                Console.Out.WriteLine("Subscribed Sku Serviceplans Count: {0}", subscribedSku.ServicePlans.Count);
                foreach (ServicePlan servicePlan in subscribedSku.ServicePlans)
                {
                    Console.Out.WriteLine("Subscribed Sku service plan display name: {0}", servicePlan.DisplayName);
                    Console.Out.WriteLine("Subscribed Sku service plan service name: {0}", servicePlan.ServiceName);
                    Console.Out.WriteLine("Subscribed Sku service plan service id: {0}", servicePlan.Id);
                    Console.Out.WriteLine("Subscribed Sku service plan target type: {0}", servicePlan.TargetType);

                    Console.Out.WriteLine("Subscribed Sku service plan capability status: {0}", servicePlan.CapabilityStatus);
                }

                Console.Out.WriteLine();
            }
        }

        /// <summary>
        /// Write license information to console.
        /// </summary>
        /// <param name="licenses">List of licenses.</param>
        public static void WriteLicensesToConsole(IEnumerable<License> licenses)
        {
            Console.Out.WriteLine("Licenses Count: " + licenses.Count());

            foreach (License license in licenses)
            {
                Console.Out.WriteLine("License Group Id: {0}", license.ProductSku.LicenseGroupId);

                var servicePlans = license.ServicePlans.ToList();
                Console.Out.WriteLine("License ServicePlans Count: {0}", servicePlans.Count);
                foreach (ServicePlan servicePlan in servicePlans)
                {
                    Console.Out.WriteLine("License service plan display name: {0}", servicePlan.DisplayName);
                    Console.Out.WriteLine("License service plan service name: {0}", servicePlan.ServiceName);
                    Console.Out.WriteLine("License service plan service id: {0}", servicePlan.Id);
                    Console.Out.WriteLine("License service plan capability status: {0}", servicePlan.CapabilityStatus);
                    Console.Out.WriteLine("License service plan target type: {0}", servicePlan.TargetType);
                }
            }
        }
    }
}
