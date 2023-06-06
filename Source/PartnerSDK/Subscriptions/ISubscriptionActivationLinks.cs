// -----------------------------------------------------------------------
// <copyright file="ISubscriptionActivationLinks.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Orders;

    /// <summary>
    /// A resource collection of subscription activation links.
    /// </summary>
    public interface ISubscriptionActivationLinks : IPartnerComponent<Tuple<string, string>>, IEntityGetOperations<ResourceCollection<OrderLineItemActivationLink>>
    {
        /// <summary>
        /// Retrieves all activation links for a specific subscription.
        /// </summary>
        /// <returns>A resource collection of activation links.</returns>
        new ResourceCollection<OrderLineItemActivationLink> Get();

        /// <summary>
        /// Asynchronously retrieves a resource collection of subscription activation links.
        /// </summary>
        /// <returns>The  resource collection of subscription activation links.</returns>
        new Task<ResourceCollection<OrderLineItemActivationLink>> GetAsync();
    }
}