// -----------------------------------------------------------------------
// <copyright file="AgreementSignatureStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Compliance
{
    /// <summary>
    /// Represent the Agreement signature status
    /// </summary>
    public sealed class AgreementSignatureStatus
    {
        /// <summary>
        /// Gets or sets a value indicating whether agreement is signed.
        /// </summary>
        public bool IsAgreementSigned { get; set; }
    }
}
