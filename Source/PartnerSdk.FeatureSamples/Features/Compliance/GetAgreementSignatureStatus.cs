// -----------------------------------------------------------------------
// <copyright file="GetAgreementSignatureStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Store.PartnerCenter.Models.Compliance;

    /// <summary>
    /// Showcases partner network profile.
    /// </summary>
    internal class GetAgreementSignatureStatus : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Agreement Signature Status"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            string selectedMpnId = state[FeatureSamplesApplication.SelectedMpnId] as string;
            Console.Out.WriteLine("Get agreement signature status by MPN Id");
            Console.Out.WriteLine("Mpn Id {0}", selectedMpnId);

            AgreementSignatureStatus agreementSignatureStatus = partnerOperations.Compliance.AgreementSignatureStatus.Get(mpnId: selectedMpnId);
            Console.Out.WriteLine("Is agreement signed : {0}", agreementSignatureStatus.IsAgreementSigned);
            Console.Out.WriteLine();

            string selectedTenantId = state[FeatureSamplesApplication.SelectedTenantId] as string;
            Console.Out.WriteLine("Get agreement signature status by tenant Id");
            Console.Out.WriteLine("Tenant Id {0}", selectedTenantId);

            agreementSignatureStatus = partnerOperations.Compliance.AgreementSignatureStatus.Get(tenantId: selectedTenantId);
            Console.Out.WriteLine("Is agreement signed : {0}", agreementSignatureStatus.IsAgreementSigned);
        }
    }
}
