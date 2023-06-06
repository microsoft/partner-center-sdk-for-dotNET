// <copyright file="GetDirectSignedCustomerAgreementStatus.cs" company="Microsoft Corporation">
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
    /// Showcases the retrieval of the status of the direct signed customer agreement.
    /// </summary>
    internal class GetDirectSignedCustomerAgreementStatus : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get direct signed customer agreement status"; }
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
            var directSignedAgreementStatus = partnerOperations.Customers.ById(selectedCustomerId).Agreements.GetDirectSignedCustomerAgreementStatus();
            Console.WriteLine($"Direct signed customer agreement status: {directSignedAgreementStatus.IsSigned}");
        }
    }
}
