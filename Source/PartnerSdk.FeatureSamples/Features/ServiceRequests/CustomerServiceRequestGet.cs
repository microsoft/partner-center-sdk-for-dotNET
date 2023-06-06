// <copyright file="CustomerServiceRequestGet.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.ServiceRequests
{
    using System;
    using System.Collections.Generic;
    using Models.ServiceRequests;

    /// <summary>
    /// Showcases customer creation.
    /// </summary>
    internal class CustomerServiceRequestGet : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Get Customer Service Request";
            }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            string customerId = state[FeatureSamplesApplication.CustomerForServiceRequests] as string;
            string serviceRequestId = state[FeatureSamplesApplication.CustomerServiceRequest] as string;

            ServiceRequest serviceRequest =
                partnerOperations.Customers.ById(customerId)
                    .ServiceRequests.ById(serviceRequestId)
                    .Get();

            Console.WriteLine("Retrieved Service Request");
            Console.WriteLine("Id: {0}", serviceRequest.Id);
            Console.WriteLine("Title: {0}", serviceRequest.Title);
            Console.WriteLine("Description: {0}", serviceRequest.Description);
            Console.WriteLine("CreatedDate: {0}", serviceRequest.CreatedDate);
            Console.WriteLine("Severity: {0}", serviceRequest.Severity);
            Console.WriteLine("Status: {0}", serviceRequest.Status);
            Console.WriteLine("SupportTopicId: {0}", serviceRequest.SupportTopicId);
            Console.WriteLine("SupportTopicName: {0}", serviceRequest.SupportTopicName);

            if (serviceRequest.PrimaryContact != null)
            {
                Console.WriteLine("PrimaryContact.FirstName: {0}", serviceRequest.PrimaryContact.FirstName);
                Console.WriteLine("PrimaryContact.LastName: {0}", serviceRequest.PrimaryContact.LastName);
                Console.WriteLine("PrimaryContact.Id: {0}", serviceRequest.PrimaryContact.ContactId);
                Console.WriteLine("PrimaryContact.PhoneNumber: {0}", serviceRequest.PrimaryContact.PhoneNumber);
                Console.WriteLine("PrimaryContact.Email: {0}", serviceRequest.PrimaryContact.Email);

                if (serviceRequest.PrimaryContact.Organization != null)
                {
                    Console.WriteLine("PrimaryContact.Organization.Id: {0}", serviceRequest.PrimaryContact.Organization.Id);
                    Console.WriteLine("serviceRequest.PrimaryContact.Organization.Name:{0} ", serviceRequest.PrimaryContact.Organization.Name);
                    Console.WriteLine("PrimaryContact.Organization.PhoneNumber: {0}", serviceRequest.PrimaryContact.Organization.PhoneNumber);
                }
            }

            if (serviceRequest.Organization != null)
            {
                Console.WriteLine("Organization.Id: {0}", serviceRequest.Organization.Id);
                Console.WriteLine("Organization.Name: {0}", serviceRequest.Organization.Name);
                Console.WriteLine("Organization.PhoneNumber: {0}", serviceRequest.Organization.PhoneNumber);
            }
        }
    }
}
