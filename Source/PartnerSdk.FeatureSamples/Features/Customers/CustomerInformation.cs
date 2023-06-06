// <copyright file="CustomerInformation.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Customers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using Models.Customers;

    /// <summary>
    /// Showcases customer information.
    /// </summary>
    internal class CustomerInformation : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "View Customer Information"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var defaultCustomerId = ConfigurationManager.AppSettings["defaultCustomerId"];

            Customer selectedCustomer = ((List<Customer>)state[FeatureSamplesApplication.CustomersKey])
                .FirstOrDefault(customer => string.Equals(customer.Id, defaultCustomerId, StringComparison.OrdinalIgnoreCase));

            if (selectedCustomer == null)
            {
                selectedCustomer = ((List<Customer>)state[FeatureSamplesApplication.CustomersKey])[0];
            }

            // get customer information
            Customer customerInfo = partnerOperations.Customers.ById(selectedCustomer.Id).Get();

            // display customer info
            Console.Out.WriteLine("Customer Id: {0} ", customerInfo.Id);
            Console.Out.WriteLine("CompanyName: {0} ", customerInfo.CompanyProfile.CompanyName);

            // set the selected customer ID into the application state
            state[FeatureSamplesApplication.SelectedCustomerKey] = selectedCustomer.Id;
        }
    }
}
