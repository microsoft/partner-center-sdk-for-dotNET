//----------------------------------------------------------------
// <copyright file="InvoiceLineItemConverter.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.JsonConverters
{
    using System;
    using System.Globalization;
    using Invoices;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Serialize and Deserialize InvoiceLineItem to correct InvoiceLineItem instance.
    /// </summary>
    internal class InvoiceLineItemConverter : JsonConverter
    {
        /// <summary>
        /// Gets a value indicating whether this instance can write.
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(InvoiceLineItem).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// reader
        /// or
        /// objectType
        /// </exception>
        /// <exception cref="Newtonsoft.Json.JsonSerializationException">
        /// </exception>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            if (objectType == null)
            {
                throw new ArgumentNullException("objectType");
            }

            if (!this.CanConvert(objectType))
            {
                throw new JsonSerializationException(string.Format(CultureInfo.InvariantCulture, "InvoiceLineItemConverter cannot deserialize '{0}' values", objectType.Name));
            }

            JObject jsonObject = JObject.Load(reader);
            object target = null;

            string billingProvider = jsonObject["billingProvider"] != null ? jsonObject["billingProvider"].ToString() : string.Empty;
            string invoiceLineItemType = jsonObject["invoiceLineItemType"] != null ? jsonObject["invoiceLineItemType"].ToString() : string.Empty;

            var provider = JsonConvert.DeserializeObject<BillingProvider>(string.Format(CultureInfo.InvariantCulture, "'{0}'", billingProvider));

            switch (invoiceLineItemType)
            {
                case "usage_line_items":
                    if (provider == BillingProvider.Azure)
                    {
                        target = new DailyUsageLineItem();
                    }

                    if (provider == BillingProvider.Marketplace)
                    {
                        target = new DailyRatedUsageLineItem();
                    }

                    break;
                case "billing_line_items":
                    if (provider == BillingProvider.Azure)
                    {
                        target = new UsageBasedLineItem();
                    }

                    if (provider == BillingProvider.Office)
                    {
                        target = new LicenseBasedLineItem();
                    }

                    if (provider == BillingProvider.OneTime)
                    {
                        target = new OneTimeInvoiceLineItem();
                    }

                    if (provider == BillingProvider.All)
                    {
                        target = new OneTimeInvoiceLineItem();
                    }

                    break;
                default:
                    throw new JsonSerializationException(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "InvoiceLineItemConverter cannot deserialize invoice line items with type: '{0}'",
                            invoiceLineItemType));
            }

            if (target == null)
            {
                throw new JsonSerializationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "InvoiceLineItemConverter cannot deserialize invoice line items with type: '{0}' && billing provider: '{1}'",
                        invoiceLineItemType,
                        billingProvider));
            }

            serializer.Populate(jsonObject.CreateReader(), target);
            return target;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // We do not need converter for writing json from here.
            // CanWrite returns false on this converter
            throw new NotImplementedException();
        }
    }
}
