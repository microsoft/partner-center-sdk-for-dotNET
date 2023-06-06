// -----------------------------------------------------------------------
// <copyright file="License.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Licenses
{
    using System.Collections.Generic;
    using PartnerCenter.Models;

    /// <summary>
    /// Model for user licenses assigned to a user.
    /// </summary>
    public sealed class License : ResourceBase
    {
        /// <summary>
        /// Gets or sets the service plan collection. Service plans refer to services that user is assigned to use.
        /// For example , Delve is a service plan which a user is either assigned to use or can be assigned to use.
        /// </summary>
        public IEnumerable<ServicePlan> ServicePlans { get; set; }

        /// <summary>
        /// Gets or sets the product SKU which the license applies to.
        /// </summary>
        public ProductSku ProductSku { get; set; }
    }
}
