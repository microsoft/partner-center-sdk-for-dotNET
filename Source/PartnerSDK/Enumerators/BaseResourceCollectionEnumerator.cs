// -----------------------------------------------------------------------
// <copyright file="BaseResourceCollectionEnumerator.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Enumerators
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Network;
    using Newtonsoft.Json;
    using RequestContext;

    /// <summary>
    /// Base implementation for resource collection enumerators.
    /// </summary>
    /// <typeparam name="T">The type of the resource collection.</typeparam>
    internal abstract class BaseResourceCollectionEnumerator<T> : BasePartnerComponent, IResourceCollectionEnumerator<T> where T : ResourceBaseWithLinks<StandardResourceCollectionLinks>
    {
        /// <summary>
        /// The current resource collection.
        /// </summary>
        private T resourceCollection = null;

        /// <summary>
        /// The resource collection JSON converter.
        /// </summary>
        private JsonConverter resourceCollectionConverter = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseResourceCollectionEnumerator{T}"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="resourceCollection">The initial resource collection.</param>
        /// <param name="resourceCollectionConverter">An optional converter.</param>
        protected BaseResourceCollectionEnumerator(IPartner rootPartnerOperations, T resourceCollection, JsonConverter resourceCollectionConverter = null)
            : base(rootPartnerOperations)
        {
            if (resourceCollection == null)
            {
                throw new ArgumentNullException("resourceCollection");
            }

            this.resourceCollection = resourceCollection;
            this.resourceCollectionConverter = resourceCollectionConverter;
        }

        /// <summary>
        /// Gets whether the current customer collection is the first page of results or not.
        /// </summary>
        public bool IsFirstPage
        {
            get
            {
                if (!this.HasValue)
                {
                    throw new InvalidOperationException("The enumerator does not have a current value");
                }

                return this.Current.Links == null || this.Current.Links.Previous == null;
            }
        }

        /// <summary>
        /// Gets whether the current customer collection is the last page of results or not.
        /// </summary>
        public bool IsLastPage
        {
            get
            {
                if (!this.HasValue)
                {
                    throw new InvalidOperationException("The enumerator does not have a current value");
                }

                return this.Current.Links == null || this.Current.Links.Next == null;
            }
        }

        /// <summary>
        /// Gets whether the current result collection has a value or not. This indicates if the collection has been fully enumerated or not.
        /// </summary>
        public bool HasValue
        {
            get
            {
                return this.resourceCollection != null;
            }
        }

        /// <summary>
        /// The current resource collection.
        /// </summary>
        public T Current
        {
            get
            {
                return this.resourceCollection;
            }
        }

        /// <summary>
        /// Retrieves the next customer result set.
        /// </summary>
        /// <param name="context">An optional request context. If not provided, the context associated with the partner operations will be used.</param>
        public void Next(IRequestContext context = null)
        {
            PartnerService.Instance.SynchronousExecute(() => this.NextAsync(context));
        }

        /// <summary>
        /// Asynchronously retrieves the next customer result set.
        /// </summary>
        /// <param name="context">An optional request context. If not provided, the context associated with the partner operations will be used.</param>
        /// <returns>A task which completes when fetching the next set of results is done.</returns>
        public async Task NextAsync(IRequestContext context = null)
        {
            if (!this.HasValue)
            {
                throw new InvalidOperationException("The enumerator does not have a current value");
            }

            if (this.IsLastPage)
            {
                // we are done
                this.resourceCollection = null;
            }
            else
            {
                // get the next page
                var partnerServiceProxy = new PartnerServiceProxy<T, T>(
                this.Partner,
                this.resourceCollection.Links.Next.Uri.ToString(),
                jsonConverter: this.resourceCollectionConverter);

                // the links already contains the query parameters, let's not replicate them
                partnerServiceProxy.IsUrlPathAlreadyBuilt = true;

                foreach (var header in this.resourceCollection.Links.Next.Headers)
                {
                    partnerServiceProxy.AdditionalRequestHeaders.Add(new KeyValuePair<string, string>(header.Key, header.Value));
                }

                this.resourceCollection = await partnerServiceProxy.GetAsync();
            }
        }

        /// <summary>
        /// Retrieves the previous customer result set.
        /// </summary>
        /// <param name="context">An optional request context. If not provided, the context associated with the partner operations will be used.</param>
        public void Previous(IRequestContext context = null)
        {
            PartnerService.Instance.SynchronousExecute(() => this.PreviousAsync(context));
        }

        /// <summary>
        /// Asynchronously retrieves the previous customer result set.
        /// </summary>
        /// <param name="context">An optional request context. If not provided, the context associated with the partner operations will be used.</param>
        /// <returns>A task which completes when fetching the previous set of results is done.</returns>
        public async Task PreviousAsync(IRequestContext context = null)
        {
            if (!this.HasValue)
            {
                throw new InvalidOperationException("The enumerator does not have a current value");
            }

            if (this.IsFirstPage)
            {
                // we are done
                this.resourceCollection = null;
            }
            else
            {
                // get the previous page
                var partnerServiceProxy = new PartnerServiceProxy<T, T>(
                    this.Partner,
                    this.resourceCollection.Links.Previous.Uri.ToString(),
                    jsonConverter: this.resourceCollectionConverter);

                // the links already contains the query parameters, let's not replicate them
                partnerServiceProxy.IsUrlPathAlreadyBuilt = true;

                foreach (var header in this.resourceCollection.Links.Previous.Headers)
                {
                    partnerServiceProxy.AdditionalRequestHeaders.Add(new KeyValuePair<string, string>(header.Key, header.Value));
                }

                this.resourceCollection = await partnerServiceProxy.GetAsync();
            }
        }
    }
}
