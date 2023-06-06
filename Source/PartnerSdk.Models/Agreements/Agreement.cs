// -----------------------------------------------------------------------
// <copyright file="Agreement.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Agreements
{
    using System;

    /// <summary>
    /// The class represents an agreement object.
    /// </summary>
    public class Agreement
    {
        /// <summary>
        /// Gets or sets the partner's user ID.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the primary contact for the agreement.
        /// </summary>
        public Contact PrimaryContact { get; set; }

        /// <summary>
        /// Gets or sets the template ID of the agreement.
        /// </summary>
        public string TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the date the agreement was signed.
        /// </summary>
        public DateTime DateAgreed { get; set; }

        /// <summary>
        /// Gets or sets the agreement type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the download link for the agreement.
        /// </summary>
        public Uri AgreementLink { get; set; }
    }
}
