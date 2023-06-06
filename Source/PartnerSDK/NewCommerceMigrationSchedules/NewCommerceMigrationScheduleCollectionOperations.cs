// -----------------------------------------------------------------------
// <copyright file="NewCommerceMigrationScheduleCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.NewCommerceMigrationSchedules
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Exceptions;
    using Microsoft.Store.PartnerCenter.Models.NewCommerceMigrationSchedules;
    using Microsoft.Store.PartnerCenter.Network;
    using Microsoft.Store.PartnerCenter.Utilities;
    using Newtonsoft.Json;

    /// <summary>
    /// New-Commerce migration schedule collection operations implementation class.
    /// </summary>
    internal class NewCommerceMigrationScheduleCollectionOperations : BasePartnerComponent, INewCommerceMigrationScheduleCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewCommerceMigrationScheduleCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer Id.</param>
        public NewCommerceMigrationScheduleCollectionOperations(IPartner rootPartnerOperations, string customerId)
            : base(rootPartnerOperations, customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }
        }

        /// <summary>
        /// Gets a specific New-Commerce migration schedule behavior.
        /// </summary>
        /// <param name="newCommerceMigrationScheduleId">The New-Commerce migration schedule ID.</param>
        /// <returns>The New-Commerce migration schedule operations.</returns>
        public INewCommerceMigrationSchedule this[string newCommerceMigrationScheduleId]
        {
            get
            {
                return this.ById(newCommerceMigrationScheduleId);
            }
        }

        /// <summary>
        /// Gets a specific New-Commerce migration schedule behavior.
        /// </summary>
        /// <param name="newCommerceMigrationScheduleId">The New-Commerce migration schedule ID.</param>
        /// <returns>The New-Commerce migration schedule operations.</returns>
        public INewCommerceMigrationSchedule ById(string newCommerceMigrationScheduleId)
        {
            return new NewCommerceMigrationScheduleOperations(this.Partner, this.Context, newCommerceMigrationScheduleId);
        }

        /// <summary>
        /// Creates a New-Commerce migration schedule.
        /// </summary>
        /// <param name="newCommerceMigrationSchedule">A New-Commerce migration schedule to be created.</param>
        /// <returns>The New-Commerce migration schedule that was just created.</returns>
        public NewCommerceMigrationSchedule Create(NewCommerceMigrationSchedule newCommerceMigrationSchedule)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.CreateAsync(newCommerceMigrationSchedule));
        }

        /// <summary>
        /// Asynchronously creates a New-Commerce migration schedule.
        /// </summary>
        /// <param name="newCommerceMigrationSchedule">A New-Commerece migration schedule to be created.</param>
        /// <returns>The New-Commerce migration schedule that was just created.</returns>
        public async Task<NewCommerceMigrationSchedule> CreateAsync(NewCommerceMigrationSchedule newCommerceMigrationSchedule)
        {
            ParameterValidator.Required(newCommerceMigrationSchedule, $"{nameof(newCommerceMigrationSchedule)} can't be null.");

            var partnerApiServiceProxy = new PartnerServiceProxy<NewCommerceMigrationSchedule, NewCommerceMigrationSchedule>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.CreateNewCommerceMigrationSchedule.Path, this.Context));

            return await partnerApiServiceProxy.PostAsync(newCommerceMigrationSchedule);
        }

        /// <summary>
        /// Gets all the New-Commerce migration schedules for the given search request.
        /// </summary>
        /// <param name="customerTenantId">The customer tenant id.</param>
        /// <param name="currentSubscriptionId">The current subscription id.</param>
        /// <param name="externalReferenceId">The external reference id.</param>
        /// <param name="pageSize">The page size, it should be between 1 to 300 if provided. Default is 300.</param>
        /// <param name="continuationToken">The continuation token.</param>
        /// <returns>The New-Commerce migration schedules and Continuation Token.</returns>
        public (IEnumerable<NewCommerceMigrationSchedule> NewCommerceMigrationSchedules, string ContinuationToken) Get(string customerTenantId = null, string currentSubscriptionId = null, string externalReferenceId = null, int? pageSize = null, string continuationToken = null)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.GetAsync(customerTenantId, currentSubscriptionId, externalReferenceId, pageSize, continuationToken));
        }

        /// <summary>
        /// Asynchronously gets all the New-Commerce migration schedules for the given search request.
        /// </summary>
        /// <param name="customerTenantId">The customer tenant id.</param>
        /// <param name="currentSubscriptionId">The current subscription id.</param>
        /// <param name="externalReferenceId">The external reference id.</param>
        /// <param name="pageSize">The page size, it should be between 1 to 300 if provided. Default is 300.</param>
        /// <param name="continuationToken">The continuation token.</param>
        /// <returns>The New-Commerce migration schedules and Continuation Token.</returns>
        public async Task<(IEnumerable<NewCommerceMigrationSchedule> NewCommerceMigrationSchedules, string ContinuationToken)> GetAsync(string customerTenantId = null, string currentSubscriptionId = null, string externalReferenceId = null, int? pageSize = null, string continuationToken = null)
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
               this.Partner, PartnerService.Instance.Configuration.Apis.GetNewCommerceMigrationSchedules.Path);

            if (!string.IsNullOrWhiteSpace(customerTenantId))
            {
                partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetNewCommerceMigrationSchedules.Parameters.CustomerTenantId, customerTenantId));
            }

            if (!string.IsNullOrWhiteSpace(currentSubscriptionId))
            {
                partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetNewCommerceMigrationSchedules.Parameters.CurrentSubscriptionId, currentSubscriptionId));
            }

            if (!string.IsNullOrWhiteSpace(externalReferenceId))
            {
                partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetNewCommerceMigrationSchedules.Parameters.ExternalReferenceId, externalReferenceId));
            }

            if (pageSize != null)
            {
                partnerApiServiceProxy.UriParameters.Add(new KeyValuePair<string, string>(
                    PartnerService.Instance.Configuration.Apis.GetNewCommerceMigrationSchedules.Parameters.PageSize, pageSize.ToString()));
            }

            if (continuationToken != null)
            {
                partnerApiServiceProxy.ContinuationToken = continuationToken;
            }

            var response = await partnerApiServiceProxy.GetAsync();

            try
            {
                string responsePayload = response?.Content == null ? string.Empty : await response.Content.ReadAsStringAsync();

                var newCommerceMigrationSchedules = JsonConvert.DeserializeObject<IEnumerable<NewCommerceMigrationSchedule>>(responsePayload);

                return (newCommerceMigrationSchedules, continuationToken);
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
    }
}