// -----------------------------------------------------------------------
// <copyright file="GetOrderAttachments.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.OrderAttachments
{
    using System;
    using System.Collections.Generic;
    using Models.Orders;
    using Newtonsoft.Json;

    /// <summary>
    /// Showcases getting all order attachments.
    /// </summary>
    internal class GetOrderAttachments : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Get All Order Attachments"; }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // read from the application state
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerKey] as string;

            string selectedOrderId = state[FeatureSamplesApplication.OrderIdForAttachments] as string;

            var orderAttachments = partnerOperations.Customers.ById(selectedCustomerId).Orders.ById(selectedOrderId).Attachments.Get();

            // display the orders
            Console.Out.WriteLine("Order attachment count: " + orderAttachments.TotalCount);

            IList<OrderAttachment> orderAttachmentList = new List<OrderAttachment>(orderAttachments.Items);

            foreach (var orderAttachment in orderAttachmentList)
            {
                Console.Out.WriteLine("Attachment Id: {0}", orderAttachment.AttachmentId);
                Console.Out.WriteLine("Attachment Type: {0}", orderAttachment.AttachmentType);
                Console.Out.WriteLine("File Name: {0}", orderAttachment.FileName);
                Console.Out.WriteLine("Size in KB: {0}", orderAttachment.SizeInKB);
                Console.Out.WriteLine();
                Console.Out.WriteLine(JsonConvert.SerializeObject(orderAttachment));
                Console.Out.WriteLine();
            }
        }
    }
}
