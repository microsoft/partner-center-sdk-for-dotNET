//----------------------------------------------------------------
// <copyright file="PolicySettingsType.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.DevicesDeployment
{
    using System;
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the settings for an OOBE(Out of box experience) policy
    /// </summary>
    [Flags]
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum PolicySettingsType
    {
        /// <summary>
        /// Default value for policy settings.
        /// </summary>
        None = 0,

        /// <summary>
        /// Remove OEM Pre-installs.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem", Justification = "naming convention.")]
        RemoveOemPreinstalls = 1,

        /// <summary>
        /// OOBE(Out of box experience) user will not be a local admin on the configured device. 
        /// </summary>
        OobeUserNotLocalAdmin = 2,

        /// <summary>
        /// Skip express settings.
        /// </summary>
        SkipExpressSettings = 4,

        /// <summary>
        /// Skip OEM registration settings.
        /// </summary>
        SkipOemRegistration = 8,

        /// <summary>
        /// Skip EULA settings.
        /// </summary>
        SkipEula = 0x10
    }
}
