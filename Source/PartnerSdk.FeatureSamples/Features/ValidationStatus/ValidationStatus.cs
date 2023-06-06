// -----------------------------------------------------------------------
// <copyright file="ValidationStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.ValidationStatus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter;
    using Microsoft.Store.PartnerCenter.Models.ValidationStatus.Enums;

    /// <summary>
    /// Showcases customer validation status
    /// </summary>
    internal class ValidationStatus : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Customer Validation Status"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var selectedCustomerId = (string)state[FeatureSamplesApplication.SelectedCustomerKey];

            // approved account status held in customer ID - "d05a2d91-c070-4a8d-98eb-704d506fe4e1"
            var validationStatus = partnerOperations.Customers.ById(selectedCustomerId).ValidationStatus.GetValidationStatus(ValidationType.Account);

            Console.WriteLine($"Validation Status retrieved for customer with ID: {selectedCustomerId}:");
            Console.WriteLine(this.DisplayValidationStatus(validationStatus));
        }

        /// <summary>
        /// Displays the validation status of the customer as a string.
        /// </summary>
        /// <param name="status">The <see cref="Models.ValidationStatus.ValidationStatus"/> of the customer.</param>
        /// <returns>A string representing the validation status of the customer.</returns>
        private string DisplayValidationStatus(Models.ValidationStatus.ValidationStatus status)
        {
            var sb = new StringBuilder("\n");

            sb.AppendLine($"Type: {status.Type}");
            sb.AppendLine($"Status: {status.Status}");
            sb.AppendLine($"Last Updated DateTime: {status.LastUpdateDateTime}");

            sb.AppendLine();

            return sb.ToString();
        }
    }
}
