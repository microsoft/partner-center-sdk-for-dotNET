// -----------------------------------------------------------------------
// <copyright file="IAvailability.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Products
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.Products;

    /// <summary>
    /// Holds operations that can be performed on a single availability.
    /// </summary>
    public interface IAvailability : IPartnerComponent<Tuple<string, string, string, string>>, IEntityGetOperations<Availability>
    {
        /// <summary>
        /// Retrieves the availability information.
        /// </summary>
        /// <returns>The availability information.</returns>
        new Availability Get();

        /// <summary>
        /// Asynchronously retrieves the availability information.
        /// </summary>
        /// <returns>The availability information.</returns>
        new Task<Availability> GetAsync();
    }
}
