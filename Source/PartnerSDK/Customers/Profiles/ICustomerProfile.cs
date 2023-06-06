// -----------------------------------------------------------------------
// <copyright file="ICustomerProfile.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Customers.Profiles
{
    using System.Threading.Tasks;
    using GenericOperations;
    using Models;

    /// <summary>
    /// Encapsulates a single customer profile behavior.
    /// </summary>
    /// <typeparam name="T">The type of the customer profile.</typeparam>
    public interface ICustomerProfile<T> : ICustomerReadonlyProfile<T>, IEntityUpdateOperations<T> where T : ResourceBase
    {
        /// <summary>
        /// Updates the customer profile.
        /// </summary>
        /// <param name="customerProfile">The customer profile information.</param>
        /// <returns>The updated customer profile.</returns>
        new T Update(T customerProfile);

        /// <summary>
        /// Asynchronously updates the customer profile.
        /// </summary>
        /// <param name="customerProfile">The customer profile information.</param>
        /// <returns>The updated customer profile.</returns>
        new Task<T> UpdateAsync(T customerProfile);
    }
}