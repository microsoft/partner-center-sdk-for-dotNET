// -----------------------------------------------------------------------
// <copyright file="IAgreementSignatureStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Compliance
{
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.Compliance;

    /// <summary>
    /// Encapsulates agreement signature status of a partner
    /// </summary>
    public interface IAgreementSignatureStatus : IPartnerComponent
    {
        /// <summary>
        /// Retrieves the agreement signature status by MPN Id or Tenant Id.
        /// </summary>
        /// <param name="mpnId">The MPN Id.</param>
        /// <param name="tenantId">The Tenant Id.</param>
        /// <returns>The Agreement signature status</returns>
        AgreementSignatureStatus Get(string mpnId = null, string tenantId = null);

        /// <summary>
        /// Asynchronously retrieves the agreement signature status by MPN Id or Tenant Id.
        /// </summary>
        /// <param name="mpnId">The MPN Id.</param>
        /// <param name="tenantId">The Tenant Id.</param>
        /// <returns>The Agreement signature status</returns>
        Task<AgreementSignatureStatus> GetAsync(string mpnId = null, string tenantId = null);
    }
}
