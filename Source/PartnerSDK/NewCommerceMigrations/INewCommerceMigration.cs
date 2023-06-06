// -----------------------------------------------------------------------
// <copyright file="INewCommerceMigration.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.NewCommerceMigrations
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.GenericOperations;
    using Microsoft.Store.PartnerCenter.Models.NewCommerceMigrations;

    /// <summary>
    /// Encapsulates the behavior of a New-Commerce migration.
    /// </summary>
    public interface INewCommerceMigration : IPartnerComponent<Tuple<string, string>>, IEntityGetOperations<NewCommerceMigration>
    {
        /// <summary>
        /// Gets a New-Commerce migration.
        /// </summary>
        /// <returns>The New-Commerce migration.</returns>
        new NewCommerceMigration Get();

        /// <summary>
        /// Asynchronously gets a New-Commerce migration.
        /// </summary>
        /// <returns>The New-Commerce migration.</returns>
        new Task<NewCommerceMigration> GetAsync();
    }
}
