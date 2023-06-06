// -----------------------------------------------------------------------
// <copyright file="IOverageCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.GenericOperations;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;

    /// <summary>
    /// This interface defines the overage operations available on customer's subscriptions.
    /// </summary>
    public interface IOverageCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<Overage, ResourceCollection<Overage>>, IEntityPutOperations<Overage>
    {
        /// <summary>
        /// Retrieves overage.
        /// </summary>
        /// <returns>The overage.</returns>
        new ResourceCollection<Overage> Get();

        /// <summary>
        /// Asynchronously retrieves overage.
        /// </summary>
        /// <returns>The overage.</returns>
        new Task<ResourceCollection<Overage>> GetAsync();

        /// <summary>
        /// Creates or updates overage.
        /// </summary>
        /// <param name="entity">The overage.</param>
        /// <returns>The updated overage.</returns>
        new Overage Put(Overage entity);

        /// <summary>
        /// Asynchronously creates or updates overage.
        /// </summary>
        /// <param name="entity">The overage.</param>
        /// <returns>The updated overage.</returns>
        new Task<Overage> PutAsync(Overage entity);
    }
}