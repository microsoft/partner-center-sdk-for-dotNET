//----------------------------------------------------------------
// <copyright file="DocumentType.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Invoices
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Different providers of billing information.
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum DocumentType
    {
        /// <summary>
        /// Value if not set.
        /// </summary>
        None,

        /// <summary>
        /// Indicates that document type is an invoice.
        /// </summary>
        Invoice,

        /// <summary>
        /// Indicates that document type is an void note.
        /// </summary>
        VoidNote,

        /// <summary>
        /// Indicates that document type is an adjustment note.
        /// </summary>
        AdjustmentNote
    }
}
