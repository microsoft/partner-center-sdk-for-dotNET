// -----------------------------------------------------------------------
// <copyright file="GetMpnProfile.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features
{
    using System;
    using System.Collections.Generic;
    using Models.Partners;

    /// <summary>
    /// Showcases partner network profile.
    /// </summary>
    internal class GetMpnProfile : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Microsoft Partner Network Profile"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            string selectedMpnId = state[FeatureSamplesApplication.SelectedMpnId] as string;
            Console.Out.WriteLine("Get Microsoft Partner Network Profile by MPN Id");
            Console.Out.WriteLine("Mpn Id {0}", selectedMpnId);

            MpnProfile mpnProfile = partnerOperations.Profiles.MpnProfile.Get(selectedMpnId);

            Console.Out.WriteLine("Mpn Id : {0}", mpnProfile.MpnId);
            Console.Out.WriteLine("Partner Name : {0}", mpnProfile.PartnerName);
            Console.Out.WriteLine();

            Console.Out.WriteLine("Get Microsoft Partner Network Profile of the logged in partner");
            mpnProfile = partnerOperations.Profiles.MpnProfile.Get();
            Console.Out.WriteLine("Partner's Mpn Id : {0}", mpnProfile.MpnId);
        }
    }
}
