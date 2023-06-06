// -----------------------------------------------------------------------
// <copyright file="InternalPropertySetterJsonResolver.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.JsonConverters
{
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Enables the JSON library to write properties with internal setters.
    /// </summary>
    internal class InternalPropertySetterJsonResolver : DefaultContractResolver
    {
        /// <summary>
        /// Creates a JSON property.
        /// </summary>
        /// <param name="member">The property information.</param>
        /// <param name="memberSerialization">The member serialization information.</param>
        /// <returns>A JSON property.</returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);

            if (!jsonProperty.Writable)
            {
                var property = member as PropertyInfo;

                if (property != null)
                {
                    jsonProperty.Writable = property.GetSetMethod(true) != null;
                }
            }

            return jsonProperty;
        }
    }    
}