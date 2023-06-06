// -----------------------------------------------------------------------
// <copyright file="EstimateCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using Utilities;

    /// <summary>
    /// Defines the operations available for estimate collection.
    /// </summary>
    internal class EstimateCollectionOperations : BasePartnerComponent, IEstimateCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EstimateCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public EstimateCollectionOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Gets an estimate links operations.
        /// </summary>
        public IEstimateLink Links
        {
            get
            {
                return new EstimateLinkOperations(this.Partner);
            }
        }
    }
}
