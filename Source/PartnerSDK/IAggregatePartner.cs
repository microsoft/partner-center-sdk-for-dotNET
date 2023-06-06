// -----------------------------------------------------------------------
// <copyright file="IAggregatePartner.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter
{
    using RequestContext;

    /// <summary>
    /// A partner operations interface which can be used to generate scoped partner operations that use a specific partner context.
    /// </summary>
    public interface IAggregatePartner : IPartner
    {
        /// <summary>
        /// Returns a partner operations object which uses the provided context when executing operations.
        /// </summary>
        /// <param name="context">An operation context object.</param>
        /// <returns>A partner operations object which uses the provided operation context.</returns>
        IPartner With(IRequestContext context);
    }
}