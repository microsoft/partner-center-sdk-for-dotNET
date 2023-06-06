// -----------------------------------------------------------------------
// <copyright file="Artifact.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Entitlements
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    
    /// <summary>
    /// Class that represents an entitlement artifact.
    /// </summary>
    [JsonConverter(typeof(JsonConverters.ArtifactConverter))]
    public class Artifact : ResourceBaseWithLinks<StandardResourceLinks>
    {
        /// <summary>
        /// Gets or sets Artifact Type.
        /// </summary>
        public string ArtifactType { get; set; }

        /// <summary>
        /// Gets or sets the dynamic attributes.
        /// </summary>
        public Dictionary<string, object> DynamicAttributes { get; set; }
    }
}
