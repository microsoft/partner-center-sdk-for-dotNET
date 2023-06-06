// -----------------------------------------------------------------------
// <copyright file="INewCommerceMigrationCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.NewCommerceMigrations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.GenericOperations;
    using Microsoft.Store.PartnerCenter.Models.NewCommerceMigrations;

    /// <summary>
    /// Encapsulates the collection behavior for a New-Commerce migration.
    /// </summary>
    public interface INewCommerceMigrationCollection : IPartnerComponent, IEntityCreateOperations<NewCommerceMigration>, IEntitySelector<INewCommerceMigration>
    {
        /// <summary>
        /// Gets a specific New-Commerce migration behavior.
        /// </summary>
        /// <param name="newCommerceMigrationId">The New-Commerce migration ID.</param>
        /// <returns>The New-Commerce migration operations.</returns>
        new INewCommerceMigration this[string newCommerceMigrationId] { get; }

        /// <summary>
        /// Gets a specific New-Commerce migration behavior.
        /// </summary>
        /// <param name="newCommerceMigrationId">The New-Commerce migration ID.</param>
        /// <returns>The New-Commerce migration operations.</returns>
        new INewCommerceMigration ById(string newCommerceMigrationId);

        /// <summary>
        /// Creates a New-Commerce migration.
        /// </summary>
        /// <param name="newCommerceMigration">A New-Commerece migration to be created.</param>
        /// <returns>The New-Commerce migration that was just created.</returns>
        new NewCommerceMigration Create(NewCommerceMigration newCommerceMigration);

        /// <summary>
        /// Asynchronously creates a New-Commerce migration.
        /// </summary>
        /// <param name="newCommerceMigration">A New-Commerece migration to be created.</param>
        /// <returns>The New-Commerce migration that was just created.</returns>
        new Task<NewCommerceMigration> CreateAsync(NewCommerceMigration newCommerceMigration);

        /// <summary>
        /// Validates a New-Commerce migration.
        /// </summary>
        /// <param name="newCommerceMigration">The New-Commerece migration to be validated.</param>
        /// <returns>The New-Commerce migration eligibility.</returns>
        NewCommerceEligibility Validate(NewCommerceMigration newCommerceMigration);

        /// <summary>
        /// Asynchronously validates a New-Commerce migration.
        /// </summary>
        /// <param name="newCommerceMigration">The New-Commerece migration to be validated.</param>
        /// <returns>The New-Commerce migration eligibility.</returns>
        Task<NewCommerceEligibility> ValidateAsync(NewCommerceMigration newCommerceMigration);

        /// <summary>
        /// Gets all the New-Commerce migrations for the given search request.
        /// </summary>
        /// <param name="customerTenantId">The customer tenant id.</param>
        /// <param name="currentSubscriptionId">The current subscription id.</param>
        /// <param name="externalReferenceId">The external reference id.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="continuationToken">The continuation token.</param>
        /// <returns>The New-Commerce migrations and Continuation Token.</returns>
        GetNewCommerceMigrationsResponse Get(string customerTenantId, string currentSubscriptionId, string externalReferenceId, int? pageSize = null, string continuationToken = null);

        /// <summary>
        /// Asynchronously gets all the New-Commerce migrations for the given search request.
        /// </summary>
        /// <param name="customerTenantId">The customer tenant id.</param>
        /// <param name="currentSubscriptionId">The current subscription id.</param>
        /// <param name="externalReferenceId">The external reference id.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="continuationToken">The continuation token.</param>
        /// <returns>The New-Commerce migrations and Continuation Token.</returns>
        Task<GetNewCommerceMigrationsResponse> GetAsync(string customerTenantId, string currentSubscriptionId, string externalReferenceId, int? pageSize = null, string continuationToken = null);

        /// <summary>
        /// Gets the collection of migration events associated to a New-Commerce migration ID or subscription ID.
        /// </summary>
        /// <param name="newCommerceMigrationId">The New-Commerce migration ID.</param>
        /// <param name="currentSubscriptionId">The current subscription ID.</param>
        /// <returns>The collection of New-Commerce migration events.</returns>
        IEnumerable<NewCommerceMigrationEvent> GetEvents(string newCommerceMigrationId, string currentSubscriptionId);

        /// <summary>
        /// Asynchronously gets the collection of migration events associated to a New-Commerce migration ID or subscription ID.
        /// </summary>
        /// <param name="newCommerceMigrationId">The New-Commerce migration ID.</param>
        /// <param name="currentSubscriptionId">The current subscription ID.</param>
        /// <returns>The collection of New-Commerce migration events.</returns>
        Task<IEnumerable<NewCommerceMigrationEvent>> GetEventsAsync(string newCommerceMigrationId, string currentSubscriptionId);
    }
}
