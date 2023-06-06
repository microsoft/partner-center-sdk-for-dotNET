// -----------------------------------------------------------------------
// <copyright file="INewCommerceMigrationSchedule.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.NewCommerceMigrationSchedules
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.GenericOperations;
    using Microsoft.Store.PartnerCenter.Models.NewCommerceMigrationSchedules;

    /// <summary>
    /// Encapsulates the behavior of a New-Commerce migration.
    /// </summary>
    public interface INewCommerceMigrationSchedule : IPartnerComponent<Tuple<string, string>>, IEntityGetOperations<NewCommerceMigrationSchedule>, IEntityUpdateOperations<NewCommerceMigrationSchedule>
    {
        /// <summary>
        /// Gets a New-Commerce migration schedule.
        /// </summary>
        /// <returns>The New-Commerce migration schedule.</returns>
        new NewCommerceMigrationSchedule Get();

        /// <summary>
        /// Asynchronously gets a New-Commerce migration schedule.
        /// </summary>
        /// <returns>The New-Commerce migration schedule.</returns>
        new Task<NewCommerceMigrationSchedule> GetAsync();

        /// <summary>
        /// Updates a New-Commerce migration schedule.
        /// </summary>
        /// <param name="newCommerceMigrationSchedule">A New-Commerece migration schedule to be updated.</param>
        /// <returns>The New-Commerce migration schedule that was updated.</returns>
        new NewCommerceMigrationSchedule Update(NewCommerceMigrationSchedule newCommerceMigrationSchedule);

        /// <summary>
        /// Asynchronously updates a New-Commerce migration schedule.
        /// </summary>
        /// <param name="newCommerceMigrationSchedule">A New-Commerece migration schedule to be updated.</param>
        /// <returns>The New-Commerce migration schedule that was updated.</returns>
        new Task<NewCommerceMigrationSchedule> UpdateAsync(NewCommerceMigrationSchedule newCommerceMigrationSchedule);

        /// <summary>
        /// Cancels a New-Commerce migration schedule.
        /// </summary>
        /// <returns>The New-Commerce migration schedule that was cancelled.</returns>
        NewCommerceMigrationSchedule Cancel();

        /// <summary>
        /// Asynchronously cancels a New-Commerce migration schedule.
        /// </summary>
        /// <returns>The New-Commerce migration schedule that was cancelled.</returns>
        Task<NewCommerceMigrationSchedule> CancelAsync();
    }
}