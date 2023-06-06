// -----------------------------------------------------------------------
// <copyright file="IPartnerAnalyticsCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Analytics
{
    /// <summary>
    /// Encapsulates collection of partner's analytics.
    /// </summary>
    public interface IPartnerAnalyticsCollection : IPartnerComponent
    {
        /// <summary>
        /// Gets the partner's licenses analytics collection.
        /// </summary>
        IPartnerLicensesAnalyticsCollection Licenses { get; }
    }
}