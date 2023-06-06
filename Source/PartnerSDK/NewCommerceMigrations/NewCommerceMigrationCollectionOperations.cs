// -----------------------------------------------------------------------
// <copyright file="NewCommerceMigrationCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.NewCommerceMigrations
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Exceptions;
    using Microsoft.Store.PartnerCenter.Models.NewCommerceMigrations;
    using Microsoft.Store.PartnerCenter.Network;
    using Microsoft.Store.PartnerCenter.Utilities;
    using Newtonsoft.Json;

    /// <summary>
    /// New-Commerce migration collection operations implementation class.
    /// </summary>
    internal class NewCommerceMigrationCollectionOperations : BasePartnerComponent, INewCommerceMigrationCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewCommerceMigrationCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        public NewCommerceMigrationCollectionOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }
        }

        /// <summary>
        /// Gets a specific New-Commerce migration behavior.
        /// </summary>
        /// <param name="newCommerceMigrationId">The New-Commerce migration ID.</param>
        /// <returns>The New-Commerce migration operations.</returns>
        public INewCommerceMigration this[string newCommerceMigrationId]
        {
            get
            {
                return this.ById(newCommerceMigrationId);
            }
        }

        /// <summary>
        /// Gets a specific New-Commerce migration behavior.
        /// </summary>
        /// <param name="newCommerceMigrationId">The New-Commerce migration ID.</param>
        /// <returns>The New-Commerce migration operations.</returns>
        public INewCommerceMigration ById(string newCommerceMigrationId)
        {
            return new NewCommerceMigrationOperations(this.Partner, this.Context, newCommerceMigrationId);
        }

        /// <summary>
        /// Creates a New-Commerce migration.
        /// </summary>
        /// <param name="newCommerceMigration">A New-Commerce migration to be created.</param>
        /// <returns>The New-Commerce migration that was just created.</returns>
        public NewCommerceMigration Create(NewCommerceMigration newCommerceMigration)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.CreateAsync(newCommerceMigration));
        }

        /// <summary>
        /// Asynchronously creates a New-Commerce migration.
        /// </summary>
        /// <param name="newCommerceMigration">A New-Commerece migration to be created.</param>
        /// <returns>The New-Commerce migration that was just created.</returns>
        public async Task<NewCommerceMigration> CreateAsync(NewCommerceMigration newCommerceMigration)
        {
            ParameterValidator.Required(newCommerceMigration, "newCommerceMigration can't be null.");

            var partnerApiServiceProxy = new PartnerServiceProxy<NewCommerceMigration, NewCommerceMigration>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.CreateNewCommerceMigration.Path, this.Context));

            return await partnerApiServiceProxy.PostAsync(newCommerceMigration);
        }

        /// <summary>
        /// Validates a New-Commerce migration.
        /// </summary>
        /// <param name="newCommerceMigration">The New-Commerece migration to be validated.</param>
        /// <returns>The New-Commerce migration eligibility.</returns>
        public NewCommerceEligibility Validate(NewCommerceMigration newCommerceMigration)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.ValidateAsync(newCommerceMigration));
        }

        /// <summary>
        /// Asynchronously validates a New-Commerce migration.
        /// </summary>
        /// <param name="newCommerceMigration">The New-Commerece migration to be validated.</param>
        /// <returns>The New-Commerce migration eligibility.</returns>
        public async Task<NewCommerceEligibility> ValidateAsync(NewCommerceMigration newCommerceMigration)
        {
            ParameterValidator.Required(newCommerceMigration, "newCommerceMigration can't be null.");

            var partnerApiServiceProxy = new PartnerServiceProxy<NewCommerceMigration, NewCommerceEligibility>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.ValidateNewCommerceMigration.Path, this.Context));

            return await partnerApiServiceProxy.PostAsync(newCommerceMigration);
        }

        /// <summary>
        /// Gets all the New-Commerce migrations for the given search request.
        /// </summary>
        /// <param name="customerTenantId">The customer tenant id.</param>
        /// <param name="currentSubscriptionId">The current subscription id.</param>
        /// <param name="externalReferenceId">The external reference id.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="continuationToken">The continuation token.</param>
        /// <returns>The New-Commerce migrations and Continuation Token.</returns>
        public GetNewCommerceMigrationsResponse Get(string customerTenantId, string currentSubscriptionId, string externalReferenceId, int? pageSize = null, string continuationToken = null)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.GetAsync(customerTenantId, currentSubscriptionId, externalReferenceId, pageSize, continuationToken));
        }

        /// <summary>
        /// Asynchronously gets all the New-Commerce migrations for the given search request.
        /// </summary>
        /// <param name="customerTenantId">The customer tenant id.</param>
        /// <param name="currentSubscriptionId">The current subscription id.</param>
        /// <param name="externalReferenceId">The external reference id.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="continuationToken">The continuation token.</param>
        /// <returns>The New-Commerce migrations and Continuation Token.</returns>
        public async Task<GetNewCommerceMigrationsResponse> GetAsync(string customerTenantId, string currentSubscriptionId, string externalReferenceId, int? pageSize = null, string continuationToken = null)
        {
            int maxPageSize = 300;

            if (string.IsNullOrWhiteSpace(customerTenantId) &&
                string.IsNullOrWhiteSpace(currentSubscriptionId) &&
                string.IsNullOrWhiteSpace(externalReferenceId))
            {
                throw new ArgumentNullException($"At least one of {nameof(customerTenantId)}, {nameof(currentSubscriptionId)} or {nameof(externalReferenceId)} is required.");
            }

            if (pageSize != null)
            {
                if (pageSize > maxPageSize)
                {
                    throw new ArgumentException($"Page size cannot be more than {maxPageSize}");
                }

                if (pageSize <= 0)
                {
                    throw new ArgumentException($"Page size must be greater than 0");
                }
            }

            var partnerApiServiceProxy = new PartnerServiceProxy<HttpResponseMessage, HttpResponseMessage>(
               this.Partner, PartnerService.Instance.Configuration.Apis.GetNewCommerceMigrations.Path);

            if (!string.IsNullOrWhiteSpace(customerTenantId))
            {
                partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetNewCommerceMigrations.Parameters.CustomerTenantId, customerTenantId));
            }

            if (!string.IsNullOrWhiteSpace(currentSubscriptionId))
            {
                partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetNewCommerceMigrations.Parameters.CurrentSubscriptionId, currentSubscriptionId));
            }

            if (!string.IsNullOrWhiteSpace(externalReferenceId))
            {
                partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetNewCommerceMigrations.Parameters.ExternalReferenceId, externalReferenceId));
            }

            if (pageSize != null)
            {
                partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetNewCommerceMigrations.Parameters.PageSize, pageSize.ToString()));
            }

            if (continuationToken != null)
            {
                partnerApiServiceProxy.ContinuationToken = continuationToken;
            }

            var response = await partnerApiServiceProxy.GetAsync();

            try
            {
                string responsePayload = response?.Content == null ? string.Empty : await response.Content.ReadAsStringAsync();

                var newCommerceMigrations = JsonConvert.DeserializeObject<IEnumerable<NewCommerceMigration>>(responsePayload);

                return new GetNewCommerceMigrationsResponse()
                {
                    NewCommerceMigrations = newCommerceMigrations,
                    ContinuationToken = response.Headers.TryGetValues("MS-ContinuationToken", out var responseContinuationTokenValues) ? responseContinuationTokenValues.FirstOrDefault() : null,
                };
            }
            catch (Exception deserializationProblem)
            {
                if (deserializationProblem.IsFatalException())
                {
                    // fail over
                    throw;
                }
                else
                {
                    throw new PartnerResponseParseException("Could not deserialize response", deserializationProblem);
                }
            }
        }

        /// <summary>
        /// Gets the collection of migration events associated to a New-Commerce migration ID or subscription ID.
        /// </summary>
        /// <param name="newCommerceMigrationId">The New-Commerce migration ID.</param>
        /// <param name="currentSubscriptionId">The current subscription ID.</param>
        /// <returns>The collection of New-Commerce migration events.</returns>
        public IEnumerable<NewCommerceMigrationEvent> GetEvents(string newCommerceMigrationId, string currentSubscriptionId)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.GetEventsAsync(newCommerceMigrationId, currentSubscriptionId));
        }

        /// <summary>
        /// Asynchronously gets the collection of migration events associated to a New-Commerce migration ID or subscription ID.
        /// </summary>
        /// <param name="newCommerceMigrationId">The New-Commerce migration ID.</param>
        /// <param name="currentSubscriptionId">The current subscription ID.</param>
        /// <returns>The collection of New-Commerce migration events.</returns>
        public async Task<IEnumerable<NewCommerceMigrationEvent>> GetEventsAsync(string newCommerceMigrationId, string currentSubscriptionId)
        {
            if (string.IsNullOrWhiteSpace(newCommerceMigrationId) && string.IsNullOrWhiteSpace(currentSubscriptionId))
            {
                throw new ArgumentNullException($"Either the {nameof(newCommerceMigrationId)} or {nameof(currentSubscriptionId)} is required.");
            }

            var partnerApiServiceProxy = new PartnerServiceProxy<IEnumerable<NewCommerceMigrationEvent>, IEnumerable<NewCommerceMigrationEvent>>(
               this.Partner,
               string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetNewCommerceMigrationEvents.Path, this.Context));

            if (!string.IsNullOrWhiteSpace(newCommerceMigrationId))
            {
                partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetNewCommerceMigrationEvents.Parameters.MigrationId, newCommerceMigrationId));
            }

            if (!string.IsNullOrWhiteSpace(currentSubscriptionId))
            {
                partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetNewCommerceMigrationEvents.Parameters.CurrentSubscriptionId, currentSubscriptionId));
            }

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
