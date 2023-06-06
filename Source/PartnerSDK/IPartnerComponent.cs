// -----------------------------------------------------------------------
// <copyright file="IPartnerComponent.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter
{
    /// <summary>
    /// Represents a partner SDK component.
    /// </summary>
    /// <typeparam name="TContext">The type of the component's context object.</typeparam>
    public interface IPartnerComponent<TContext>
    {
        /// <summary>
        /// Gets a reference to the partner operations instance that generated this component.
        /// </summary>
        IPartner Partner { get; }

        /// <summary>
        /// Gets the component context object.
        /// </summary>
        TContext Context { get; }
    }

    /// <summary>
    /// Represents a partner SDK component which has a string context.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Not applicable.")]
    public interface IPartnerComponent : IPartnerComponent<string>
    {
    }
}
