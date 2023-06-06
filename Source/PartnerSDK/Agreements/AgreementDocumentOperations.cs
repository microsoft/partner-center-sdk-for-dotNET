// -----------------------------------------------------------------------
// <copyright file="AgreementDocumentOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Agreements
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models.Agreements;
    using Network;
    using Utilities;

    /// <summary>
    /// Supports operations on an agreement document.
    /// </summary>
    internal class AgreementDocumentOperations : BasePartnerComponent<AgreementDocumentContext>, IAgreementDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgreementDocumentOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="templateId">The template ID.</param>
        /// <param name="language">(Optional) The language for the document.</param>
        /// <param name="country">(Optional) The country for the document.</param>
        public AgreementDocumentOperations(IPartner rootPartnerOperations, string templateId, string language = null, string country = null) :
            base(rootPartnerOperations, new AgreementDocumentContext { TemplateId = templateId })
        {
            if (string.IsNullOrWhiteSpace(templateId))
            {
                throw new ArgumentException($"{nameof(templateId)} is required.");
            }

            if (!string.IsNullOrWhiteSpace(language) || !string.IsNullOrWhiteSpace(country))
            {
                this.Context.TransformOptions = new AgreementDocumentTransformOptions
                {
                    Language = language,
                    Country = country
                };
            }
        }

        /// <inheritdoc/>
        public AgreementDocument Get()
        {
            return PartnerService.Instance.SynchronousExecute(() => this.GetAsync());
        }

        /// <inheritdoc/>
        public async Task<AgreementDocument> GetAsync()
        {
            var resourcePath = string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetAgreementDocument.Path, this.Context.TemplateId);

            var partnerApiServiceProxy = new PartnerServiceProxy<AgreementDocument, AgreementDocument>(this.Partner, resourcePath);

            if (!string.IsNullOrWhiteSpace(this.Context.TransformOptions?.Language))
            {
                partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(PartnerService.Instance.Configuration.Apis.GetAgreementDocument.Parameters.Language, this.Context.TransformOptions.Language));
            }

            if (!string.IsNullOrWhiteSpace(this.Context.TransformOptions?.Country))
            {
                partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(PartnerService.Instance.Configuration.Apis.GetAgreementDocument.Parameters.Country, this.Context.TransformOptions.Country));
            }

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <inheritdoc/>
        public IAgreementDocument ByLanguage(string language)
        {
            ParameterValidator.Required(language, $"{nameof(language)} is required.");

            return new AgreementDocumentOperations(this.Partner, this.Context.TemplateId, language, this.Context.TransformOptions?.Country);
        }

        /// <inheritdoc/>
        public IAgreementDocument ByCountry(string country)
        {
            ParameterValidator.Required(country, $"{nameof(country)} is required.");

            return new AgreementDocumentOperations(this.Partner, this.Context.TemplateId, this.Context.TransformOptions?.Language, country);
        }
    }
}
