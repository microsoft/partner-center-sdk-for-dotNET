// -----------------------------------------------------------------------
// <copyright file="NewCommerceMigrationScheduleOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.NewCommerceMigrationSchedules
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.NewCommerceMigrationSchedules;
    using Microsoft.Store.PartnerCenter.Network;
    using Microsoft.Store.PartnerCenter.Utilities;

    /// <summary>
    /// New-Commerce migration schedule operations implementation class.
    /// </summary>
    internal class NewCommerceMigrationScheduleOperations : BasePartnerComponent<Tuple<string, string>>, INewCommerceMigrationSchedule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewCommerceMigrationScheduleOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant ID.</param>
        /// <param name="newCommerceMigrationScheduleId">The New-Commerce Migration Schedule ID.</param>
        public NewCommerceMigrationScheduleOperations(IPartner rootPartnerOperations, string customerId, string newCommerceMigrationScheduleId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, newCommerceMigrationScheduleId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            if (string.IsNullOrWhiteSpace(newCommerceMigrationScheduleId))
            {
                throw new ArgumentException("newCommerceMigrationScheduleId must be set.");
            }
        }

        /// <summary>
        /// Retrieves a New-Commerce migration schedule.
        /// </summary>
        /// <returns>The New-Commerce migration schedule.</returns>
        public NewCommerceMigrationSchedule Get()
        {
            return PartnerService.Instance.SynchronousExecute<NewCommerceMigrationSchedule>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves a New-Commerce migration schedule.
        /// </summary>
        /// <returns>The New-Commerce migration schedule.</returns>
        public async Task<NewCommerceMigrationSchedule> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<NewCommerceMigrationSchedule, NewCommerceMigrationSchedule>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetNewCommerceMigrationSchedule.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }

        /// <summary>
        /// Updates a New-Commerce migration schedule.
        /// </summary>
        /// <param name="newCommerceMigrationSchedule">A New-Commerece migration schedule to be updated.</param>
        /// <returns>The New-Commerce migration schedule that was updated.</returns>
        public NewCommerceMigrationSchedule Update(NewCommerceMigrationSchedule newCommerceMigrationSchedule)
        {
            return PartnerService.Instance.SynchronousExecute(() => this.UpdateAsync(newCommerceMigrationSchedule));
        }

        /// <summary>
        /// Asynchronously updates a New-Commerce migration schedule.
        /// </summary>
        /// <param name="newCommerceMigrationSchedule">A New-Commerece migration schedule to be updated.</param>
        /// <returns>The New-Commerce migration schedule that was updated.</returns>
        public async Task<NewCommerceMigrationSchedule> UpdateAsync(NewCommerceMigrationSchedule newCommerceMigrationSchedule)
        {
            ParameterValidator.Required(newCommerceMigrationSchedule, $"{nameof(newCommerceMigrationSchedule)} can't be null.");

            var partnerApiServiceProxy = new PartnerServiceProxy<NewCommerceMigrationSchedule, NewCommerceMigrationSchedule>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.UpdateNewCommerceMigrationSchedule.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.PutAsync(newCommerceMigrationSchedule);
        }

        /// <summary>
        /// Cancels a New-Commerce migration schedule.
        /// </summary>
        /// <returns>The New-Commerce migration schedule that was cancelled.</returns>
        public NewCommerceMigrationSchedule Cancel()
        {
            return PartnerService.Instance.SynchronousExecute(() => this.CancelAsync());
        }

        /// <summary>
        /// Asynchronously cancels a New-Commerce migration schedule.
        /// </summary>
        /// <returns>The New-Commerce migration schedule that was cancelled.</returns>
        public async Task<NewCommerceMigrationSchedule> CancelAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<NewCommerceMigrationSchedule, NewCommerceMigrationSchedule>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.CancelNewCommerceMigrationSchedule.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.PostAsync(null);
        }
    }
}