// -----------------------------------------------------------------------
// <copyright file="ICustomerSubscribedSkuCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.SubscribedSkus
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.Licenses;
    using PartnerCenter.Models;

    /// <summary>
    /// Represents the behavior of the customer's subscribed products.
    /// </summary>
    public interface ICustomerSubscribedSkuCollection : IPartnerComponent
    {
        /// <summary>
        /// Retrieves all the customer subscribed products.
        /// </summary>
        /// <param name="licenseGroupIds"> License group id.</param>
        /// <returns>All the customer subscribed products.</returns>
        ResourceCollection<SubscribedSku> Get(List<LicenseGroupId> licenseGroupIds = null);

        /// <summary>
        /// Asynchronously retrieves all the customer subscribed products.
        /// </summary>
        /// <param name="licenseGroupIds"> License group id.</param>
        /// <returns>All the customer subscribed products.</returns>
        Task<ResourceCollection<SubscribedSku>> GetAsync(List<LicenseGroupId> licenseGroupIds = null);
    }
}
