// -----------------------------------------------------------------------
// <copyright file="UtilizationCollectionEnumeratorContainer.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Enumerators
{
    using System;
    using Factory;
    using Models;
    using Models.Utilizations;

    /// <summary>
    /// Utilization collection enumerator container implementation class.
    /// </summary>
    internal class UtilizationCollectionEnumeratorContainer : BasePartnerComponent, IUtilizationCollectionEnumeratorContainer
    {
        /// <summary>
        /// A lazy reference to an Azure utilization record enumerator factory.
        /// </summary>
        private Lazy<IndexBasedCollectionEnumeratorFactory<AzureUtilizationRecord, ResourceCollection<AzureUtilizationRecord>>> azureUtilizationRecordEnumeratorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="UtilizationCollectionEnumeratorContainer"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public UtilizationCollectionEnumeratorContainer(IPartner rootPartnerOperations) : base(rootPartnerOperations)
        {
            this.azureUtilizationRecordEnumeratorFactory =
                new Lazy<IndexBasedCollectionEnumeratorFactory<AzureUtilizationRecord, ResourceCollection<AzureUtilizationRecord>>>(
                    () => new IndexBasedCollectionEnumeratorFactory<AzureUtilizationRecord, ResourceCollection<AzureUtilizationRecord>>(this.Partner));
        }

        /// <summary>
        /// Gets a factory that creates Azure utilization record collection enumerators.
        /// </summary>
        public IResourceCollectionEnumeratorFactory<ResourceCollection<AzureUtilizationRecord>> Azure
        {
            get
            {
                return this.azureUtilizationRecordEnumeratorFactory.Value;
            }
        }
    }
}