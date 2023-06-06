// <copyright file="DeleteCustomerFromTipAccount.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Customers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using Extensions;

    /// <summary>
    /// Showcases the delete customer from testing in production account API.
    /// </summary>
    internal class DeleteCustomerFromTipAccount : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Delete customer from TIP account"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // since this operation can only be performed on TIP accounts, we need to login to a TIP account here
            // and ignore the given partner operations which relies on a stadard account
            IPartnerCredentials tipAccountCredentials = PartnerCredentials.Instance.GenerateByApplicationCredentials(
                ConfigurationManager.AppSettings["tipAccount.application.id"],
                ConfigurationManager.AppSettings["tipAccount.application.secret"],
                ConfigurationManager.AppSettings["tipAccount.application.domain"],
                ConfigurationManager.AppSettings["aad.authority"],
                ConfigurationManager.AppSettings["aad.graphEndpoint"]);

            IPartner tipAccountPartnerOperations = PartnerService.Instance.CreatePartnerOperations(tipAccountCredentials);

            // let the user enter the tenant ID of the customer to be deleted
            Console.WriteLine("Enter the AAD tenant ID of the customer to be deleted from the tip account:");
            string customerTenantId = Console.ReadLine();
            
            // delete the customer
            Console.WriteLine("Deleting...");
            tipAccountPartnerOperations.Customers.ById(customerTenantId).Delete();
            Console.WriteLine("Deletion complete!");
        }
    }
}
