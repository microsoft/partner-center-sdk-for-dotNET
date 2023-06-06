// -----------------------------------------------------------------------
// <copyright file="AgreementDocumentTransformOptions.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Agreements
{
    /// <summary>
    /// Contains transform options for an agreement document.
    /// </summary>
    public class AgreementDocumentTransformOptions
    {
        /// <summary>
        /// Gets or sets the language and locale of the agreement document.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the country of the agreement document.
        /// </summary>
        public string Country { get; set; }
    }
}
