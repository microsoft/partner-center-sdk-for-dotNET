// -----------------------------------------------------------------------
// <copyright file="ReservedInstanceArtifact.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Entitlements
{
    /// <summary>
    /// Class that represents reserved instance artifact.
    /// </summary>
    public class ReservedInstanceArtifact : Artifact
    {
        /// <summary>
        /// Gets or sets artifact link.
        /// </summary>
        public Link Link { get; set; }

        /// <summary>
        /// Gets or sets resourceId (reservation order id).
        /// </summary>
        public string ResourceId { get; set; }
    }
}
