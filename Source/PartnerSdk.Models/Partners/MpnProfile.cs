// -----------------------------------------------------------------------
// <copyright file="MpnProfile.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Partners
{
    /// <summary>
    /// Represents a partner's partner network profile.
    /// </summary>
    public sealed class MpnProfile : ResourceBaseWithLinks<StandardResourceLinks>
    {
        /// <summary>
        /// Gets or sets the partner's organization name.
        /// </summary>
        public string PartnerName { get; set; }

        /// <summary>
        /// Gets or sets the partner's Microsoft Partner Network Id .
        /// </summary>
        public string MpnId { get; set; }
    }
}
