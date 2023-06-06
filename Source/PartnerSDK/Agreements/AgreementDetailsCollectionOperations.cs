// -----------------------------------------------------------------------
// <copyright file="AgreementDetailsCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Agreements
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models;
    using Models.Agreements;
    using Network;
    using Utilities;

    /// <summary>
    /// Agreement details collection operations implementation class.
    /// </summary>
    internal class AgreementDetailsCollectionOperations : BasePartnerComponent, IAgreementDetailsCollection
    {
        /// <summary>
        /// The agreement type to filter the result set on.
        /// </summary>
        private readonly string agreementTypeFilter;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgreementDetailsCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="agreementType">(Optional) The type of the agreement to filter on.</param>
        public AgreementDetailsCollectionOperations(IPartner rootPartnerOperations, string agreementType = null) : base(rootPartnerOperations)
        {
            this.agreementTypeFilter = agreementType;
        }

        /// <inheritdoc/>
        public ResourceCollection<AgreementMetaData> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<AgreementMetaData>>(() => this.GetAsync());
        }

        /// <inheritdoc/>
        public async Task<ResourceCollection<AgreementMetaData>> GetAsync()
        {
            var resourcePath = string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetAgreementsDetails.Path, this.Context);

            var partnerApiServiceProxy = new PartnerServiceProxy<AgreementMetaData, ResourceCollection<AgreementMetaData>>(this.Partner, resourcePath);
            if (!string.IsNullOrWhiteSpace(this.agreementTypeFilter))
            {
                partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>("agreementType", this.agreementTypeFilter));
            }

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <inheritdoc/>
        public IAgreementDetailsCollection ByAgreementType(string agreementType)
        {
            ParameterValidator.Required(agreementType, $"{nameof(agreementType)} is required.");

            return new AgreementDetailsCollectionOperations(this.Partner, agreementType);
        }
    }
}
