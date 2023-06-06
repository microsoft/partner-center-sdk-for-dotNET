// -----------------------------------------------------------------------
// <copyright file="ICustomerRelationshipRequest.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.RelationshipRequests
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.RelationshipRequests;

    /// <summary>
    /// Holds operations that apply to customer relationship requests.
    /// </summary>
    public interface ICustomerRelationshipRequest : IPartnerComponent, IEntityGetOperations<CustomerRelationshipRequest>
    {
        /// <summary>
        /// Retrieves the customer relationship request which can be used by a customer to establish a relationship with the current partner.
        /// </summary>
        /// <returns>A customer relationship request.</returns>
        new CustomerRelationshipRequest Get();

        /// <summary>
        /// Asynchronously retrieves the customer relationship request which can be used by a customer to establish a relationship with the current partner.
        /// </summary>
        /// <returns>A customer relationship request.</returns>
        new Task<CustomerRelationshipRequest> GetAsync();
    }
}
