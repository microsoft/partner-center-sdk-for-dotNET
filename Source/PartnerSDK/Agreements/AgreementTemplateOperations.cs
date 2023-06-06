// -----------------------------------------------------------------------
// <copyright file="AgreementTemplateOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Agreements
{
    using System;

    /// <summary>
    /// Supports operations on agreement templates.
    /// </summary>
    internal class AgreementTemplateOperations : BasePartnerComponent<AgreementTemplateContext>, IAgreementTemplate
    {
        /// <summary>
        /// The operations for the agreement template's document.
        /// </summary>
        private readonly Lazy<IAgreementDocument> agreementDocument;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgreementTemplateOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="templateId">The template ID.</param>
        public AgreementTemplateOperations(IPartner rootPartnerOperations, string templateId) :
            base(rootPartnerOperations, new AgreementTemplateContext { TemplateId = templateId })
        {
            if (string.IsNullOrWhiteSpace(templateId))
            {
                throw new ArgumentException("templateId is required.");
            }

            this.agreementDocument = new Lazy<IAgreementDocument>(() => new AgreementDocumentOperations(rootPartnerOperations, templateId));
        }

        /// <inheritdoc/>
        public IAgreementDocument Document
        {
            get
            {
                return this.agreementDocument.Value;
            }
        }
    }
}
