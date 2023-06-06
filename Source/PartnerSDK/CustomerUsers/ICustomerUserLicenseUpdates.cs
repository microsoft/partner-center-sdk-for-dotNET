// -----------------------------------------------------------------------
// <copyright file="ICustomerUserLicenseUpdates.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.CustomerUsers
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.Licenses;

    /// <summary>
    /// Represents the behavior of a customer user's license update collection.
    /// </summary>
    public interface ICustomerUserLicenseUpdates : IPartnerComponent<Tuple<string, string>>, IEntityCreateOperations<LicenseUpdate>
    {
        /// <summary>
        /// Assign licenses to a user.
        /// This method serves three scenarios:
        /// 1. Add license to a customer user.
        /// 2. Remove license from a customer user.
        /// 3. Update existing license for a customer user.
        /// </summary>
        /// <param name="entity">License update object.</param>
        /// <returns>Returned license update object.</returns>
        new LicenseUpdate Create(LicenseUpdate entity);

        /// <summary>
        /// Asynchronously assign licenses to a user.
        /// This method serves three scenarios:
        /// 1. Add license to a customer user.
        /// 2. Remove license from a customer user.
        /// 3. Update existing license for a customer user.
        /// </summary>
        /// <param name="entity">License update object.</param>
        /// <returns>Returned license update object.</returns>
        new Task<LicenseUpdate> CreateAsync(LicenseUpdate entity);
    }
}