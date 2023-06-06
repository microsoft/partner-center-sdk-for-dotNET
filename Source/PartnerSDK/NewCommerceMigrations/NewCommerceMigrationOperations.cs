// -----------------------------------------------------------------------
// <copyright file="NewCommerceMigrationOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.NewCommerceMigrations
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.Models.NewCommerceMigrations;
    using Microsoft.Store.PartnerCenter.Network;

    /// <summary>
    /// New-Commerce migration operations implementation class.
    /// </summary>
    internal class NewCommerceMigrationOperations : BasePartnerComponent<Tuple<string, string>>, INewCommerceMigration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewCommerceMigrationOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="customerId">The customer tenant ID.</param>
        /// <param name="newCommerceMigrationId">The New-Commerce Migration ID.</param>
        public NewCommerceMigrationOperations(IPartner rootPartnerOperations, string customerId, string newCommerceMigrationId)
            : base(rootPartnerOperations, new Tuple<string, string>(customerId, newCommerceMigrationId))
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("customerId must be set.");
            }

            if (string.IsNullOrWhiteSpace(newCommerceMigrationId))
            {
                throw new ArgumentException("newCommerceMigrationId must be set.");
            }
        }

        /// <summary>
        /// Retrieves a New-Commerce migration.
        /// </summary>
        /// <returns>The New-Commerce migration.</returns>
        public NewCommerceMigration Get()
        {
            return PartnerService.Instance.SynchronousExecute<NewCommerceMigration>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves a New-Commerce migration.
        /// </summary>
        /// <returns>The New-Commerce migration.</returns>
        public async Task<NewCommerceMigration> GetAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<NewCommerceMigration, NewCommerceMigration>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.GetNewCommerceMigration.Path, this.Context.Item1, this.Context.Item2));

            return await partnerApiServiceProxy.GetAsync();
        }
    }
}
