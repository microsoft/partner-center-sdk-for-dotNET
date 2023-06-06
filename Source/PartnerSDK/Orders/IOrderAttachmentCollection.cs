// -----------------------------------------------------------------------
// <copyright file="IOrderAttachmentCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Orders
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Store.PartnerCenter.GenericOperations;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Orders;

    /// <summary>
    /// Encapsulates customer order attaachment collection behavior.
    /// </summary>
    public interface IOrderAttachmentCollection : IEntireEntityCollectionRetrievalOperations<OrderAttachment, ResourceCollection<OrderAttachment>>, IEntitySelector<IOrderAttachment>
    {
        /// <summary>
        /// Gets a specific attachment behavior.
        /// </summary>
        /// <param name="attachmentId">The order attachment id.</param>
        /// <returns>The order attachment operations.</returns>
        new IOrderAttachment this[string attachmentId] { get; }

        /// <summary>
        /// Obtains a specific attachment behavior.
        /// </summary>
        /// <param name="attachmentId">The order attachment id.</param>
        /// <returns>The order attachment operations.</returns>
        new IOrderAttachment ById(string attachmentId);

        /// <summary>
        /// Uploads attachments for an order for the customer.
        /// </summary>
        /// <param name="attachmentsToUpload">The attachments information.</param>
        /// <returns>The uploaded order attachments.</returns>
        ResourceCollection<OrderAttachment> Upload(MultipartFormDataContent attachmentsToUpload);

        /// <summary>
        /// Asynchronously uploads attachments for an order for the customer.
        /// </summary>
        /// <param name="attachmentsToUpload">The attachment details.</param>
        /// <returns>The uploaded order attachments.</returns>
        Task<ResourceCollection<OrderAttachment>> UploadAsync(MultipartFormDataContent attachmentsToUpload);

        /// <summary>
        /// Retrieves all order attachments.
        /// </summary>
        /// <returns>The order attachments.</returns>
        new ResourceCollection<OrderAttachment> Get();

        /// <summary>
        /// Asynchronously retrieves all customer order attachments.
        /// </summary>
        /// <returns>The customer order attchments.</returns>
        new Task<ResourceCollection<OrderAttachment>> GetAsync();
    }
}
