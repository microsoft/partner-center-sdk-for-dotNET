// <copyright file="PartnerServiceRequestsPagination.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.ServiceRequests
{
    using System;
    using System.Collections.Generic;
    using Models.Query;
    using RequestContext;

    /// <summary>
    /// Showcases service requests search with pagination
    /// </summary>
    internal class PartnerServiceRequestsPagination : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Search Partner Service Request: Pagination";
            }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            IPartner scopedPartnerOperations =
                partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

            var serviceRequestsBatch = scopedPartnerOperations.ServiceRequests.Query(QueryFactory.Instance.BuildIndexedQuery(5));
            var serviceRequestsEnumerator = scopedPartnerOperations.Enumerators.ServiceRequests.Create(serviceRequestsBatch);

            while (serviceRequestsEnumerator.HasValue)
            {
                Console.Out.WriteLine("Service Request count: " + serviceRequestsEnumerator.Current.TotalCount);

                foreach (var serviceRequest in serviceRequestsEnumerator.Current.Items)
                {
                    Console.Out.WriteLine("Id: {0}", serviceRequest.Id);
                    Console.Out.WriteLine("Title: {0}", serviceRequest.Title);
                    Console.Out.WriteLine("Creation Date: {0}", serviceRequest.CreatedDate);
                    Console.Out.WriteLine("Status: {0}", serviceRequest.Status);
                    Console.Out.WriteLine("Name: {0} {1}", serviceRequest.PrimaryContact.FirstName, serviceRequest.PrimaryContact.LastName);

                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Enter 'n' to go Next Page; Enter 'p' to go to Previous Page; Enter 'x' to exit feature");
                char w = Console.ReadKey().KeyChar;

                if (w == 'n')
                {
                    serviceRequestsEnumerator.Next();
                }

                if (w == 'p')
                {
                    serviceRequestsEnumerator.Previous();
                }

                if (w == 'x')
                {
                    break;
                }
            }
        }
    }
}