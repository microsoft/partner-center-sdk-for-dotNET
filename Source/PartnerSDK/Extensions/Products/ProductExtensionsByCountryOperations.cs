// -----------------------------------------------------------------------
// <copyright file="ProductExtensionsByCountryOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Extensions.Products
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models.Products;
    using Network;
    using Utilities;

    /// <summary>
    /// Product extensions operations implementation by country.
    /// </summary>
    internal class ProductExtensionsByCountryOperations : BasePartnerComponent, IProductExtensionsByCountry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductExtensionsByCountryOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="country">The country on which to base the corresponding products.</param>
        public ProductExtensionsByCountryOperations(IPartner rootPartnerOperations, string country) :
            base(rootPartnerOperations, country)
        {
            ParameterValidator.ValidateCountryCode(country);
        }

        /// <inheritdoc/>
        public IEnumerable<InventoryItem> CheckInventory(InventoryCheckRequest checkRequest)
        {
            return PartnerService.Instance.SynchronousExecute<IEnumerable<InventoryItem>>(() => this.CheckInventoryAsync(checkRequest));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<InventoryItem>> CheckInventoryAsync(InventoryCheckRequest checkRequest)
        {
            var partnerServiceProxy = new PartnerServiceProxy<InventoryCheckRequest, IEnumerable<InventoryItem>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.CheckInventory.Path));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.CheckInventory.Parameters.Country, this.Context));

            return await partnerServiceProxy.PostAsync(checkRequest);
        }
    }
}
