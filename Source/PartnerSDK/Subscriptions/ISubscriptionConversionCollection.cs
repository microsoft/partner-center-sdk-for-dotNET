// -----------------------------------------------------------------------
// <copyright file="ISubscriptionConversionCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;
    using Models.Subscriptions;

    /// <summary>
    /// This interface defines the conversion operations available on a customer's trial subscription.
    /// </summary>
    public interface ISubscriptionConversionCollection : IPartnerComponent<Tuple<string, string>>, IEntireEntityCollectionRetrievalOperations<Conversion, ResourceCollection<Conversion>>, IEntityCreateOperations<Conversion, ConversionResult>
    {
        /// <summary>
        /// Submits a trial subscription conversion.
        /// </summary>
        /// <param name="conversion">The new subscription conversion information.</param>
        /// <returns>The subscription conversion results.</returns>
        new ConversionResult Create(Conversion conversion);

        /// <summary>
        /// Asynchronously submits a trial subscription conversion.
        /// </summary>
        /// <param name="conversion">The new subscription conversion information.</param>
        /// <returns>The subscription conversion results.</returns>       
        new Task<ConversionResult> CreateAsync(Conversion conversion);

        /// <summary>
        /// Retrieves all conversions for the trial subscription.
        /// </summary>
        /// <returns>The subscription conversions.</returns>
        new ResourceCollection<Conversion> Get();

        /// <summary>
        /// Asynchronously retrieves all conversions for the trial subscription.
        /// </summary>
        /// <returns>The subscription conversions.</returns>
        new Task<ResourceCollection<Conversion>> GetAsync();
    }
}
