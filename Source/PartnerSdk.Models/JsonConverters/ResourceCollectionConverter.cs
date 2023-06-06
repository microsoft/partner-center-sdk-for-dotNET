// -----------------------------------------------------------------------
// <copyright file="ResourceCollectionConverter.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.JsonConverters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Serialize and Deserialize ResourceCollection to correct ResourceCollection instance.
    /// </summary>
    /// <typeparam name="T">The type of the resource.</typeparam>
    internal class ResourceCollectionConverter<T> : JsonConverter
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
            return typeof(ResourceCollection<T>).IsAssignableFrom(objectType) ||
                typeof(SeekBasedResourceCollection<T>).IsAssignableFrom(objectType);
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
                throw new JsonSerializationException(string.Format(CultureInfo.InvariantCulture, "ResourceCollectionConverter cannot deserialize '{0}' values", objectType.Name));
            }

            JObject jsonObject = JObject.Load(reader);
            object target;

            if (objectType == typeof(SeekBasedResourceCollection<T>))
            {
                var continuationToken = jsonObject["continuationToken"] ?? string.Empty;
                target = new SeekBasedResourceCollection<T>(new List<T>(), continuationToken.ToString());
            }
            else
            {
                target = new ResourceCollection<T>(new List<T>());
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
