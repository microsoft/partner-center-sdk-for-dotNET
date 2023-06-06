// -----------------------------------------------------------------------
// <copyright file="EnumJsonConverter.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.JsonConverters
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// Formats the Enum values to the format that we follow across commerce.
    /// </summary>
    internal class EnumJsonConverter : JsonConverter
    {
        /// <summary>
        /// Boolean value to indicate whether PascalToJscriptCase() conversion should be disabled or not.
        /// </summary>
        private readonly bool isPascalToJscriptCaseConversionDisabled;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumJsonConverter"/> class.
        /// </summary>
        public EnumJsonConverter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumJsonConverter"/> class.
        /// </summary>
        /// <param name="isPascalToJscriptCaseConversionDisabled">Boolean value to indicate if PascalToJscriptCase() conversion is disabled or not.</param>
        public EnumJsonConverter(bool isPascalToJscriptCaseConversionDisabled)
        {
            this.isPascalToJscriptCaseConversionDisabled = isPascalToJscriptCaseConversionDisabled;
        }

        /// <summary>
        /// Converts from JavaScript to Pascal case.
        /// </summary>
        /// <param name="jsonValue">The JSON value.</param>
        /// <returns>Pascal cased value</returns>
        public static string JScriptToPascalCase(string jsonValue)
        {
            Debug.Assert(jsonValue != null && jsonValue.Length > 0, "jsonValue should be not null and at least one letter long");

            if (jsonValue == null)
            {
                throw new ArgumentNullException("jsonValue");
            }

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(char.ToUpper(jsonValue[0], CultureInfo.InvariantCulture));
            for (int i = 1; i < jsonValue.Length; i++)
            {
                stringBuilder.Append(jsonValue[i] == '_'
                    ? char.ToUpper(jsonValue[++i], CultureInfo.InvariantCulture)
                    : jsonValue[i]);
            }

            return stringBuilder.ToString();
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
            return objectType != null && objectType.IsEnum;
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

            if (!objectType.IsEnum)
            {
                throw new JsonSerializationException(string.Format(CultureInfo.InvariantCulture, "EnumJsonConverter cannot deserialize '{0}' values", objectType.Name));
            }

            if (reader.TokenType == JsonToken.String)
            {
                var enumString = this.isPascalToJscriptCaseConversionDisabled ? reader.Value.ToString() : JScriptToPascalCase(reader.Value.ToString());
                if (Enum.IsDefined(objectType, enumString))
                {
                    return Enum.Parse(objectType, enumString);
                }
                else
                {
                    return Enum.ToObject(objectType, 0);
                }
            }
            else if (reader.TokenType == JsonToken.Integer)
            {
                return Enum.ToObject(objectType, reader.Value);
            }
            else
            {
                throw new JsonSerializationException(string.Format(CultureInfo.InvariantCulture, "EnumJsonConverter cannot deserialize '{0}' values", reader.TokenType));
            }
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            if (value != null)
            {
                writer.WriteValue(this.isPascalToJscriptCaseConversionDisabled ? value.ToString() : PascalToJscriptCase(value.ToString()));
            }
        }

        /// <summary>
        /// Converts from Pascal to JavaScript case.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>string enum value</returns>
        private static string PascalToJscriptCase(string enumValue)
        {
            Debug.Assert(enumValue != null && enumValue.Length > 0, "enumValue should be not null and at least one letter long");

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(char.ToLower(enumValue[0], CultureInfo.InvariantCulture));

            for (int i = 1; i < enumValue.Length; i++)
            {
                if (char.IsUpper(enumValue[i]))
                {
                    stringBuilder.Append("_");
                    stringBuilder.Append(char.ToLower(enumValue[i], CultureInfo.InvariantCulture));
                }
                else
                {
                    stringBuilder.Append(enumValue[i]);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
