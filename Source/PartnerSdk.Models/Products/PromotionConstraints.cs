// <copyright file="PromotionConstraints.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Store.PartnerCenter.Models.Products
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Class represents Promotion constraints.
    /// </summary>
    public class PromotionConstraints
    {
        private readonly List<SeatConstraint> seatConstraints;
        private readonly List<AssetOwnershipLimit> assetOwnershipLimits;
        private readonly List<EligibilityConstraint> eligibilityConstraints;
        private readonly List<List<ProductOwnershipConstraint>> productOwnershipConstraints;

        /// <summary>
        /// Initializes a new instance of the <see cref="PromotionConstraints"/> class.
        /// </summary>
        public PromotionConstraints()
        {
            this.seatConstraints = new List<SeatConstraint>();
            this.assetOwnershipLimits = new List<AssetOwnershipLimit>();
            this.eligibilityConstraints = new List<EligibilityConstraint>();
            this.productOwnershipConstraints = new List<List<ProductOwnershipConstraint>>();
        }

        /// <summary>
        /// Gets SeatConstraints.
        /// </summary>
        public List<SeatConstraint> SeatConstraints
        {
            get { return this.seatConstraints; }
        }

        /// <summary>
        /// Gets AssetOwnershipLimits.
        /// </summary>
        public List<AssetOwnershipLimit> AssetOwnershipLimits
        {
            get { return this.assetOwnershipLimits; }
        }

        /// <summary>
        /// Gets EligibilityConstraints.
        /// </summary>
        public List<EligibilityConstraint> EligibilityConstraints
        {
            get { return this.eligibilityConstraints; }
        }

        /// <summary>
        /// Gets ProductOwnershipConstraints.
        /// </summary>
        public List<List<ProductOwnershipConstraint>> ProductOwnershipConstraints
        {
            get { return this.productOwnershipConstraints; }
        }
    }
}
