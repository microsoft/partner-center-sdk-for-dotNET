// -----------------------------------------------------------------------
// <copyright file="DeviceUploadStatusType.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------
namespace Microsoft.Store.PartnerCenter.Models.DevicesDeployment
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Devices Batch upload status.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum DeviceUploadStatusType
    {
        /// <summary>
        /// Should never happen.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Batch is queued.
        /// </summary>
        Queued = 1,

        /// <summary>
        /// Batch is processing.
        /// </summary>
        Processing = 2,

        /// <summary>
        /// Batch is complete.
        /// </summary>
        Finished = 3,

        /// <summary>
        /// Batch is complete with an error.
        /// </summary>
        FinishedWithErrors = 4
    }
}
