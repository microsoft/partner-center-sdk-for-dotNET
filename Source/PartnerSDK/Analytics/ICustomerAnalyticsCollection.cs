// -----------------------------------------------------------------------
// <copyright file="ICustomerAnalyticsCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Analytics
{
    /// <summary>
    /// Holds operations that apply to customer analytics collection.
    /// </summary>
    public interface ICustomerAnalyticsCollection : IPartnerComponent
    {
        /// <summary>
        /// Gets the customer's licenses analytics collection.
        /// </summary>
        ICustomerLicensesAnalyticsCollection Licenses { get; }
    }
}