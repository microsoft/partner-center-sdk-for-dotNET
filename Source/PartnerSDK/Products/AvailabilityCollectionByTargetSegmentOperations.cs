// -----------------------------------------------------------------------
// <copyright file="AvailabilityCollectionByTargetSegmentOperations.cs" company="Microsoft Corporation">
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
    internal class AvailabilityCollectionByTargetSegmentOperations : BasePartnerComponent<Tuple<string, string, string, string>>, IAvailabilityCollectionByTargetSegment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvailabilityCollectionByTargetSegmentOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="productId">The corresponding product id.</param>
        /// <param name="skuId">The corresponding sku id.</param>
        /// <param name="country">The country on which to base the product.</param>
        /// <param name="targetSegment">The target segment used for filtering the availabilities.</param>
        public AvailabilityCollectionByTargetSegmentOperations(IPartner rootPartnerOperations, string productId, string skuId, string country, string targetSegment) :
            base(rootPartnerOperations, new Tuple<string, string, string, string>(productId, skuId, country, targetSegment))
        {
            ParameterValidator.Required(productId, "productId must be set");
            ParameterValidator.Required(skuId, "skuId must be set");
            ParameterValidator.ValidateCountryCode(country);
            ParameterValidator.Required(targetSegment, "targetSegment must be set");
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
                PartnerService.Instance.Configuration.Apis.GetAvailabilities.Parameters.TargetSegment, this.Context.Item4));

            return await partnerServiceProxy.GetAsync();
        }

        /// <inheritdoc/>
        public IAvailabilityCollectionByTargetSegmentByReservationScopeOperations ByReservationScope(string reservationScope)
        {
            return new AvailabilityCollectionByTargetSegmentByReservationScopeOperations(this.Partner, this.Context.Item1, this.Context.Item2, this.Context.Item3, this.Context.Item4, reservationScope);
        }
    }
}