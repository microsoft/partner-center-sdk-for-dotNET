// -----------------------------------------------------------------------
// <copyright file="AvailabilityCollectionByReservationScopeOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Models.Products;
    using Network;
    using Utilities;

    /// <summary>
    /// Availabilities implementation class.
    /// </summary>
    internal class AvailabilityCollectionByReservationScopeOperations : BasePartnerComponent<Tuple<string, string, string, string>>, IAvailabilityCollectionByReservationScopeOperations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvailabilityCollectionByReservationScopeOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="productId">The corresponding product id.</param>
        /// <param name="skuId">The corresponding sku id.</param>
        /// <param name="country">The country on which to base the product.</param>
        /// <param name="reservationScope">The reservation scope used for filtering the availabilities.</param>
        public AvailabilityCollectionByReservationScopeOperations(IPartner rootPartnerOperations, string productId, string skuId, string country, string reservationScope) :
            base(rootPartnerOperations, new Tuple<string, string, string, string>(productId, skuId, country, reservationScope))
        {
            ParameterValidator.Required(productId, "productId must be set");
            ParameterValidator.Required(skuId, "skuId must be set");
            ParameterValidator.ValidateCountryCode(country);
            ParameterValidator.Required(reservationScope, "reservationScope must be set");
        }

        /// <inheritdoc/>
        public ResourceCollection<Availability> Get()
        {
            return PartnerService.Instance.SynchronousExecute<ResourceCollection<Availability>>(() => this.GetAsync());
        }

        /// <inheritdoc/>
        public async Task<ResourceCollection<Availability>> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<Availability, ResourceCollection<Availability>>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.GetAvailabilities.Path, this.Context.Item1, this.Context.Item2));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetAvailabilities.Parameters.Country, this.Context.Item3));

            partnerServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetAvailabilities.Parameters.ReservationScope, this.Context.Item4));

            return await partnerServiceProxy.GetAsync();
        }
    }
}