// <copyright file="IOffer.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Offers
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.Offers;

    /// <summary>
    /// Holds operations that can be performed on a single offer.
    /// </summary>
    public interface IOffer : IPartnerComponent<Tuple<string, string>>, IEntityGetOperations<Offer>
    {
        /// <summary>
        /// Gets the operations for the current offer's add-ons.
        /// </summary>
        IOfferAddOns AddOns { get; }

        /// <summary>
        /// Retrieves the offer information.
        /// </summary>
        /// <returns>The offer information.</returns>
        new Offer Get();

        /// <summary>
        /// Asynchronously retrieves the offer information.
        /// </summary>
        /// <returns>The offer information.</returns>
        new Task<Offer> GetAsync();
    }
}
