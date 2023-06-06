// -----------------------------------------------------------------------
// <copyright file="ICustomerCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Customers;
    using Models.Query;
    using RelationshipRequests;
    using Usage;

    /// <summary>
    /// Represents the behavior of the partner customers.
    /// </summary>
    public interface ICustomerCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<Customer, SeekBasedResourceCollection<Customer>>, IEntityCreateOperations<Customer>, IEntitySelector<ICustomer>
    {
        /// <summary>
        /// Obtains the customer usage records behavior for the partner.
        /// </summary>
        /// <returns>The requested customers usage record.</returns>
        ICustomerUsageRecordCollection UsageRecords { get; }

        /// <summary>
        /// Gets the relationship request behavior used to relate customers into the partner.
        /// </summary>
        ICustomerRelationshipRequest RelationshipRequest { get; }

        /// <summary>
        /// Gets the customer operations for a specific customer.
        /// </summary>
        /// <param name="customerId">The customer id.</param>
        /// <returns>The customer operations.</returns>
        new ICustomer this[string customerId] { get; }

        /// <summary>
        /// Gets the customer operations for a specific customer.
        /// </summary>
        /// <param name="customerId">The customer id.</param>
        /// <returns>The customer operations.</returns>
        new ICustomer ById(string customerId);

        /// <summary>
        /// Retrieves all the partner's customers.
        /// </summary>
        /// <returns>The partner's customers.</returns>
        new SeekBasedResourceCollection<Customer> Get();

        /// <summary>
        /// Asynchronously retrieves all the partner's customers.
        /// </summary>
        /// <returns>The partner's customers..</returns>
        new Task<SeekBasedResourceCollection<Customer>> GetAsync();

        /// <summary>
        /// Queries customers associated to the partner.
        /// - Count queries are not supported by this operation.
        /// - You can set the page size or filter or do both at the same time.
        /// - Sort is not supported.
        /// - You can navigate to other pages by specifying a seek query with the seek operation and the continuation
        ///   token sent by the previous operation.
        /// </summary>
        /// <param name="customersQuery">A query to apply onto customers. Check <see cref="QueryFactory"/> to see how
        /// to build queries.</param>
        /// <returns>The requested customers.</returns>
        SeekBasedResourceCollection<Customer> Query(IQuery customersQuery);

        /// <summary>
        /// Asynchronously queries customers associated to the partner.
        /// - Count queries are not supported by this operation.
        /// - You can set the page size or filter or do both at the same time.
        /// - Sort is not supported.
        /// - You can navigate to other pages by specifying a seek query with the seek operation and the continuation
        ///   token sent by the previous operation.
        /// </summary>
        /// <param name="customersQuery">A query to apply onto customers. Check <see cref="QueryFactory"/> to see how
        /// to build queries.</param>
        /// <returns>The requested customers.</returns>
        Task<SeekBasedResourceCollection<Customer>> QueryAsync(IQuery customersQuery);
    }
}
