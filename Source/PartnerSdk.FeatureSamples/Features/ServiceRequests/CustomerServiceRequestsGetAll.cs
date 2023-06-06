// <copyright file="CustomerServiceRequestsGetAll.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.ServiceRequests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Models.ServiceRequests;

    /// <summary>
    /// Showcases customer creation.
    /// </summary>
    internal class CustomerServiceRequestsGetAll : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Get All Customer Service Requests";
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

            ResourceCollection<ServiceRequest> serviceRequests =
                partnerOperations.Customers.ById(customerId)
                    .ServiceRequests.Get();

            if (serviceRequests.Items.Any())
            {
                state[FeatureSamplesApplication.CustomerServiceRequest] = serviceRequests.Items.OrderBy(sr => Guid.NewGuid()).First().Id;
            }

            foreach (var serviceRequest in serviceRequests.Items)
            {
                Console.Out.WriteLine("Id: {0}", serviceRequest.Id);
                Console.Out.WriteLine("Title: {0}", serviceRequest.Title);
                Console.Out.WriteLine("Creation Date: {0}", serviceRequest.CreatedDate);
                Console.Out.WriteLine("Status: {0}", serviceRequest.Status);
                Console.Out.WriteLine("Name: {0} {1}", serviceRequest.PrimaryContact.FirstName, serviceRequest.PrimaryContact.LastName);

                Console.WriteLine();
            }
        }
    }
}
