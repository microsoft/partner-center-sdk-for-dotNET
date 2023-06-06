// -----------------------------------------------------------------------
// <copyright file="IPartnerUsageSummary.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Usage
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.Usage;

    /// <summary>
    /// Defines the operations available on a partner's usage summary.
    /// </summary>
    public interface IPartnerUsageSummary : IPartnerComponent, IEntityGetOperations<PartnerUsageSummary>
    {
        /// <summary>
        /// Retrieves the partner's usage summary.
        /// </summary>
        /// <returns>The partner's usage summary.</returns>
        new PartnerUsageSummary Get();

        /// <summary>
        /// Asynchronously retrieves the partner's usage summary.
        /// </summary>
        /// <returns>The partner's usage summary.</returns>
        new Task<PartnerUsageSummary> GetAsync();
    }
}
