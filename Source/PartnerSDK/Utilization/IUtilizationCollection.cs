﻿// -----------------------------------------------------------------------
// <copyright file="IUtilizationCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Utilization
{
    using System;

    /// <summary>
    /// Groups subscription utilization behavior.
    /// </summary>
    public interface IUtilizationCollection : IPartnerComponent<Tuple<string, string>>
    {
        /// <summary>
        /// Gets Azure subscription utilization behavior.
        /// </summary>
        IAzureUtilizationCollection Azure { get; }
    }
}
