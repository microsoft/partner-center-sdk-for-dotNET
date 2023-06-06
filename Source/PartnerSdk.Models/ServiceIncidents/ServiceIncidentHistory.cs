//----------------------------------------------------------------
// <copyright file="ServiceIncidentHistory.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.ServiceIncidents
{
    using System;

    /// <summary>
    /// Represents the message history for service incident.
    /// </summary>
    public sealed class ServiceIncidentHistory
    {
        /// <summary>
        /// Gets or sets the published time.
        /// </summary>
        public DateTime PublishedTime { get; set; }

        /// <summary>
        /// Gets or sets the Message text.
        /// </summary>
        public string MessageText { get; set; }
    }
}
