// -----------------------------------------------------------------------
// <copyright file="AgreementTemplateCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Agreements
{
    /// <summary>
    /// Agreement template collection operations implementation class.
    /// </summary>
    internal class AgreementTemplateCollectionOperations : BasePartnerComponent, IAgreementTemplateCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgreementTemplateCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public AgreementTemplateCollectionOperations(IPartner rootPartnerOperations) : base(rootPartnerOperations)
        {
        }

        /// <inheritdoc/>
        public IAgreementTemplate this[string id]
        {
            get
            {
                return this.ById(id);
            }
        }

        /// <inheritdoc/>
        public IAgreementTemplate ById(string id)
        {
            return new AgreementTemplateOperations(this.Partner, id);
        }
    }
}
