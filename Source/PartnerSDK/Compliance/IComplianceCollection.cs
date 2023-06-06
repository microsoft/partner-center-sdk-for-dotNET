// -----------------------------------------------------------------------
// <copyright file="IComplianceCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Compliance
{
    /// <summary>
    /// Represents the compliance status of a partner.
    /// </summary>
    public interface IComplianceCollection : IPartnerComponent
    {
        /// <summary>
        /// Gets the operations available for agreement signature status.
        /// </summary>
        IAgreementSignatureStatus AgreementSignatureStatus { get; }
    }
}
