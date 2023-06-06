// <copyright file="GetCustomerRelationshipRequest.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Customers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using FeatureSamples;

    /// <summary>
    /// Showcases the get customer relationship request API.
    /// </summary>
    internal class GetCustomerRelationshipRequest : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get customer relationship request"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var customerRelationshipRequest = partnerOperations.Customers.RelationshipRequest.Get();
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Url: {0}", customerRelationshipRequest.Url.ToString()));
        }
    }
}
