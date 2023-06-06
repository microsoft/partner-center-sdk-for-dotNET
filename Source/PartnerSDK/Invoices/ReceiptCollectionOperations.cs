// -----------------------------------------------------------------------
// <copyright file="ReceiptCollectionOperations.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Invoices
{
    using Utilities;

    /// <summary>
    /// Represents the operations that can be done on Partner's invoice receipts.
    /// </summary>
    internal class ReceiptCollectionOperations : BasePartnerComponent, IReceiptCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiptCollectionOperations"/> class.
        /// </summary>
        /// <param name="rootPartnerOperations">The root partner operations instance.</param>
        /// <param name="invoiceId">The invoice id.</param>
        public ReceiptCollectionOperations(IPartner rootPartnerOperations, string invoiceId)
            : base(rootPartnerOperations, invoiceId)
        {
        }

        /// <summary>
        /// Gets a single receipt operations.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>The invoice operations.</returns>
        public IReceipt this[string receiptId]
        {
            get
            {
                return this.ById(receiptId);
            }
        }

        /// <summary>
        /// Gets a single receipt operations.
        /// </summary>
        /// <param name="receiptId">The invoice id.</param>
        /// <returns>The invoice operations.</returns>
        public IReceipt ById(string receiptId)
        {
            ParameterValidator.Required(receiptId, "receiptId has to be set.");
            return new ReceiptOperations(this.Partner, this.Context, receiptId);
        }        
    }
}
