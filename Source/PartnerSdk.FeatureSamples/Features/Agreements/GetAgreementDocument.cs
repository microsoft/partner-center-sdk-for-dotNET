// -----------------------------------------------------------------------
// <copyright file="GetAgreementDocument.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Agreements
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Agreements;

    /// <summary>
    /// Showcases getting an agreement document.
    /// </summary>
    internal class GetAgreementDocument : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get agreement document."; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // Get Microsoft Customer Agreement document (for different language/locales and countries).
            var microsoftCustomerAgreementDetails = partnerOperations.AgreementDetails.ByAgreementType("MicrosoftCustomerAgreement").Get().Items.Single();

            var agreementTemplates = partnerOperations.AgreementTemplates.ById(microsoftCustomerAgreementDetails.TemplateId);

            var agreementDocument = agreementTemplates.Document.Get();
            Display(agreementDocument);

            agreementDocument = agreementTemplates.Document.ByLanguage("de-DE").Get();
            Display(agreementDocument);

            agreementDocument = agreementTemplates.Document.ByLanguage("de-DE").ByCountry("CA").Get();
            Display(agreementDocument);

            agreementDocument = agreementTemplates.Document.ByCountry("KO").ByLanguage("ja-JP").Get();
            Display(agreementDocument);
        }

        /// <summary>
        /// Convenience method to display the details of an agreement document.
        /// </summary>
        /// <param name="agreementDocument">The agreement document details to display.</param>
        private static void Display(AgreementDocument agreementDocument)
        {
            Console.Out.WriteLine("Display Uri: {0}", agreementDocument.DisplayUri);
            Console.Out.WriteLine("Download Uri: {0}", agreementDocument.DownloadUri);
            Console.Out.WriteLine("Language: {0}", agreementDocument.Language);
            Console.Out.WriteLine("Country: {0}", agreementDocument.Country);
            Console.Out.WriteLine();
        }
    }
}
