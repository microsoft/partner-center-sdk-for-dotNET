// -----------------------------------------------------------------------
// <copyright file="ISubscriptionRegistrationStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;

    /// <summary>
    /// Defines the operations available on a customer's subscription registration status.
    /// </summary>
    public interface ISubscriptionRegistrationStatus : IPartnerComponent<Tuple<string, string>>
    {
        /// <summary>
        /// Retrieves a subscription registration status.
        /// </summary>
        /// <returns>The subscription registration status details.</returns>
        SubscriptionRegistrationStatus Get();

        /// <summary>
        /// Asynchronously retrieves a subscription registration status.
        /// </summary>
        /// <returns>The subscription registration status details.</returns>
        Task<SubscriptionRegistrationStatus> GetAsync();
    }
}