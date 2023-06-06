// -----------------------------------------------------------------------
// <copyright file="PartnerUsageSummaryOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Usage
{
    using System.Threading.Tasks;
    using Models.Usage;
    using Network;

    /// <summary>
    /// This class implements the operations available on a partner's usage summary.
    /// </summary>
    internal class PartnerUsageSummaryOperations : BasePartnerComponent, IPartnerUsageSummary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerUsageSummaryOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public PartnerUsageSummaryOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Gets the partner's usage summary.
        /// </summary>
        /// <returns>The partner's usage summary.</returns>
        public PartnerUsageSummary Get()
        {
            return PartnerService.Instance.SynchronousExecute<PartnerUsageSummary>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously gets the partner's usage summary.
        /// </summary>
        /// <returns>The partner's usage summary.</returns>
        public async Task<PartnerUsageSummary> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<PartnerUsageSummary, PartnerUsageSummary>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetPartnerUsageSummary.Path);

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
