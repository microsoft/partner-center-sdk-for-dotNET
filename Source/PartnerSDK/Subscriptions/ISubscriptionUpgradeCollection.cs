// -----------------------------------------------------------------------
// <copyright file="ISubscriptionUpgradeCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Subscriptions;

    /// <summary>
    /// This interface defines the upgrade operations available on a customer's subscription.
    /// </summary>
    public interface ISubscriptionUpgradeCollection : IPartnerComponent<Tuple<string, string>>, IEntireEntityCollectionRetrievalOperations<Upgrade, ResourceCollection<Upgrade>>, IEntityCreateOperations<Upgrade, UpgradeResult>
    {
        /// <summary>
        /// Submits a subscription upgrade.
        /// </summary>
        /// <param name="upgrade">The new subscription upgrade information.</param>
        /// <returns>The subscription upgrade results.</returns>
        new UpgradeResult Create(Upgrade upgrade);

        /// <summary>
        /// Asynchronously submits a subscription upgrade.
        /// </summary>
        /// <param name="upgrade">The new subscription upgrade information.</param>
        /// <returns>The subscription upgrade results.</returns>
        new Task<UpgradeResult> CreateAsync(Upgrade upgrade);

        /// <summary>
        /// Retrieves all subscription upgrades.
        /// </summary>
        /// <returns>The subscription upgrades.</returns>
        new ResourceCollection<Upgrade> Get();

        /// <summary>
        /// Asynchronously retrieves all subscription upgrades.
        /// </summary>
        /// <returns>The subscription upgrades.</returns>
        new Task<ResourceCollection<Upgrade>> GetAsync();
    }
}