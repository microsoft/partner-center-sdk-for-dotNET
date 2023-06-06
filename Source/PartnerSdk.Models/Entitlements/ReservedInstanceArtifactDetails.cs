// -----------------------------------------------------------------------
// <copyright file="ReservedInstanceArtifactDetails.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Entitlements
{
    using System.Collections.Generic;

    /// <summary>
    /// Class that represents reserved instance artifact details.
    /// </summary>
    public class ReservedInstanceArtifactDetails : ResourceBaseWithLinks<StandardResourceLinks>
    {
        /// <summary>
        /// Gets or sets artifact type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets reservation collection.
        /// </summary>
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
