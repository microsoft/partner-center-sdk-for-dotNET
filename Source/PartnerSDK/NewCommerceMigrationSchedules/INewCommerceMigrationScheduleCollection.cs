// -----------------------------------------------------------------------
// <copyright file="INewCommerceMigrationScheduleCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.NewCommerceMigrationSchedules
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.GenericOperations;
    using Microsoft.Store.PartnerCenter.Models.NewCommerceMigrationSchedules;

    /// <summary>
    /// Encapsulates the collection behavior for a New-Commerce migration schedule.
    /// </summary>
    public interface INewCommerceMigrationScheduleCollection : IPartnerComponent, IEntityCreateOperations<NewCommerceMigrationSchedule>, IEntitySelector<INewCommerceMigrationSchedule>
    {
        /// <summary>
        /// Gets a specific New-Commerce migration schedule behavior.
        /// </summary>
        /// <param name="newCommerceMigrationScheduleId">The New-Commerce migration schedule ID.</param>
        /// <returns>The New-Commerce migration schedule operations.</returns>
        new INewCommerceMigrationSchedule this[string newCommerceMigrationScheduleId] { get; }

        /// <summary>
        /// Gets a specific New-Commerce migration schedule behavior.
        /// </summary>
        /// <param name="newCommerceMigrationScheduleId">The New-Commerce migration schedule ID.</param>
        /// <returns>The New-Commerce migration schedule operations.</returns>
        new INewCommerceMigrationSchedule ById(string newCommerceMigrationScheduleId);

        /// <summary>
        /// Creates a New-Commerce migration schedule.
        /// </summary>
        /// <param name="newCommerceMigrationSchedule">A New-Commerece migration schedule to be created.</param>
        /// <returns>The New-Commerce migration schedule that was just created.</returns>
        new NewCommerceMigrationSchedule Create(NewCommerceMigrationSchedule newCommerceMigrationSchedule);

        /// <summary>
        /// Asynchronously creates a New-Commerce migration schedule.
        /// </summary>
        /// <param name="newCommerceMigrationSchedule">A New-Commerece migration schedule to be created.</param>
        /// <returns>The New-Commerce migration schedule that was just created.</returns>
        new Task<NewCommerceMigrationSchedule> CreateAsync(NewCommerceMigrationSchedule newCommerceMigrationSchedule);

        /// <summary>
        /// Gets all the New-Commerce migration schedules for the given search request.
        /// </summary>
        /// <param name="customerTenantId">The customer tenant id.</param>
        /// <param name="currentSubscriptionId">The current subscription id.</param>
        /// <param name="externalReferenceId">The external reference id.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="continuationToken">The continuation token.</param>
        /// <returns>The New-Commerce migration schedules and Continuation Token.</returns>
        (IEnumerable<NewCommerceMigrationSchedule> NewCommerceMigrationSchedules, string ContinuationToken) Get(string customerTenantId = null, string currentSubscriptionId = null, string externalReferenceId = null, int? pageSize = null, string continuationToken = null);

        /// <summary>
        /// Asynchronously gets all the New-Commerce migration schedules for the given search request.
        /// </summary>
        /// <param name="customerTenantId">The customer tenant id.</param>
        /// <param name="currentSubscriptionId">The current subscription id.</param>
        /// <param name="externalReferenceId">The external reference id.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="continuationToken">The continuation token.</param>
        /// <returns>The New-Commerce migration schedules and Continuation Token.</returns>
        Task<(IEnumerable<NewCommerceMigrationSchedule> NewCommerceMigrationSchedules, string ContinuationToken)> GetAsync(string customerTenantId = null, string currentSubscriptionId = null, string externalReferenceId = null, int? pageSize = null, string continuationToken = null);
    }
}