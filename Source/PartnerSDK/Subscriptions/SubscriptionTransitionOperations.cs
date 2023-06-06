// -----------------------------------------------------------------------
// <copyright file="SubscriptionTransitionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Subscriptions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Microsoft.Store.PartnerCenter.Models.Subscriptions;
    using Microsoft.Store.PartnerCenter.Network;

    /// <summary>
    /// The customer subscription transition implementation.
    /// </summary>
    internal class SubscriptionTransitionOperations : BasePartnerComponent<Tuple<string, string>>, ISubscriptionTransitionCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionTransitionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id to whom the subscriptions belong.</param>
        /// <param name="subscriptionId">The subscription Id where the transition is occurring.</param>
        public SubscriptionTransitionOperations(IPartner rootPartnerOperations, string customerId, string subscriptionId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, subscriptionId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId should be set.");
            }

            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentException("subscriptionId should be set.");
            }
        }

        /// <summary>
        /// Retrieves all subscription transitions.
        /// </summary>
        /// <returns>The subscription transitions.</returns>
        public ResourceCollection<Transition> Get() => PartnerService.Instance.SynchronousExecute(this.GetAsync);

        /// <summary>
        /// Asynchronously retrieves all subscription transitions.
        /// </summary>
        /// <returns>The subscription transitions.</returns>
        public async Task<ResourceCollection<Transition>> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<Transition, ResourceCollection<Transition>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscriptionTransitions.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <summary>
        /// Retrieves all subscription transitions matching the operation id.
        /// </summary>
        /// <param name="operationId">The operation id of the transition being queried for.</param>
        /// <returns>The subscription transitions.</returns>
        public ResourceCollection<Transition> Get(string operationId) => PartnerService.Instance.SynchronousExecute(() => this.GetAsync(operationId));

        /// <summary>
        /// Asynchronously retrieves all subscription transitions matching the operation id.
        /// </summary>
        /// <param name="operationId">The operation id of the transition being queried for.</param>
        /// <returns>The subscription transitions.</returns>
        public async Task<ResourceCollection<Transition>> GetAsync(string operationId)
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<Transition, ResourceCollection<Transition>>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetSubscriptionTransitions.Path, this.Context.Item1, this.Context.Item2));

            partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                PartnerService.Instance.Configuration.Apis.GetSubscriptionTransitions.Parameters.OperationId, operationId));

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <summary>
        /// Performs a subscription transition.
        /// </summary>
        /// <param name="subscriptionTransition">The subscription transition to perform.</param>
        /// <returns>The subscription transition result.</returns>
        public Transition Create(Transition subscriptionTransition) => PartnerService.Instance.SynchronousExecute(() => this.CreateAsync(subscriptionTransition));

        /// <summary>
        /// Asynchronously performs a subscription transition.
        /// </summary>
        /// <param name="subscriptionTransition">The subscription transition to perform.</param>
        /// <returns>A task containing the subscription transition result.</returns>
        public async Task<Transition> CreateAsync(Transition subscriptionTransition)
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<Transition, Transition>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.PostSubscriptionTransition.Path, this.Context.Item1, this.Context.Item2),
                jsonConverter: new ResourceCollectionConverter<Transition>());

            return await partnerApiServiceProxy.PostAsync(subscriptionTransition);
        }
    }
}
