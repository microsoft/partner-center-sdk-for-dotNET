// -----------------------------------------------------------------------
// <copyright file="BasePartnerComponent.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter
{
    using System;

    /// <summary>
    /// Holds common partner component properties and behavior. The context is string type by default.
    /// </summary>
    internal abstract class BasePartnerComponent : BasePartnerComponent<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasePartnerComponent"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations that created this component.</param>
        /// <param name="componentContext">A component context object to work with.</param>
        protected BasePartnerComponent(IPartner rootPartnerOperations, string componentContext = default(string))
            : base(rootPartnerOperations, componentContext)
        {
        }
    }
}