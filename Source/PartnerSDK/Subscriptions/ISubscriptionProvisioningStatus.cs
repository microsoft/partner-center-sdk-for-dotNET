// -----------------------------------------------------------------------
// <copyright file="ISubscriptionProvisioningStatus.cs" company="Microsoft Corporation">
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
    /// The subscription provisioning status..
    /// </summary>
    public interface ISubscriptionProvisioningStatus : IPartnerComponent<Tuple<string, string>>, IEntityGetOperations<SubscriptionProvisioningStatus>
    {
        /// <summary>
        /// Retrieves all subscription provisioning status details.
        /// </summary>
        /// <returns>The subscription provisioning status details.</returns>
        new SubscriptionProvisioningStatus Get();

        /// <summary>
        /// Asynchronously retrieves all subscription provisioning status details.
        /// </summary>
        /// <returns>The subscription provisioning status details.</returns>
        new Task<SubscriptionProvisioningStatus> GetAsync();
    }
}