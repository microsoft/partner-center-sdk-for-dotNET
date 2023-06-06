// -----------------------------------------------------------------------
// <copyright file="IOrderAttachment.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Orders
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Holds operations that apply Order attachments.
    /// </summary>
    public interface IOrderAttachment : IPartnerComponent<Tuple<string, string, string>>
    {
        /// <summary>
        /// Retrieves the order attachment.
        /// </summary>
        /// <returns>The order attachments.</returns>
        Stream Download();

        /// <summary>
        /// Asynchronously retrieves the order attachment.
        /// </summary>
        /// <returns>The order attachment.</returns>
        Task<Stream> DownloadAsync();
    }
}
