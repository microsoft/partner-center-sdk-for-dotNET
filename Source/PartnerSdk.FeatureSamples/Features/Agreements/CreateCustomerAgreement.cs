// -----------------------------------------------------------------------
// <copyright file="CreateCustomerAgreement.cs" company="Microsoft Corporation">
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
    /// Showcases creation of a customer agreement (Microsoft Cloud Agreement/ Microsoft Customer Agreement) attestation.
    /// </summary>
    internal class CreateCustomerAgreement : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Create customer agreement (Microsoft Cloud Agreement/ Microsoft Customer Agreement) attestation."; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var agreements = partnerOperations.AgreementDetails.ByAgreementType("*").Get();
            foreach (var agreement in agreements.Items)
            {
                this.CreateAgreement(partnerOperations, state, agreement);
            }
        }

        /// <summary>
        /// Utility method to create an agreement attestation.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        /// <param name="agreementMetadata">The agreement metadata (that contains the template ID) used to create an agreement.</param>
        private void CreateAgreement(IAggregatePartner partnerOperations, IDictionary<string, object> state, AgreementMetaData agreementMetadata)
        {
            var selectedCustomerId = state[FeatureSamplesApplication.CustomerWithAgreements] as string;
            var selectedUserId = state[FeatureSamplesApplication.PartnerUserForAgreement] as string;

            var agreement = new Agreement
            {
                UserId = selectedUserId,
                DateAgreed = DateTime.UtcNow,
                Type = agreementMetadata.AgreementType,
                TemplateId = agreementMetadata.TemplateId,
                PrimaryContact = new Contact
                {
                    FirstName = "First",
                    LastName = "Last",
                    Email = "first.last@outlook.com",
                    PhoneNumber = "4123456789"
                }
            };

            agreement = partnerOperations.Customers.ById(selectedCustomerId).Agreements.Create(agreement);

            Console.Out.WriteLine("Customer agreement created details");
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
