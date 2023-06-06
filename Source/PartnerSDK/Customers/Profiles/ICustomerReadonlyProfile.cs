// -----------------------------------------------------------------------
// <copyright file="ICustomerReadonlyProfile.cs" company="Microsoft Corporation">
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
    /// Encapsulates a single customer readonly profile behavior.
    /// </summary>
    /// <typeparam name="T">The type of the customer profile.</typeparam>
    public interface ICustomerReadonlyProfile<T> : IPartnerComponent, IEntityGetOperations<T> where T : ResourceBase
    {
        /// <summary>
        /// Retrieves the customer profile.
        /// </summary>
        /// <returns>The customer profile.</returns>
        new T Get();

        /// <summary>
        /// Asynchronously retrieves the customer profile.
        /// </summary>
        /// <returns>The customer profile.</returns>
        new Task<T> GetAsync();
    }
}