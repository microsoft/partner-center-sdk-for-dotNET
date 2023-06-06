// -----------------------------------------------------------------------
// <copyright file="DownloadOrderAttachment.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.OrderAttachments
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Showcases downloading order attachment.
    /// </summary>
    internal class DownloadOrderAttachment : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Download Order Attachment"; }
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

            string selectedAttachmentId = state[FeatureSamplesApplication.OrderAttachmentId] as string;

            string selectedOrderId = state[FeatureSamplesApplication.OrderIdForAttachments] as string;

            var orderAttachment = partnerOperations.Customers.ById(selectedCustomerId).Orders.ById(selectedOrderId).Attachments.ById(selectedAttachmentId).Download();

            var filePath = "C:\\test";
            var fileName = string.Format("C:\\test\\PO_{0}.pdf", selectedAttachmentId);

            bool exists = Directory.Exists(filePath);

            if (!exists)
            {
                Directory.CreateDirectory(filePath);
            }

            using (var fileStream = File.Create(fileName))
            {
                orderAttachment.CopyTo(fileStream);
            }

            Console.Out.WriteLine("\tAttachment Id: {0}", selectedAttachmentId);
            Console.Out.WriteLine("\tSize: {0} bytes", orderAttachment.Length);
            Console.Out.WriteLine("\tAttachment downloaded in this location: {0}", fileName);
        }
    }
}
