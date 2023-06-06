// -----------------------------------------------------------------------
// <copyright file="UpdateSupportProfile.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Models.Partners;

    /// <summary>
    /// Showcases Update Support Profile.
    /// </summary>
    internal class UpdateSupportProfile : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Update Support Profile";
            }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            SupportProfile supportProfile = partnerOperations.Profiles.SupportProfile.Get();
            Console.Out.WriteLine("========================== Existing Support Profile ==========================");
            Console.Out.WriteLine("Support email: " + supportProfile.Email);
            Console.Out.WriteLine("Support website: " + supportProfile.Website);
            Console.Out.WriteLine("Support telephone: " + supportProfile.Telephone);

            SupportProfile newSupportProfile = new SupportProfile
            {
                Email = supportProfile.Email,
                Website = supportProfile.Website,
                Telephone = new Random().Next(10000000, 99999999).ToString(CultureInfo.InvariantCulture)
            };

            Console.Out.WriteLine("New Support telephone: " + newSupportProfile.Telephone);

            SupportProfile updatedSupportProfile = partnerOperations.Profiles.SupportProfile.Update(newSupportProfile);
            Console.Out.WriteLine("========================== Updated Support Profile ==========================");
            Console.Out.WriteLine("Support email: " + updatedSupportProfile.Email);
            Console.Out.WriteLine("Support website: " + updatedSupportProfile.Website);
            Console.Out.WriteLine("Support telephone: " + updatedSupportProfile.Telephone);
        }
    }
}
