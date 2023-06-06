// -----------------------------------------------------------------------
// <copyright file="ArtifactConverter.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.JsonConverters
{
    using System;
    using Entitlements;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    
    /// <summary>
    /// Serialize and deserialize entitlement artifact to correct instance based on artifact type.
    /// </summary>
    public class ArtifactConverter : JsonConverter
    {
        /// <inheritdoc />
        public override bool CanWrite => false;

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // We do not need converter for writing json from here.
            // CanWrite returns false on this converter
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            object target = (jsonObject["artifactType"].ToString().ToLowerInvariant() == "reservedinstance") ? new ReservedInstanceArtifact() : new Artifact();

            serializer.Populate(jsonObject.CreateReader(), target);
            return target;
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return typeof(Artifact).IsAssignableFrom(objectType);
        }
    }
}
