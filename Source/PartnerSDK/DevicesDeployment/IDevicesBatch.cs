﻿// -----------------------------------------------------------------------
// <copyright file="IDevicesBatch.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.DevicesDeployment
{
    using System;
    using Microsoft.Store.PartnerCenter.GenericOperations;

    /// <summary>
    /// Represents the operations that can be done on the partner's devices batches.
    /// </summary>
    public interface IDevicesBatch : IPartnerComponent<Tuple<string, string>>
    {
        /// <summary>
        /// Obtains the Devices behavior of the devices batch.
        /// </summary>
        IDeviceCollection Devices { get; }
    }
}