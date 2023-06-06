// -----------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a unit of work to be executed by the SDK sample application
    /// </summary>
    internal interface IUnitOfWork
    {
        /// <summary>
        /// Gets the title of the unit of work.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state);
    }
}
