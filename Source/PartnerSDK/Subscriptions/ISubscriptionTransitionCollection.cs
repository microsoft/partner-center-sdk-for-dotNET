// -----------------------------------------------------------------------
// <copyright file="ISubscriptionTransitionCollection.cs" company="Microsoft Corporation">
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
    /// This interface defines the transition operations available on a customer's subscription.
    /// </summary>
    public interface ISubscriptionTransitionCollection : IPartnerComponent<Tuple<string, string>>, IEntireEntityCollectionRetrievalOperations<Transition, ResourceCollection<Transition>>, IEntityCreateOperations<Transition, Transition>
    {
        /// <summary>
        /// Submits a subscription transition.
        /// </summary>
        /// <param name="transition">The new subscription transition information.</param>
        /// <returns>The subscription transition result.</returns>
        new Transition Create(Transition transition);

        /// <summary>
        /// Asynchronously submits a subscription transition.
        /// </summary>
        /// <param name="transition">The new subscription transition information.</param>
        /// <returns>The subscription transition result.</returns>
        new Task<Transition> CreateAsync(Transition transition);

        /// <summary>
        /// Retrieves all subscription transitions.
        /// </summary>
        /// <returns>The subscription transitions.</returns>
        new ResourceCollection<Transition> Get();

        /// <summary>
        /// Asynchronously retrieves all subscription transitions.
        /// </summary>
        /// <returns>The subscription transitions.</returns>
        new Task<ResourceCollection<Transition>> GetAsync();

        /// <summary>
        /// Retrieves all subscription transitions matching the operation id.
        /// </summary>
        /// <param name="operationId">The operation id of the transition being queried for.</param>
        /// <returns>The subscription transitions.</returns>
        ResourceCollection<Transition> Get(string operationId);

        /// <summary>
        /// Asynchronously retrieves all subscription transitions matching the operation id.
        /// </summary>
        /// <param name="operationId">The operation id of the transition being queried for.</param>
        /// <returns>The subscription transitions.</returns>
        Task<ResourceCollection<Transition>> GetAsync(string operationId);
    }
}