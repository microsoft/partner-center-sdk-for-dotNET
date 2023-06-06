// -----------------------------------------------------------------------
// <copyright file="Reservation.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Entitlements
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class that represents a reservation.
    /// </summary>
    public class Reservation : ResourceBaseWithLinks<StandardResourceLinks>
    {
        /// <summary>
        /// Gets or sets DisplayName
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets reservation id.
        /// </summary>
        public string ReservationId { get; set; }

        /// <summary>
        /// Gets or sets Applied Scopes
        /// </summary>
        public IEnumerable<string> AppliedScopes { get; set; }

        /// <summary>
        /// Gets or sets scope type.
        /// </summary>
        public string ScopeType { get; set; }

        /// <summary>
        /// Gets or sets quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets expiry date time. 
        /// </summary>
        public DateTime ExpiryDateTime { get; set; }

        /// <summary>
        /// Gets or sets effective date time.
        /// </summary>
        public DateTime EffectiveDateTime { get; set; }

        /// <summary>
        /// Gets or sets provisioning state.
        /// </summary>
        public string ProvisioningState { get; set; }
    }
}
