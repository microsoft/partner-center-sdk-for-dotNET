// <copyright file="ISubscriptionAddOnCollection.cs" company="Microsoft Corporation">
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
    /// Defines the behavior for a subscription's add-ons.
    /// </summary>
    public interface ISubscriptionAddOnCollection : IPartnerComponent<Tuple<string, string>>, IEntireEntityCollectionRetrievalOperations<Subscription, ResourceCollection<Subscription>>
    {
        /// <summary>
        /// Retrieves all subscription add-ons.
        /// </summary>
        /// <returns>The subscription add-ons.</returns>
        new ResourceCollection<Subscription> Get();

        /// <summary>
        /// Asynchronously retrieves all subscription add-ons.
        /// </summary>
        /// <returns>The subscription add-ons.</returns>
        new Task<ResourceCollection<Subscription>> GetAsync();
    }
}
