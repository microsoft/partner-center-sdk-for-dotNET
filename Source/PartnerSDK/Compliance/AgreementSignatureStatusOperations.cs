// -----------------------------------------------------------------------
// <copyright file="AgreementSignatureStatusOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Compliance
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.Compliance;
    using Microsoft.Store.PartnerCenter.Network;
    using Microsoft.Store.PartnerCenter.Utilities;

    /// <summary>
    /// Class which contains operations for Agreement signature status.
    /// </summary>
    internal class AgreementSignatureStatusOperations : BasePartnerComponent, IAgreementSignatureStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgreementSignatureStatusOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public AgreementSignatureStatusOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Retrieves the agreement signature status by MPN Id or Tenant Id.
        /// </summary>
        /// <param name="mpnId">The MPN Id.</param>
        /// <param name="tenantId">The Tenant Id.</param>
        /// <returns>The Agreement signature status</returns>
        public AgreementSignatureStatus Get(string mpnId = null, string tenantId = null)
        {
            return PartnerService.Instance.SynchronousExecute<AgreementSignatureStatus>(() => this.GetAsync(mpnId, tenantId));
        }

        /// <summary>
        /// Asynchronously retrieves the agreement signature status by MPN Id or Tenant Id.
        /// </summary>
        /// <param name="mpnId">The MPN Id.</param>
        /// <param name="tenantId">The Tenant Id.</param>
        /// <returns>The Agreement signature status</returns>
        public async Task<AgreementSignatureStatus> GetAsync(string mpnId = null, string tenantId = null)
        {
            var partnerServiceProxy = new PartnerServiceProxy<AgreementSignatureStatus, AgreementSignatureStatus>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetAgreementSignatureStatus.Path);

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetAgreementSignatureStatus.Parameters.MpnId, mpnId));
            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetAgreementSignatureStatus.Parameters.TenantId, tenantId));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
