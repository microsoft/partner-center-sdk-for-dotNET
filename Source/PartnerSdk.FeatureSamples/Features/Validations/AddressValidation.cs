// -----------------------------------------------------------------------
// <copyright file="AddressValidation.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Domains
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Customers;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Showcases address validation.
    /// </summary>
    internal class AddressValidation : IUnitOfWork
    {
        /// <summary>
        /// The different states of the modern AVS.
        /// </summary>
        private static readonly List<string> AVSStates = new List<string> 
        {
            "VerifiedShippable",
            "Verified",
            "InteractionRequired",
            "StreetPartial",
            "PremisesPartial",
            "Multiple",
            "None",
            "NotValidated"
        };

        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Address Validation"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var requestObjects = GetAddressSet();
         
            foreach (var addressRequest in requestObjects)
            {
                Console.WriteLine("Start AVS Case: {0}", addressRequest.Key);
                Display(addressRequest.Key, addressRequest.Value, partnerOperations.Validations.IsAddressValid(addressRequest.Value));
                Console.WriteLine("End AVS Case: {0}", addressRequest.Key);
                Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+\n");
            }
        }

        /// <summary>
        /// Method to display the address and the API result.
        /// </summary>
        /// <param name="addressCase">The address case under test.</param>
        /// <param name="originalAddress">The original request object.</param>
        /// <param name="result">The address validation response result.</param>
        private static void Display(string addressCase, Address originalAddress, AddressValidationResponse result)
        {
            Console.ForegroundColor = result.Status.Equals(addressCase, StringComparison.OrdinalIgnoreCase) ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine("Validated Result Case of {0}: {1}", addressCase, result.Status.Equals(addressCase, StringComparison.OrdinalIgnoreCase));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Count of Suggested Addresses Returned: {0}", result.SuggestedAddresses.Count);
            Console.WriteLine("Validation Message Returned: {0}", result?.ValidationMessage ?? "No message returned.");
            
            Console.WriteLine("Original Address:\n");
            DisplayAddress(originalAddress);

            if (result.SuggestedAddresses.Any())
            {
                Console.WriteLine("Suggested Addresses:\n");
                result.SuggestedAddresses.ForEach(sa => DisplayAddress(sa));
            }
            else
            {
                Console.WriteLine("Suggested Addresses: None to display.");
            }
        }

        /// <summary>
        /// Displays an address object.
        /// </summary>
        /// <param name="address">The address object.</param>
        private static void DisplayAddress(Address address)
        {            
            Console.WriteLine("{0}\n{1}\n{2}\n{3}\n", address.AddressLine1, address.City, address.Country, address.PostalCode);
        }

        /// <summary>
        /// Constructs the various address objects to send in the request body.
        /// </summary>
        /// <returns>A dictionary paired with the test case as keys and the request object as values.</returns>
        private static Dictionary<string, Address> GetAddressSet()
        {
            var addressSet = new Dictionary<string, Address>();

            foreach (var addressCase in AVSStates)
            {
                Address requestAddress = null;

                switch (addressCase)
                {
                    case "Verified":
                        requestAddress = GetAddress(alternatePostalCode: "98052-8300");
                        break;
                    case "InteractionRequired":
                        requestAddress = GetAddress(alternateCity: "Seattle");
                        break;
                    case "StreetPartial":
                        requestAddress = GetAddress(alternateLine1: "Microsoft Way");
                        break;
                    case "PremisesPartial":
                        requestAddress = GetAddress(alternateLine1: "3 Microsoft Way");
                        break;
                    case "None":
                        requestAddress = GetAddress(alternateCity: "Seattle", alternatePostalCode: "98055");
                        break;
                    case "NotValidated":
                        requestAddress = GetAddress(alternateCountry: string.Empty);
                        break;
                    case "Multiple":
                        requestAddress = new Address { AddressLine1 = "1 Strada Dacia", City = "Bulboac", PostalCode = "6512", Region = "Anenii Noi", Country = "MD" };
                        break;
                    default:
                        requestAddress = GetAddress();
                        break;
                }

                addressSet.Add(addressCase, requestAddress);
            }

            return addressSet;
        }

        /// <summary>
        /// Helper method to construct Address objects.
        /// </summary>
        /// <param name="alternateLine1">The address line.</param>
        /// <param name="alternateCity">The city.</param>
        /// <param name="alternatePostalCode">The postal code.</param>
        /// <param name="alternateCountry">The country.</param>
        /// <returns>The address object.</returns>
        private static Address GetAddress(string alternateLine1 = "1 Microsoft Way", string alternateCity = "Redmond", string alternatePostalCode = "98052", string alternateCountry = "US")
        {
            return new Address
            {
                AddressLine1 = alternateLine1,
                City = alternateCity,
                State = "WA",
                PostalCode = alternatePostalCode,
                Country = alternateCountry
            };
        }
    }
}
