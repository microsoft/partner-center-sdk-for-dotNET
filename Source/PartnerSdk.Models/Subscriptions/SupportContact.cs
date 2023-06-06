// -----------------------------------------------------------------------
// <copyright file="SupportContact.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using Models;

    /// <summary>
    /// Represents a form of support contact for a customer's subscription.
    /// </summary>
    public sealed class SupportContact : ResourceBaseWithLinks<StandardResourceLinks>
    {
        /// <summary>
        /// Gets or sets the support tenant id.
        /// </summary>
        public string SupportTenantId { get; set; }

        /// <summary>
        /// Gets or sets the support MPN id.
        /// </summary>
        public string SupportMpnId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
