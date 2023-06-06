// -----------------------------------------------------------------------
// <copyright file="PromotionEligibilitiesRequest.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.PromotionEligibilities
{
    using System.Collections.Generic;

    /// <summary>
    /// This class represents a model of a promotion eligibilities request object.
    /// </summary>
    public class PromotionEligibilitiesRequest : ResourceBase
    {
        /// <summary>
        /// Gets or sets the items to check promotional eligibility against.
        /// </summary>
        public IEnumerable<PromotionEligibilitiesRequestItem> Items { get; set; }
    }
}
