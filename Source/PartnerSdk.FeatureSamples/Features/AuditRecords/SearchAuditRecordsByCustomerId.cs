// <copyright file="SearchAuditRecordsByCustomerId.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.Invoices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Auditing;
    using Models.Query;
    using RequestContext;

    /// <summary>
    /// Showcases searching audit records by ResourceType.
    /// </summary>
    internal class SearchAuditRecordsByCustomerId : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Search Audit Records by Customer Id"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // all the operations executed on this partner operation instance will share the same correlation Id but will differ in request Id
            IPartner scopedPartnerOperations = partnerOperations.With(RequestContextFactory.Instance.Create(Guid.NewGuid()));

            var customerId = "1780da95-3a7c-46fd-b9e2-693efbae4ac7";

            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
            var filter = new SimpleFieldFilter(AuditRecordSearchField.CustomerId.ToString(), FieldFilterOperation.Equals, customerId);
            var collection = scopedPartnerOperations.AuditRecords.Query(startDate.Date, query: QueryFactory.Instance.BuildSimpleQuery(filter: filter));
            var enumerator = scopedPartnerOperations.Enumerators.AuditRecords.Create(collection);

            while (enumerator.HasValue)
            {
                Console.Out.WriteLine("Record count: " + enumerator.Current.TotalCount);

                foreach (var record in enumerator.Current.Items)
                {
                    Console.Out.WriteLine("PartnerId:              {0}", record.PartnerId);
                    Console.Out.WriteLine("Customer Id:            {0}", record.CustomerId);
                    Console.Out.WriteLine("Customer Name:          {0}", record.CustomerName);
                    Console.Out.WriteLine("Resource Type:          {0}", record.ResourceType);
                    Console.Out.WriteLine("Date:                   {0:yyyy-MM-dd}", record.OperationDate);
                    Console.Out.WriteLine("Operation Type:         {0}", record.OperationType);
                    Console.Out.WriteLine("Status:                 {0}", record.OperationStatus);
                    Console.Out.WriteLine("OriginalCorrelationId:  {0}", record.OriginalCorrelationId);
                    Console.Out.WriteLine("User:                   {0}", record.UserPrincipalName);
                    Console.Out.WriteLine("Application:            {0}", record.ApplicationId);
                    Console.Out.WriteLine("New Value:              {0}", record.ResourceNewValue);
                    Console.Out.WriteLine();
                }

                enumerator.Next();
            }
        }
    }
}
