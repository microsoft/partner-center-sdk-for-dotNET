// -----------------------------------------------------------------------
// <copyright file="IPartnerFactory.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Factory
{
    /// <summary>
    /// Creates instances of <see cref="IPartner"/>.
    /// </summary>
    internal interface IPartnerFactory
    {
        /// <summary>
        /// Builds a <see cref="IAggregatePartner"/> instance and configures it using the provided partner credentials.
        /// </summary>
        /// <param name="credentials">The partner credentials. Use the extensions to obtain these.</param>
        /// <returns>A configured partner object.</returns>
        IAggregatePartner Build(IPartnerCredentials credentials);
    }
}
