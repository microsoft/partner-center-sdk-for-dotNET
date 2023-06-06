// -----------------------------------------------------------------------
// <copyright file="PromotionEligibilityError.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.PromotionEligibilities
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    ///  This class represents a model of a promotion eligibility error object.
    /// </summary>
    [JsonConverter(typeof(PromotionEligibilityErrorConverter))]
    public class PromotionEligibilityError
    {
        /// <summary>
        /// Gets or sets the type of error.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the description of the error.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Json converter for this class.
        /// </summary>
        private class PromotionEligibilityErrorConverter : JsonConverter
        {
            /// <summary>
            /// The can convert method which tests whether or not we can convert the type to the promotion eligibility error object.
            /// </summary>
            /// <param name="objectType">The object type.</param>
            /// <returns>A flag indicating whether or not the obect can be converted.</returns>
            public override bool CanConvert(Type objectType)
            {
                return typeof(PromotionEligibilityError).IsAssignableFrom(objectType);
            }

            /// <summary>
            /// Reads the json and converts to proper promotion eligibility error concrete class.
            /// </summary>
            /// <param name="reader">The json reader.</param>
            /// <param name="objectType">The object type.</param>
            /// <param name="existingValue">The existing value.</param>
            /// <param name="serializer">The serializer.</param>
            /// <returns>The object.</returns>
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                object target = new PromotionEligibilityError();
                var jsonObject = JObject.Load(reader);

                if (jsonObject.TryGetValue("type", out JToken eligibilityErrorType))
                {
                    var type = eligibilityErrorType.ToString();

                    if (!string.IsNullOrWhiteSpace(type))
                    {
                        switch (type.ToLowerInvariant())
                        {
                            case "invalidcatalogitemid":
                                target = new InvalidCatalogItemIdPromotionEligibilityError();
                                break;
                            case "invalidpromotion":
                                target = new InvalidPromotionEligibilityError();
                                break;
                            case "prerequisiteproductownership":
                                target = new PrerequisiteProductOwnershipPromotionEligibilityError();
                                break;
                            case "redemptionlimit":
                                target = new RedemptionLimitPromotionEligibilityError();
                                break;
                            case "seatcount":
                                target = new SeatCountPromotionEligibilityError();
                                break;
                            case "offerpurchasedpreviously":
                                target = new OfferPurchasedPreviouslyPromotionEligibilityError();
                                break;
                            case "term":
                                target = new TermPromotionEligibilityError();
                                break;
                            case "nopromotionsavailable":
                                target = new NoPromotionsAvailableEligibilityError();
                                break;
                            default:
                                target = new PromotionEligibilityError();
                                break;
                        }
                    }
                }

                serializer.Populate(jsonObject.CreateReader(), target);
                return target;
            }

            /// <summary>
            /// Writes the json.
            /// </summary>
            /// <param name="writer">The json writer.</param>
            /// <param name="value">The value object.</param>
            /// <param name="serializer">The json serializer.</param>
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
