// -----------------------------------------------------------------------
// <copyright file="CustomerQualificationOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Qualification
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Store.PartnerCenter.Models.Customers.V2;

    /// <summary>
    /// Showcases customer qualification
    /// </summary>
    internal class CustomerQualificationOperations : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Customer Qualification"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var selectedCustomerId = (string)state[FeatureSamplesApplication.SelectedCustomerKey];

            // Get Customer Qualifications
            var customerQualificationAsync = partnerOperations.Customers.ById(selectedCustomerId).Qualification.GetQualifications();
            Console.WriteLine("GET Customer Qualifications (Async API):\n{0}", this.DisplayCustomerQualifications(customerQualificationAsync));

            // Create Customer Qualifications
            var eduRequestBody = new CustomerQualificationRequest { Qualification = "Education", EducationSegment = "K12", Website = "example.edu" };
            var gccRequestBody = new CustomerQualificationRequest { Qualification = "GovernmentCommunityCloud", ValidationCode = "NotRealCode" };
            var soeRequestBody = new CustomerQualificationRequest { Qualification = "StateOwnedEntity" };

            // Executing asynchronous operation
            var createCustomerQualifications = partnerOperations.Customers.ById(selectedCustomerId).Qualification.CreateQualifications(eduRequestBody);

            // Displaying outputs
            Console.WriteLine("Customer Id : {0}", selectedCustomerId);
            Console.WriteLine("Create Customer Qualifications: {0} - {1}", createCustomerQualifications?.Qualification?.ToString(), createCustomerQualifications?.VettingStatus);
            Console.WriteLine();

            // this should say that GCC and Edu cannot be combined
            try
            {
                var createCustomerGCCQualifications = partnerOperations.Customers.ById(selectedCustomerId).Qualification.CreateQualifications(gccRequestBody);
            }
            catch (Exception e)
            {
                Console.WriteLine("Create Customer Qualifications {0}: {1}", gccRequestBody.Qualification, e.Message);
                Console.WriteLine();
            }

            // Executing asynchronous SOE operation
            var createCustomerSOEQualifications = partnerOperations.Customers.ById(selectedCustomerId).Qualification.CreateQualifications(soeRequestBody);
            Console.WriteLine("Create Customer Qualifications {0}: {1}", soeRequestBody.Qualification, createCustomerSOEQualifications?.Qualification?.ToString());
        }

        /// <summary>
        /// Displays the async return object for customer qualifications.
        /// </summary>
        /// <param name="customerQualifications">The customer's qualifications.</param>
        /// <returns>A string to display the customer's qualifications.</returns>
        private string DisplayCustomerQualifications(IEnumerable<Models.Customers.V2.CustomerQualification> customerQualifications)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var qualification in customerQualifications)
            {
                sb.AppendLine($"Qualification: {qualification.Qualification}");
                sb.AppendLine($"Vetting Status: {qualification.VettingStatus ?? "No status to display."}");
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
