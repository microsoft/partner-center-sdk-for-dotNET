// -----------------------------------------------------------------------
// <copyright file="ResourceBase.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models
{
    /// <summary>
    /// Base class for partner API resources.
    /// </summary>
    public abstract class ResourceBase
    {
        /// <summary>
        /// The resource attributes.
        /// </summary>
        private readonly ResourceAttributes attributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceBase"/> class.
        /// </summary>
        protected ResourceBase()
        {
            this.attributes = new ResourceAttributes(this.GetType());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceBase"/> class.
        /// </summary>
        /// <param name="objectType">The type of the object.</param>
        protected ResourceBase(string objectType)
            : this()
        {
            this.attributes.ObjectType = objectType;
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        public ResourceAttributes Attributes
        {
            get
            {
                return this.attributes;
            }
        }
    }
}