// -----------------------------------------------------------------------
// <copyright file="UploadOrderAttachments.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.OrderAttachments
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using Microsoft.AspNetCore.Http.Internal;
    using Models.Orders;
    using Newtonsoft.Json;

    /// <summary>
    /// Showcases getting all order attachments.
    /// </summary>
    internal class UploadOrderAttachments : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Upload Order Attachments"; }
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

            // Create a dummy file to upload
            var fileContent = Encoding.UTF8.GetBytes("This is a test file1 for PO content");

            var file = new FormFile(
                        baseStream: new MemoryStream(fileContent),
                        baseStreamOffset: 0,
                        length: fileContent.Length,
                        name: "Mock_Data",
                        fileName: "TestFile_PO_1.pdf");

            Stream fileStream = file?.OpenReadStream();
            MultipartFormDataContent multiContent = new MultipartFormDataContent();

            var streamContent = new StreamContent(fileStream);
            streamContent.Headers.Add("Content-Type", "application/pdf");
            streamContent.Headers.Add("Content-Disposition", "form-data; name=\"POFiles\"; filename=\"" + file.FileName + "\"");
            multiContent.Add(streamContent, "POFiles", file.FileName);

            var attachmentMetadata = new AttachmentMetadata()
            {
                Currency = "USD",
                CustomerPrice = "100",
                FxRate = "1",
                IsPartOfTender = false,
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(attachmentMetadata), System.Text.Encoding.UTF8, "application/json");
            stringContent.Headers.Add("Content-Disposition", "form-data; name=\"metadata\"");
            multiContent.Add(stringContent, "metadata");

            var orderAttachments = partnerOperations.Customers.ById(selectedCustomerId).Orders.ById(selectedOrderId).Attachments.Upload(multiContent);

            // display the orders
            Console.Out.WriteLine("Order attachment count: " + orderAttachments.Items.Count());

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
