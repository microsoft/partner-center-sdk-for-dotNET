// -----------------------------------------------------------------------
// <copyright file="BasePartnerComponent{TContext}.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter
{
    using System;

    /// <summary>
    /// Holds common partner component properties and behavior. All components should inherit from this class.
    /// </summary>
    /// <typeparam name="TContext">The context object type.</typeparam>
    internal abstract class BasePartnerComponent<TContext> : IPartnerComponent<TContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasePartnerComponent{TContext}"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations that created this component.</param>
        /// <param name="componentContext">A component context object to work with.</param>
        protected BasePartnerComponent(IPartner rootPartnerOperations, TContext componentContext = default(TContext))
        {
            if (rootPartnerOperations == null)
            {
                throw new ArgumentNullException("rootPartnerOperations");
            }

            this.Partner = rootPartnerOperations;
            this.Context = componentContext;
        }

        /// <summary>
        /// Gets a reference to the partner operations instance that generated this component.
        /// </summary>
        public IPartner Partner { get; private set; }

        /// <summary>
        /// Gets the component context object.
        /// </summary>
        public TContext Context { get; private set; }
    }
}