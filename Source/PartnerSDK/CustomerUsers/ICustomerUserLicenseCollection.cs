// -----------------------------------------------------------------------
// <copyright file="ICustomerUserLicenseCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CustomerUsers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.Licenses;
    using PartnerCenter.Models;

    /// <summary>
    /// Represents the behavior of a customer's user license collection.
    /// </summary>
    public interface ICustomerUserLicenseCollection : IPartnerComponent<Tuple<string, string>>
    {
        /// <summary>
        /// Retrieves the assigned licenses to a customer user.
        /// </summary>
        /// <param name="licenseGroupIds"> License group id.</param>
        /// <returns>The assigned licenses to a customer user.</returns>
        ResourceCollection<License> Get(List<LicenseGroupId> licenseGroupIds = null);

        /// <summary>
        /// Asynchronously retrieves the assigned licenses to a customer user.
        /// </summary>
        /// <param name="licenseGroupIds"> License group id.</param>
        /// <returns>The assigned licenses to a customer user.</returns>
        Task<ResourceCollection<License>> GetAsync(List<LicenseGroupId> licenseGroupIds = null);
    }
}