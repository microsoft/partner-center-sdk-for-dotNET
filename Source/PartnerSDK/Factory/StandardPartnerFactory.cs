// -----------------------------------------------------------------------
// <copyright file="StandardPartnerFactory.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Factory
{
    /// <summary>
    /// Standard implementation of the partner factory.
    /// </summary>
    internal class StandardPartnerFactory : IPartnerFactory
    {
        /// <summary>
        /// Builds a <see cref="IAggregatePartner"/> instance and configures it using the provided partner credentials.
        /// </summary>
        /// <param name="credentials">The partner credentials. Use the extensions to obtain these.</param>
        /// <returns>A configured partner object.</returns>
        public IAggregatePartner Build(IPartnerCredentials credentials)
        {
            return new AggregatePartnerOperations(credentials);
        }
    }
}
