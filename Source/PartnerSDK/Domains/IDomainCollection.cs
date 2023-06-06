// -----------------------------------------------------------------------
// <copyright file="IDomainCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Domains
{
    /// <summary>
    /// Encapsulates domains behavior.
    /// </summary>
    public interface IDomainCollection : IPartnerComponent
    {
        /// <summary>
        /// Obtains a specific domain behavior.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>The domain operations.</returns>
        IDomain ByDomain(string domain);
    }
}
