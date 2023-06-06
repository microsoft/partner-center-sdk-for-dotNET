// <copyright file="GetCustomerAgreements.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Agreements
{
    using System;
    using System.Collections.Generic;
    using Models.Agreements;
    
    /// <summary>
    /// Showcases the retrieval of customer agreements.
    /// </summary>
    internal class GetCustomerAgreements : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get all customer agreements"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read the customer from the application state
            var selectedCustomerId = state[FeatureSamplesApplication.CustomerWithAgreements] as string;

            // Get all agreements of the customer.
            var agreements = partnerOperations.Customers.ById(selectedCustomerId).Agreements.ByAgreementType("*").Get();
            Console.Out.WriteLine("Count of all agreements: " + agreements.TotalCount);
            Display(agreements.Items);

            // Get agreements of a specific type of the customer.
            var microsoftCloudAgreements = partnerOperations.Customers.ById(selectedCustomerId).Agreements.ByAgreementType("MicrosoftCloudAgreement").Get();
            Console.Out.WriteLine("Count of agreements of type Microsoft Cloud Agreement: " + microsoftCloudAgreements.TotalCount);
            Display(microsoftCloudAgreements.Items);

            var microsoftCustomerAgreements = partnerOperations.Customers.ById(selectedCustomerId).Agreements.ByAgreementType("MicrosoftCustomerAgreement").Get();
            Console.Out.WriteLine("Count of agreements of type Microsoft Customer Agreement: " + microsoftCustomerAgreements.TotalCount);
            Display(microsoftCustomerAgreements.Items);
        }

        /// <summary>
        /// Convenience method to display agreement details.
        /// </summary>
        /// <param name="agreements">The list of agreements to display.</param>
        private static void Display(IEnumerable<Agreement> agreements)
        {
            foreach (var agreement in agreements)
            {
                Console.Out.WriteLine("User Id: {0}", agreement.UserId);
                Console.Out.WriteLine("Date agreed: {0}", agreement.DateAgreed);
                Console.Out.WriteLine("Agreement type: {0}", agreement.Type);
                Console.Out.WriteLine("Agreement link: {0}", agreement.AgreementLink);
                Console.Out.WriteLine("Contact first name: {0}", agreement.PrimaryContact.FirstName);
                Console.Out.WriteLine("Contact last name: {0}", agreement.PrimaryContact.LastName);
                Console.Out.WriteLine("Contact email address: {0}", agreement.PrimaryContact.Email);
                Console.Out.WriteLine("Contact phone number: {0}", agreement.PrimaryContact.PhoneNumber);
                Console.Out.WriteLine();
            }
        }
    }
}
