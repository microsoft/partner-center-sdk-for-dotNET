// -----------------------------------------------------------------------
// <copyright file="ProductUpgradesOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.ProductUpgrades
{
    using System;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.ProductUpgrades;
    using Microsoft.Store.PartnerCenter.Network;
    using Microsoft.Store.PartnerCenter.Utilities;

    /// <summary>
    /// The operations for product upgrades
    /// </summary>
    internal class ProductUpgradesOperations : BasePartnerComponent, IProductUpgrades
    {
        /// <summary>
        /// The upgrade id.
        /// </summary>
        private readonly string upgradeId;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductUpgradesOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="upgradeId">The upgrade id.</param>
        public ProductUpgradesOperations(IPartner rootPartnerOperations, string upgradeId)
            : base(rootPartnerOperations, upgradeId)
        {
            if (string.IsNullOrWhiteSpace(upgradeId))
            {
                throw new ArgumentException("upgradeId must be set");
            }
            else
            {
                this.upgradeId = upgradeId;
            }
        }

        /// <summary>
        /// Checks the status for a customer for product upgrade
        /// </summary>
        /// <param name="productUpgradesRequest">The product upgrade request body.</param>
        /// <returns>The eligibility object.</returns>
        public ProductUpgradesStatus CheckStatus(ProductUpgradesRequest productUpgradesRequest)
        {
            return PartnerService.Instance.SynchronousExecute<ProductUpgradesStatus>(() => this.CheckStatusAsync(productUpgradesRequest));
        }

        /// <summary>
        /// Asynchronously checks the status for a customer for product upgrade
        /// </summary>
        /// <param name="productUpgradesRequest">The product upgrade request body.</param>
        /// <returns>The cart checkout result.</returns>
        public async Task<ProductUpgradesStatus> CheckStatusAsync(ProductUpgradesRequest productUpgradesRequest)
        {
            ParameterValidator.Required(productUpgradesRequest, "productUpgradesRequest can't be null");

            var partnerApiServiceProxy = new PartnerServiceProxy<ProductUpgradesRequest, ProductUpgradesStatus>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetProductUpgradeStatus.Path, this.upgradeId));

            return await partnerApiServiceProxy.PostAsync(productUpgradesRequest);
        }
    }
}