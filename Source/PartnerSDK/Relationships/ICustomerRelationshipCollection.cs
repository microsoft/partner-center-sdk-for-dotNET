// -----------------------------------------------------------------------
// <copyright file="ICustomerRelationshipCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Relationships
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Relationships;

    /// <summary>
    /// For two-tier partner scenarios, holds the operations for retrieving partner relationships of a customer with respect to
    /// the logged in partner.
    /// </summary>
    public interface ICustomerRelationshipCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<PartnerRelationship, ResourceCollection<PartnerRelationship>>
    {
        /// <summary>
        /// Retrieves all the partner relationships associated to the customer based on the logged in partner.
        /// </summary>
        /// <returns>The partner relationships.</returns>
        new ResourceCollection<PartnerRelationship> Get();

        /// <summary>
        /// Asynchronously retrieves all the partner relationships associated to the customer based on the logged in partner.
        /// </summary>
        /// <returns>The partner relationships.</returns>
        new Task<ResourceCollection<PartnerRelationship>> GetAsync();
    }
}
