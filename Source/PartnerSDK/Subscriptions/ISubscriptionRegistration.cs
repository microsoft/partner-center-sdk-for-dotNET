// -----------------------------------------------------------------------
// <copyright file="ISubscriptionRegistration.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the operations available on a customer's subscription registrations.
    /// </summary>
    public interface ISubscriptionRegistration : IPartnerComponent<Tuple<string, string>>
    {
        /// <summary>
        /// Register a subscription to enable Azure Reserved instance purchase.
        /// </summary>
        /// <returns>The location which indicates the URL of the API to query for status.</returns>
        string Register();

        /// <summary>
        /// Asynchronously register a subscription to enable Azure Reserved instance purchase.
        /// </summary>
        /// <returns>The location which indicates the URL of the API to query for status.</returns>
        Task<string> RegisterAsync();
    }
}