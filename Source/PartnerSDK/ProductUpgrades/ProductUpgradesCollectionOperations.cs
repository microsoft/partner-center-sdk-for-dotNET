// -----------------------------------------------------------------------
// <copyright file="ProductUpgradesCollectionOperations.cs" company="Microsoft Corporation">
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
    internal class ProductUpgradesCollectionOperations : BasePartnerComponent, IProductUpgradesCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductUpgradesCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public ProductUpgradesCollectionOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Gets a single product upgrades operations.
        /// </summary>
        /// <param name="upgradeId">The upgrade id.</param>
        /// <returns>The product upgrades operations.</returns>
        public IProductUpgrades ById(string upgradeId)
        {
            return new ProductUpgradesOperations(this.Partner, upgradeId);
        }

        /// <summary>
        /// Checks the eligibility for a customer for product upgrade
        /// </summary>
        /// <param name="productUpgradesRequest">The product upgrade request body.</param>
        /// <returns>The eligibility object.</returns>
        public ProductUpgradesEligibility CheckEligibility(ProductUpgradesRequest productUpgradesRequest)
        {
            return PartnerService.Instance.SynchronousExecute<ProductUpgradesEligibility>(() => this.CheckEligibilityAsync(productUpgradesRequest));
        }

        /// <summary>
        /// Asynchronously checks the eligibility for a customer for product upgrade
        /// </summary>
        /// <param name="productUpgradesRequest">The product upgrade request body.</param>
        /// <returns>The cart checkout result.</returns>
        public async Task<ProductUpgradesEligibility> CheckEligibilityAsync(ProductUpgradesRequest productUpgradesRequest)
        {
            ParameterValidator.Required(productUpgradesRequest, "productUpgradesRequest can't be null");

            var partnerApiServiceProxy = new PartnerServiceProxy<ProductUpgradesRequest, ProductUpgradesEligibility>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetProductUpgradeEligibility.Path, this.Context));

            return await partnerApiServiceProxy.PostAsync(productUpgradesRequest);
        }

        /// <summary>
        /// Upgrades product
        /// </summary>
        /// <param name="productUpgradesRequest">The product upgrade request body.</param>
        /// <returns>The eligibility object.</returns>
        public string Create(ProductUpgradesRequest productUpgradesRequest)
        {
            return PartnerService.Instance.SynchronousExecute<string>(() => this.CreateAsync(productUpgradesRequest));
        }

        /// <summary>
        /// Asynchronously upgrades product
        /// </summary>
        /// <param name="productUpgradesRequest">The product upgrade request body.</param>
        /// <returns>The cart checkout result.</returns>
        public async Task<string> CreateAsync(ProductUpgradesRequest productUpgradesRequest)
        {
            ParameterValidator.Required(productUpgradesRequest, "productUpgradesRequest can't be null");

            var partnerApiServiceProxy = new PartnerServiceProxy<ProductUpgradesRequest, HttpResponseMessage>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.UpgradeProduct.Path, this.Context));

            var response = await partnerApiServiceProxy.PostAsync(productUpgradesRequest);
            return response.Headers.Location != null ? response.Headers.Location.OriginalString : string.Empty;
        }
    }
}