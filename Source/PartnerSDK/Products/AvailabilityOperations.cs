// -----------------------------------------------------------------------
// <copyright file="AvailabilityOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Models.Products;
    using Network;
    using Utilities;

    /// <summary>
    /// Single availability operations implementation.
    /// </summary>
    internal class AvailabilityOperations : BasePartnerComponent<Tuple<string, string, string, string>>, IAvailability
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvailabilityOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="productId">The corresponding product id.</param>
        /// <param name="skuId">The corresponding sku id.</param>
        /// <param name="availabilityId">The availability id.</param>
        /// <param name="country">The country on which to base the availability.</param>
        public AvailabilityOperations(IPartner rootPartnerOperations, string productId, string skuId, string availabilityId, string country) :
            base(rootPartnerOperations, new Tuple<string, string, string, string>(productId, skuId, availabilityId, country))
        {
            ParameterValidator.Required(productId, "productId has to be set.");
            ParameterValidator.Required(skuId, "skuId has to be set.");
            ParameterValidator.Required(availabilityId, "availabilityId has to be set.");
            ParameterValidator.ValidateCountryCode(country);
        }

        /// <inheritdoc/>
        public Availability Get()
        {
            return PartnerService.Instance.SynchronousExecute<Availability>(() => this.GetAsync());
        }

        /// <inheritdoc/>
        public async Task<Availability> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Availability, Availability>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetAvailability.Path, this.Context.Item1, this.Context.Item2, this.Context.Item3));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(PartnerService.Instance.Configuration.Apis.GetAvailability.Parameters.Country, this.Context.Item4));

            return await partnerServiceProxy.GetAsync();
        }
    }
}
