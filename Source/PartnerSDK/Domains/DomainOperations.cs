// -----------------------------------------------------------------------
// <copyright file="DomainOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Domains
{
    using System.Globalization;
    using System.Threading.Tasks;
    using Exceptions;
    using Network;
    using Utilities;

    /// <summary>
    /// The domain operations implementation class.
    /// </summary>
    internal class DomainOperations : BasePartnerComponent, IDomain
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="domain">The domain.</param>
        public DomainOperations(IPartner rootPartnerOperations, string domain)
            : base(rootPartnerOperations, domain)
        {
            ParameterValidator.Required(domain, "Domain is a required parameter.");
        }

        /// <summary>
        /// Checks the availability of a domain.
        /// </summary>
        /// <returns>A boolean value to indicate if the domain is available.</returns>
        public bool Exists()
        {
            return PartnerService.Instance.SynchronousExecute<bool>(() => this.ExistsAsync());
        }

        /// <summary>
        /// Asynchronously checks the availability of a domain.
        /// </summary>
        /// <returns>A boolean value to indicate if the domain is available.</returns>
        public async Task<bool> ExistsAsync()
        {
            var partnerApiServiceProxy = new PartnerServiceProxy<string, string>(
                this.Partner,
                string.Format(CultureInfo.InvariantCulture, PartnerService.Instance.Configuration.Apis.CheckDomainAvailability.Path, this.Context));

            try
            {
                await partnerApiServiceProxy.HeadAsync();
            }
            catch (PartnerException ex)
            {
                if (ex.ErrorCategory == PartnerErrorCategory.NotFound)
                {
                    return false;
                }

                throw;
            }

            return true;
        }
    }
}