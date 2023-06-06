// -----------------------------------------------------------------------
// <copyright file="ApplicationPartnerCredentials.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Logging;
    using Microsoft.Identity.Client;
    using RequestContext;

    /// <summary>
    /// Partner service credentials based on azure active directory application credentials.
    /// </summary>
    internal class ApplicationPartnerCredentials : BasePartnerCredentials
    {
        /// <summary>
        /// The default AAD authority endpoint.
        /// </summary>
        private const string DefaultAadAuthority = "https://login.windows.net";

        /// <summary>
        /// The default graph endpoint.
        /// </summary>
        private const string DefaultGraphEndpoint = "https://graph.windows.net";

        /// <summary>
        /// The azure active directory application secret.
        /// </summary>
        private readonly string applicationSecret;

        /// <summary>
        /// The application domain in azure active directory.
        /// </summary>
        private readonly string aadApplicationDomain;

        /// <summary>
        /// The active directory authentication endpoint.
        /// </summary>
        private readonly string activeDirectoryAuthority;

        /// <summary>
        /// The graph API endpoint.
        /// </summary>
        private readonly string graphApiEndpoint;
        
        /// <summary>
        /// Initializes static members of the <see cref="ApplicationPartnerCredentials"/> class.
        /// </summary>
        static ApplicationPartnerCredentials()
        {
            // register with the refresh credentials delegate in order to be able to refresh stale JWT tokens
            PartnerService.Instance.RefreshCredentials += OnCredentialsRefreshNeededAsync;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationPartnerCredentials"/> class.
        /// </summary>
        /// <param name="aadApplicationId">The application Id in Azure Active Directory.</param>
        /// <param name="aadApplicationSecret">The application secret in Azure Active Directory.</param>
        /// <param name="aadApplicationDomain">The application domain in Azure Active Directory.</param>
        public ApplicationPartnerCredentials(string aadApplicationId, string aadApplicationSecret, string aadApplicationDomain)
            : this(aadApplicationId, aadApplicationSecret, aadApplicationDomain, ApplicationPartnerCredentials.DefaultAadAuthority, ApplicationPartnerCredentials.DefaultGraphEndpoint)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationPartnerCredentials"/> class.
        /// </summary>
        /// <param name="aadApplicationId">The application Id in Azure Active Directory.</param>
        /// <param name="aadApplicationSecret">The application secret in Azure Active Directory.</param>
        /// <param name="aadApplicationDomain">The application domain in Azure Active Directory.</param>
        /// <param name="aadAuthorityEndpoint">The active directory authority endpoint.</param>
        /// <param name="graphApiEndpoint">The AAD graph API endpoint.</param>
        public ApplicationPartnerCredentials(string aadApplicationId, string aadApplicationSecret, string aadApplicationDomain, string aadAuthorityEndpoint, string graphApiEndpoint) : base(aadApplicationId)
        {
            if (string.IsNullOrWhiteSpace(aadApplicationSecret))
            {
                throw new ArgumentException("aadApplicationSecret has to be set");
            }

            if (string.IsNullOrWhiteSpace(aadApplicationDomain))
            {
                throw new ArgumentException("aadApplicationDomain has to be set");
            }

            if (string.IsNullOrWhiteSpace(aadAuthorityEndpoint))
            {
                throw new ArgumentException("aadAuthorityEndpoint has to be set");
            }

            if (string.IsNullOrWhiteSpace(graphApiEndpoint))
            {
                throw new ArgumentException("graphApiEndpoint has to be set");
            }

            this.applicationSecret = aadApplicationSecret;
            this.aadApplicationDomain = aadApplicationDomain;
            this.activeDirectoryAuthority = aadAuthorityEndpoint;
            this.graphApiEndpoint = graphApiEndpoint;
        }

        /// <summary>
        /// Authenticates with the partner service.
        /// </summary>
        /// <param name="requestContext">An optional request context.</param>
        /// <returns>A task that is complete when the authentication is complete.</returns>
        public override async Task AuthenticateAsync(IRequestContext requestContext = null)
        {
            // get the application AAD token
            var activeDirectoryEndpoint = new UriBuilder(this.activeDirectoryAuthority)
            {
                Path = this.aadApplicationDomain,
            };

            var scopes = new string[] { $"{this.graphApiEndpoint}/.default" };
            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder.Create(this.ClientId)
                .WithAuthority(activeDirectoryEndpoint.Uri.AbsoluteUri, false)
                .WithClientSecret(this.applicationSecret)
                .Build();

            var builder = confidentialClientApplication.AcquireTokenForClient(scopes);

            if (requestContext != null)
            {
                builder.WithCorrelationId(requestContext.CorrelationId);
            }

            var authResult = await builder.ExecuteAsync();
            this.AADToken = new AuthenticationToken(authResult.AccessToken, authResult.ExpiresOn);
        }

        /// <summary>
        /// Called when a partner credentials instance needs to be refreshed.
        /// </summary>
        /// <param name="credentials">The outdated partner credentials.</param>
        /// <param name="context">The partner context.</param>
        /// <returns>A task that is complete when the credential refresh is complete.</returns>
        private static async Task OnCredentialsRefreshNeededAsync(IPartnerCredentials credentials, IRequestContext context)
        {
            ApplicationPartnerCredentials partnerCredentials = credentials as ApplicationPartnerCredentials;

            if (partnerCredentials != null)
            {
                // we can deal with the partner credentials object, refresh it
                await partnerCredentials.AuthenticateAsync(context);
            }
            else
            {
                LogManager.Instance.Warning(typeof(ApplicationPartnerCredentials) + ": The given credentials are not supported.");
            }
        }
    }
}
