// -----------------------------------------------------------------------
// <copyright file="UtilizationCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Utilization
{
    using System;

    /// <summary>
    /// Implements subscription utilization behavior.
    /// </summary>
    internal class UtilizationCollectionOperations : BasePartnerComponent<Tuple<string, string>>, IUtilizationCollection
    {
        /// <summary>
        /// A lazy reference to Azure utilization collection object.
        /// </summary>
        private Lazy<IAzureUtilizationCollection> azureUtilizationOperations;

        /// <summary>
        /// Initializes a new instance of the <see cref="UtilizationCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations.</param>
        /// <param name="customerId">The customer ID.</param>
        /// <param name="subscriptionId">The subscription ID.</param>
        public UtilizationCollectionOperations(IPartner rootPartnerOperations, string customerId, string subscriptionId) :
            base(rootPartnerOperations, new Tuple<string, string>(customerId, subscriptionId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set");
            }

            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentException("subscriptionId must be set");
            }

            this.azureUtilizationOperations = new Lazy<IAzureUtilizationCollection>(() => new AzureUtilizationCollectionOperations(
                this.Partner,
                this.Context.Item1,
                this.Context.Item2));
        }

        /// <summary>
        /// Gets Azure subscription utilization behavior.
        /// </summary>
        public IAzureUtilizationCollection Azure
        {
            get
            {
                return this.azureUtilizationOperations.Value;
            }
        }
    }
}
