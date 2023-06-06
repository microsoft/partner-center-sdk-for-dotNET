// -----------------------------------------------------------------------
// <copyright file="ISubscriptionSupportContact.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Threading.Tasks;
    using GenericOperations;
    using Models.Subscriptions;

    /// <summary>
    /// Defines the operations available on a customer's subscription support contact.
    /// </summary>
    public interface ISubscriptionSupportContact : IPartnerComponent<Tuple<string, string>>, IEntityGetOperations<SupportContact>, IEntityUpdateOperations<SupportContact>
    {
        /// <summary>
        /// Retrieves the support contact of the customer's subscription.
        /// </summary>
        /// <returns>The support contact.</returns>
        new SupportContact Get();

        /// <summary>
        /// Asynchronously retrieves the support contact of the customer's subscription.
        /// </summary>
        /// <returns>The support contact.</returns>
        new Task<SupportContact> GetAsync();

        /// <summary>
        /// Updates the support contact of the customer's subscription.
        /// </summary>
        /// <param name="supportContact">The support contact.</param>
        /// <returns>The updated support contact.</returns>
        new SupportContact Update(SupportContact supportContact);

        /// <summary>
        /// Asynchronously updates the support contact of the customer's subscription.
        /// </summary>
        /// <param name="supportContact">The support contact.</param>
        /// <returns>The updated support contact.</returns>
        new Task<SupportContact> UpdateAsync(SupportContact supportContact);
    }
}
