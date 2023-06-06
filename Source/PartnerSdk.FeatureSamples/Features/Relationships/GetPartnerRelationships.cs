// -----------------------------------------------------------------------
// <copyright file="GetPartnerRelationships.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Relationships
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models.Customers;
    using Models.Query;
    using Models.Relationships;

    /// <summary>
    /// Test get partner relationship scenario
    /// </summary>
    internal class GetPartnerRelationships : IUnitOfWork
    {
        /// <summary>
        /// Enables running the tests.
        /// </summary>
        private readonly bool testMultiTier;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPartnerRelationships"/> class.
        /// </summary>
        /// <param name="testMultiTier">true, runs the tests</param>
        public GetPartnerRelationships(bool testMultiTier)
        {
            this.testMultiTier = testMultiTier;
        }
        
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get Partner Relationships"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            if (!this.testMultiTier)
            {
                Console.WriteLine("Skipping tests.");
                return;
            }

            var valueAddedResellers = partnerOperations.Relationships.Get(PartnerRelationshipType.IsIndirectCloudSolutionProviderOf);

            foreach (var reseller in valueAddedResellers.Items)
            {
                Console.WriteLine("Reseller Id: {0}", reseller.Id);
                Console.WriteLine("Reseller Name: {0}", reseller.Name);
                Console.WriteLine("Reseller MPN Id: {0}", reseller.MpnId);
                Console.WriteLine("Reseller Location: {0}", reseller.Location);
                Console.WriteLine("Relationship type: {0}", reseller.RelationshipType);
                Console.WriteLine("Relationship state: {0}", reseller.State);
                Console.WriteLine();
            }

            Console.WriteLine("Filter by partner name");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();

            FieldFilter filter = new SimpleFieldFilter(PartnerRelationshipSearchField.Name.ToString(), FieldFilterOperation.Substring, "ArtemisAdvisor");
            valueAddedResellers = partnerOperations.Relationships.Query(PartnerRelationshipType.IsIndirectCloudSolutionProviderOf, QueryFactory.Instance.BuildSimpleQuery(filter));

            foreach (var reseller in valueAddedResellers.Items)
            {
                Console.WriteLine("Reseller Id: {0}", reseller.Id);
                Console.WriteLine("Reseller Name: {0}", reseller.Name);
                Console.WriteLine("Reseller MPN Id: {0}", reseller.MpnId);
                Console.WriteLine("Reseller Location: {0}", reseller.Location);
                Console.WriteLine("Relationship type: {0}", reseller.RelationshipType);
                Console.WriteLine();
            }

            var selectedVar = valueAddedResellers.Items.FirstOrDefault();

            if (selectedVar != null)
            {
                Console.WriteLine("Get list of customers for Value Added Reseller : {0}", selectedVar.Name);
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();

                filter = new SimpleFieldFilter(CustomerSearchField.IndirectReseller.ToString(), FieldFilterOperation.StartsWith, selectedVar.Id);

                var customers = partnerOperations.Customers.Query(QueryFactory.Instance.BuildSimpleQuery(filter));

                foreach (var customer in customers.Items)
                {
                    Console.WriteLine("Id: {0}", customer.Id);
                    Console.WriteLine("Name: {0}", customer.CompanyProfile.CompanyName);
                    Console.WriteLine("Domain: {0}", customer.CompanyProfile.Domain);
                    Console.WriteLine();
                }
            }
        }
    }
}
