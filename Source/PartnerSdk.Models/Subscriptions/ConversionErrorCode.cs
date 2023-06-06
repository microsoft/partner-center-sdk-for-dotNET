// -----------------------------------------------------------------------
// <copyright file="ConversionErrorCode.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    /// <summary>
    /// The type of errors that prevent trial subscription conversion from happening.
    /// </summary>
    public enum ConversionErrorCode
    {
        /// <summary>
        /// General error.
        /// </summary>
        Other = 0,

        /// <summary>
        /// Cannot find any conversions for the trial subscription to convert to.
        /// </summary>
        ConversionsNotFound = 1,
    }
}
