﻿// -----------------------------------------------------------------------
// <copyright file="PartnerRelationship.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Relationships
{
    /// <summary>
    /// This represents a relationship between two partners.
    /// </summary>
    public class PartnerRelationship : ResourceBase
    {
        /// <summary>
        /// Gets or sets the partner identifier.
        /// The partner identifier specifies tenant id of the partner who is 
        /// in the recipient (from) side of relationship.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the partner who is in the recipient 
        /// (from) side of the relationship.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the relationship.
        /// </summary>
        public PartnerRelationshipType RelationshipType { get; set; }

        /// <summary>
        /// Gets or sets MPN Id.
        /// </summary>
        public string MpnId { get; set; }

        /// <summary>
        /// Gets or sets the location of the partner.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the state of the relationship.
        /// </summary>
        public string State { get; set; }
    }
}
