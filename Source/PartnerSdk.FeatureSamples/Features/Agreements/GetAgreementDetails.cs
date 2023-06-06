// -----------------------------------------------------------------------
// <copyright file="GetAgreementDetails.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Agreements
{
    using System;
    using System.Collections.Generic;
    using Models.Agreements;

    /// <summary>
    /// Showcases getting the list of agreement details.
    /// </summary>
    internal class GetAgreementDetails : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get agreement details."; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // Get all agreements.
            var agreementDetails = partnerOperations.AgreementDetails.ByAgreementType("*").Get();
            Console.Out.WriteLine("All agreements: ");
            Display(agreementDetails.Items);

            // Get details of specific agreements.
            var microsoftCloudAgreementDetails = partnerOperations.AgreementDetails.ByAgreementType("MicrosoftCloudAgreement").Get();
            Console.Out.WriteLine("Microsoft Cloud Agreement details: ");
            Display(microsoftCloudAgreementDetails.Items);

            var microsoftCustomerAgreementDetails = partnerOperations.AgreementDetails.ByAgreementType("MicrosoftCustomerAgreement").Get();
            Console.Out.WriteLine("Microsoft Customer Agreement details: ");
            Display(microsoftCustomerAgreementDetails.Items);
        }

        /// <summary>
        /// Convenience method to display agreement details.
        /// </summary>
        /// <param name="agreements">The list of agreements to display.</param>
        private static void Display(IEnumerable<AgreementMetaData> agreements)
        {
            foreach (var agreement in agreements)
            {
                Console.Out.WriteLine("Agreement type: {0}", agreement.AgreementType);
                Console.Out.WriteLine("Link: {0}", agreement.AgreementLink);
                Console.Out.WriteLine("Template Id: {0}", agreement.TemplateId);
                Console.Out.WriteLine("Version Rank: {0}", agreement.VersionRank);
                Console.Out.WriteLine();
            }
        }
    }
}
