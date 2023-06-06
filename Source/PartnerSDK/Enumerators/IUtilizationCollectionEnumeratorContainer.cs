// -----------------------------------------------------------------------
// <copyright file="IUtilizationCollectionEnumeratorContainer.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Enumerators
{
    using Factory;
    using Models;
    using Models.Utilizations;

    /// <summary>
    /// Groups all supported utilization record enumerators for usage based subscriptions.
    /// </summary>
    public interface IUtilizationCollectionEnumeratorContainer : IPartnerComponent
    {
        /// <summary>
        /// Gets a factory that creates Azure utilization record collection enumerators.
        /// </summary>
        IResourceCollectionEnumeratorFactory<ResourceCollection<AzureUtilizationRecord>> Azure { get; }
    }
}
