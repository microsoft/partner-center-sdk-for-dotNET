// -----------------------------------------------------------------------
// <copyright file="ISubscriptionTransitionEligibilityCollection.cs" company="Microsoft Corporation">
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
    /// This interface defines the transition eligibility operations available on a customer's subscription.
    /// </summary>
    public interface ISubscriptionTransitionEligibilityCollection : IPartnerComponent<Tuple<string, string>>, IEntireEntityCollectionRetrievalOperations<TransitionEligibility, ResourceCollection<TransitionEligibility>>
    {
        /// <summary>
        /// Retrieves all transition eligibilities.
        /// </summary>
        /// <param name="eligibilityType">The eligibility type (none/immediate/scheduled).</param>
        /// <returns>The transition eligibilities.</returns>
        ResourceCollection<TransitionEligibility> Get(string eligibilityType);

        /// <summary>
        /// Asynchronously retrieves all transition eligibilities.
        /// </summary>
        /// <param name="eligibilityType">The eligibility type (none/immediate/scheduled).</param>
        /// <returns>The transition eligibilities.</returns>
        Task<ResourceCollection<TransitionEligibility>> GetAsync(string eligibilityType);
    }
}