// -----------------------------------------------------------------------
// <copyright file="ComplianceCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Compliance
{
    using System;

    /// <summary>
    /// The compliance collection operations implementation.
    /// </summary>
    internal class ComplianceCollectionOperations : BasePartnerComponent, IComplianceCollection
    {
        /// <summary>
        /// The agreement status operations.
        /// </summary>
        private readonly Lazy<IAgreementSignatureStatus> agreementSignatureStatus;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplianceCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public ComplianceCollectionOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
            this.agreementSignatureStatus = new Lazy<IAgreementSignatureStatus>(() => new AgreementSignatureStatusOperations(this.Partner));
        }

        /// <summary>
        /// Gets the operations available for the agreement signature status.
        /// </summary>
        public IAgreementSignatureStatus AgreementSignatureStatus => this.agreementSignatureStatus.Value;
    }
}
