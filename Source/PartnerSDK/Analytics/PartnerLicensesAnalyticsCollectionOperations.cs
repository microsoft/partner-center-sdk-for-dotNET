// -----------------------------------------------------------------------
// <copyright file="PartnerLicensesAnalyticsCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------
namespace Microsoft.Store.PartnerCenter.Analytics
{
    /// <summary>
    /// Implements the operations on partner licenses analytics collection.
    /// </summary>
    internal class PartnerLicensesAnalyticsCollectionOperations : BasePartnerComponent, IPartnerLicensesAnalyticsCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerLicensesAnalyticsCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public PartnerLicensesAnalyticsCollectionOperations(IPartner rootPartnerOperations) : base(rootPartnerOperations)
        {
            this.Deployment = new PartnerLicensesDeploymentInsightsCollectionOperations(rootPartnerOperations);
            this.Usage = new PartnerLicensesUsageInsightsCollectionOperations(rootPartnerOperations);
        }

        /// <summary>
        /// Gets the partner's licenses' deployment insights collection operations.
        /// </summary>
        public IPartnerLicensesDeploymentInsightsCollection Deployment { get; private set; }

        /// <summary>
        /// Gets the partner's licenses' usage insights collection operations.
        /// </summary>
        public IPartnerLicensesUsageInsightsCollection Usage { get; private set; }
    }
}
