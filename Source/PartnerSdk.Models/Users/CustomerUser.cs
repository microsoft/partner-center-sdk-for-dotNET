﻿// -----------------------------------------------------------------------
// <copyright file="CustomerUser.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Users
{
    /// <summary>
    /// Entity to define customer user
    /// </summary>
    public sealed class CustomerUser : User
    {
        /// <summary>
        /// Gets or sets usage location, the location where user intends to use the license.
        /// </summary>
        public string UsageLocation { get; set; }
    }
}