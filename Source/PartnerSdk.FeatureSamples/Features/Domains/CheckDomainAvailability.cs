// -----------------------------------------------------------------------
// <copyright file="CheckDomainAvailability.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Domains
{
    using System;
    using System.Collections.Generic;
    using Exceptions;

    /// <summary>
    /// Showcases checking domain availability.
    /// </summary>
    internal class CheckDomainAvailability : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Check Domain Availability"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            const string Suffix = ".ccsctp.net";
            string domain = string.Concat("abc123", Suffix);
            bool result = partnerOperations.Domains.ByDomain(domain).Exists();
            Display(domain, result.ToString());

            domain = string.Concat(Guid.NewGuid().ToString("N").Substring(0, 20), Suffix);
            result = partnerOperations.Domains.ByDomain(domain).Exists();
            Display(domain, result.ToString());

            try
            {
                domain = string.Concat("abc!", Suffix);
                result = partnerOperations.Domains.ByDomain(domain).Exists();
                Display(domain, "ERROR: Should have thrown exception - BadRequest(400)!");
            }
            catch (PartnerException exception)
            {
                if (exception.ErrorCategory == PartnerErrorCategory.BadInput)
                {
                    Display(domain, exception.ErrorCategory.ToString());
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Method to display the domain and the API result.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <param name="result">The result.</param>
        private static void Display(string domain, string result)
        {
            Console.WriteLine("Domain : {0}", domain);
            Console.WriteLine("Result : {0}", result);
            Console.WriteLine();
        }
    }
}
