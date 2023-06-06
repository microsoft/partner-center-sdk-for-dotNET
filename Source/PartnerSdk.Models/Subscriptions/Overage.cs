// -----------------------------------------------------------------------
// <copyright file="Overage.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Subscriptions
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Initializes a new instance of the <see cref="Overage"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Overage : Contract
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Overage"/> class.
        /// </summary>
        public Overage()
        {
            this.Links = new OverageLinks();
        }

        /// <summary>
        /// Gets or sets the azure entitlement identifier.
        /// </summary>
        /// <value>
        /// The azure entitlement identifier.
        /// </value>
        public string AzureEntitlementId { get; set; }

        /// <summary>
        /// Gets or sets the MPN ID of the reseller of record, used in the indirect partner model.
        /// </summary>
        /// <value>
        /// The partner identifier.
        /// </value>
        public string PartnerId { get; set; }

        /// <summary>
        /// Gets or sets the type of the overage/PayG.
        /// </summary>
        /// <value>
        /// The overage type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the azure entitlement has overage enabled.
        /// </summary>
        /// <value>True if the overage is enabled.</value>
        public bool OverageEnabled { get; set; }

        /// <summary>
        /// Gets the type of contract.
        /// </summary>
        public override ContractType ContractType
        {
            get { return ContractType.Subscription; }
        }

        /// <summary>
        /// Gets or sets the overage links.
        /// </summary>
        /// <value>
        /// The links.
        /// </value>
        public OverageLinks Links { get; set; }
    }
}
