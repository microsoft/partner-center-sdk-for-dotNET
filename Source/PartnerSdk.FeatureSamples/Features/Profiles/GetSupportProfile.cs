// -----------------------------------------------------------------------
// <copyright file="GetSupportProfile.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Profiles
{
    using System;
    using System.Collections.Generic;
    using Models.Partners;

    /// <summary>
    /// Showcases get support profile.
    /// </summary>
    internal class GetSupportProfile : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Support Profile"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {            
            SupportProfile supportProfile = partnerOperations.Profiles.SupportProfile.Get();

            Console.Out.WriteLine("Support email: " + supportProfile.Email);
            Console.Out.WriteLine("Support website: " + supportProfile.Website);
            Console.Out.WriteLine("Support telephone: " + supportProfile.Telephone);
        }
    }
}
