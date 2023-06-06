// -----------------------------------------------------------------------
// <copyright file="EstimateLinkOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using Utilities;

    /// <summary>
    /// Represents the operations available on an estimate link.
    /// </summary>
    internal class EstimateLinkOperations : BasePartnerComponent, IEstimateLink
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EstimateLinkOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public EstimateLinkOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Get estimate link collection by currency
        /// </summary>
        /// <param name="currencyCode">The currency code</param>
        /// <returns>The estimate link collection by currency operations</returns>
        public IEstimateLinkCollectionByCurrency ByCurrency(string currencyCode)
        {
            return new EstimateLinkCollectionByCurrencyOperations(this.Partner, currencyCode);
        }
    }
}
