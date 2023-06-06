// -----------------------------------------------------------------------
// <copyright file="UserPartnerCredentials.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Exceptions;
    using Logging;
    using RequestContext;

    /// <summary>
    /// Partner service credentials based on azure active directory user credentials.
    /// </summary>
    internal class UserPartnerCredentials : BasePartnerCredentials
    {
        /// <summary>
        /// The delegate used to refresh the azure active directory token.
        /// </summary>
        private readonly TokenRefresher tokenRefresher;

        /// <summary>
        /// Initializes static members of the <see cref="UserPartnerCredentials"/> class.
        /// </summary>
        static UserPartnerCredentials()
        {
            // register with the refresh credentials delegate in order to be able to refresh stale JWT tokens
            PartnerService.Instance.RefreshCredentials += OnCredentialsRefreshNeededAsync;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserPartnerCredentials"/> class.
        /// </summary>
        /// <param name="aadApplicationId">The id of the application in azure active directory.</param>
        /// <param name="aadAuthenticationToken">The azure active directory token.</param>
        /// <param name="aadTokenRefresher">An optional delegate which will be called when the azure active directory token
        /// expires and can no longer be used to generate the partner credentials. This delegate should return
        /// an up to date azure active directory token.</param>
        public UserPartnerCredentials(string aadApplicationId, AuthenticationToken aadAuthenticationToken, TokenRefresher aadTokenRefresher = null) : base(aadApplicationId)
        {
            if (aadAuthenticationToken == null)
            {
                throw new ArgumentNullException("aadAuthenticationToken");
            }
            else
            {
                if (aadAuthenticationToken.IsExpired())
                {
                    throw new ArgumentException("aadAuthenticationToken is expired.");
                }
            }

            this.AADToken = aadAuthenticationToken;
            this.tokenRefresher = aadTokenRefresher;
        }

        /// <summary>
        /// Called when a partner credentials instance needs to be refreshed.
        /// </summary>
        /// <param name="credentials">The outdated partner credentials.</param>
        /// <param name="context">The partner context.</param>
        /// <returns>A task that is complete when the credential refresh is done.</returns>
        private static async Task OnCredentialsRefreshNeededAsync(IPartnerCredentials credentials, IRequestContext context)
        {
            UserPartnerCredentials partnerCredentials = credentials as UserPartnerCredentials;

            if (partnerCredentials != null)
            {
                // we can deal with the partner credentials object, refresh it
                await partnerCredentials.RefreshAsync(context);
            }
            else
            {
                LogManager.Instance.Warning(typeof(UserPartnerCredentials) + ": The given credentials are not supported.");
            }
        }

        /// <summary>
        /// Refreshes the partner credentials.
        /// </summary>
        /// <param name="context">The partner context.</param>
        /// <returns>A task which is complete when the refresh is done.</returns>
        private async Task RefreshAsync(IRequestContext context)
        {
            if (this.AADToken.IsExpired())
            {
                // we need to refresh the AAD before attempting to re-authenticate with the partner service
                if (this.tokenRefresher != null)
                {
                    // invoke the callback and let it provide us with the new aad token
                    var newAadToken = await this.tokenRefresher(this.AADToken);

                    if (newAadToken == null)
                    {
                        const string ErrorMessage = "Token refresher returned null token.";
                        LogManager.Instance.Error(ErrorMessage);
                        throw new PartnerException(ErrorMessage, context, PartnerErrorCategory.Unauthorized);
                    }

                    if (newAadToken.IsExpired())
                    {
                        const string ErrorMessage = "Token refresher returned an expired token.";
                        LogManager.Instance.Error(ErrorMessage);
                        throw new PartnerException(ErrorMessage, context, PartnerErrorCategory.Unauthorized);
                    }

                    this.AADToken = newAadToken;
                }
                else
                {
                    const string ErrorMessage = "AAD Token needs refreshing but no handler was registered.";
                    LogManager.Instance.Warning(ErrorMessage);
                    throw new PartnerException(ErrorMessage, context, PartnerErrorCategory.Unauthorized);
                }
            }

            // get a new partner service token using the AAD token we have
            await this.AuthenticateAsync(context);
        }
    }
}