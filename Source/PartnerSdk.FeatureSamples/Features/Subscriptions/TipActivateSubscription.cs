// <copyright file="TipActivateSubscription.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Subscriptions
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Security;
    using System.Threading.Tasks;
    using Microsoft.Identity.Client;
    using Microsoft.Store.PartnerCenter.Extensions;

    /// <summary>
    /// TIP Subscription Activation.
    /// </summary>
    internal class TipActivateSubscription : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title => "Activates a 3PP SaaS Sandbox subscription";

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // since this operation can only be performed on TIP accounts, we need to login to a TIP account here
            // and ignore the given partner operations which relies on a stadard account
            string authority = ConfigurationManager.AppSettings["aad.authority"];
            string resourceUrl = ConfigurationManager.AppSettings["aad.resourceUrl"];
            string organizationsDomain = ConfigurationManager.AppSettings["aad.organizationsDomain"];
            string clientId = ConfigurationManager.AppSettings["tipAccount.aad.clientId"];
            string userName = ConfigurationManager.AppSettings["tipAccount.aad.userName"];
            string password = ConfigurationManager.AppSettings["tipAccount.aad.password"];

            var addAuthority = new UriBuilder(authority)
            {
                Path = organizationsDomain
            };

            var task = Task.Run(async () => await this.GetAADToken(addAuthority.Uri.AbsoluteUri, clientId, resourceUrl, userName, password));
            var authenticationResult = task.Result;

            IPartnerCredentials tipAccountCredentials = PartnerCredentials.Instance.GenerateByUserCredentials(
                ConfigurationManager.AppSettings["tipAccount.application.id"],
                new AuthenticationToken(
                    authenticationResult.AccessToken,
                    authenticationResult.ExpiresOn));
            
            IPartner tipAccountPartnerOperations = PartnerService.Instance.CreatePartnerOperations(tipAccountCredentials);

            Console.WriteLine("Enter the AAD tenant ID of the customer:");
            string customerTenantId = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Enter the Subscription id to activate:");
            string subscriptionId = Console.ReadLine();

            var subscriptionActivationResult = tipAccountPartnerOperations.Customers.ById(customerTenantId).Subscriptions.ById(subscriptionId).Activate();

            Console.WriteLine($"Subscription Activation Status - {subscriptionActivationResult.Status}");
        }

        /// <summary>
        /// Get AAD token using MSAL Public Client and userName, password.
        /// </summary>
        /// <param name="authority">The Authority to request token from.</param>
        /// <param name="clientId">The ClientId of the AAD Application.</param>
        /// <param name="resource">The ResourceUrl for which the token needs to be generated.</param>
        /// <param name="userName">The Username.</param>
        /// <param name="password">The Password.</param>
        /// <returns name="Task{AuthenticationResult}">The Authentication Result.</returns>
        private async Task<AuthenticationResult> GetAADToken(string authority, string clientId, string resource, string userName, string password)
        {
            var scopes = new string[] { $"{resource}/.default" };

            IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder.Create(clientId)
                    .WithAuthority(authority)
                    .Build();

            var accounts = await publicClientApplication.GetAccountsAsync();

            if (accounts.Any())
            {
                return await publicClientApplication.AcquireTokenSilent(scopes, accounts.FirstOrDefault())
                                  .ExecuteAsync();
            }
            else
            {
                try
                {
                    var securePassword = new SecureString();
                    foreach (char c in password)
                    {
                        securePassword.AppendChar(c);
                    }

                    var res = await publicClientApplication.AcquireTokenByUsernamePassword(scopes, userName, securePassword)
                                       .ExecuteAsync();

                    return res;
                }
                catch (MsalException e)
                {
                    throw e;
                }
            }
        }
    }
}
