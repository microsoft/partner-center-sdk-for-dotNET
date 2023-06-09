﻿// -----------------------------------------------------------------------
// <copyright file="UserMember.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Roles
{
    /// <summary>
    /// Represents a user role member.
    /// </summary>
    public sealed class UserMember : ResourceBase
    {
        /// <summary>
        /// Gets or sets the id of the member.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the name of the user principal.
        /// </summary>
        public string UserPrincipalName { get; set; }
    }
}
