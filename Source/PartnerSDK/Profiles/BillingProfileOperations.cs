// -----------------------------------------------------------------------
// <copyright file="BillingProfileOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Profiles
{
    using System.Threading.Tasks;
    using Models.Partners;
    using Network;

    /// <summary>
    /// The billing profile operations implementation.
    /// </summary>
    internal class BillingProfileOperations : BasePartnerComponent, IBillingProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BillingProfileOperations"/> class. 
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        public BillingProfileOperations(IPartner rootPartnerOperations)
            : base(rootPartnerOperations)
        {
        }

        /// <summary>
        /// Retrieves the billing profile.
        /// </summary>
        /// <returns>The billing profile.</returns>
        public BillingProfile Get()
        {
            return PartnerService.Instance.SynchronousExecute<BillingProfile>(() => this.GetAsync());
        }

        /// <summary>
        /// Asynchronously retrieves the billing profile.
        /// </summary>
        /// <returns>The billing profile.</returns>
        public async Task<BillingProfile> GetAsync()
        {
            var partnerServiceProxy = new PartnerServiceProxy<BillingProfile, BillingProfile>(
                this.Partner,
                PartnerService.Instance.Configuration.Apis.GetPartnerBillingProfile.Path);
            return await partnerServiceProxy.GetAsync();
        }

        /// <summary>
        /// Updates the billing profile.
        /// </summary>
        /// <param name="updatePayload">Payload of the update request.</param>
        /// <returns>Updated billing profile.</returns>
        public BillingProfile Update(BillingProfile updatePayload)
        {
            return PartnerService.Instance.SynchronousExecute<BillingProfile>(() => this.UpdateAsync(updatePayload));
        }

        /// <summary>
        /// Updates the billing profile.
        /// </summary>
        /// <param name="updatePayload">Payload of the update request.</param>
        /// <returns>Updated billing Profile.</returns>
        public async Task<BillingProfile> UpdateAsync(BillingProfile updatePayload)
        {
            var partnerServiceProxy = new PartnerServiceProxy<BillingProfile, BillingProfile>(
                this.Partner,
                string.Format(PartnerService.Instance.Configuration.Apis.UpdatePartnerBillingProfile.Path));

            return await partnerServiceProxy.PutAsync(updatePayload);
        }
    }
}
