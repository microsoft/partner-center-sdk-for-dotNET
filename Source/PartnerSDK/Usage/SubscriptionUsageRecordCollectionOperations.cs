// -----------------------------------------------------------------------
// <copyright file="SubscriptionUsageRecordCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Usage
{
    using System;

    /// <summary>
    /// Implementation class for subscription usage record collection operations.
    /// </summary>
    internal class SubscriptionUsageRecordCollectionOperations : BasePartnerComponent<Tuple<string, string>>, ISubscriptionUsageRecordCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionUsageRecordCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        /// <param name="subscriptionId">The subscription id.</param>
        public SubscriptionUsageRecordCollectionOperations(IPartner rootPartnerOperations, string customerId, string subscriptionId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, subscriptionId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId should be set.");
            }

            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentException("subscriptionId should be set.");
            }
        }

        /// <summary>
        /// Gets the subscription usage records grouped by resource.
        /// </summary>
        public IUsageRecordByResourceCollection ByResource
        {
            get
            {
                return new UsageRecordByResourceCollectionOperations(this.Partner, this.Context.Item1, this.Context.Item2);
            }
        }

        /// <summary>
        /// Gets the subscription usage records grouped by meter.
        /// </summary>
        public IUsageRecordByMeterCollection ByMeter
        {
            get
            {
                return new UsageRecordByMeterCollectionOperations(this.Partner, this.Context.Item1, this.Context.Item2);
            }
        }
    }
}
