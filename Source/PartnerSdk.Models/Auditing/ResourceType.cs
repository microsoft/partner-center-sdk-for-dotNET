//----------------------------------------------------------------
// <copyright file="ResourceType.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Auditing
{
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Enumeration to represent type of
    /// Resource being performed
    /// </summary>
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum ResourceType
    {
        /// <summary>
        /// The undefined
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Customer Resource
        /// </summary>
        Customer = 1,

        /// <summary>
        /// Customer User
        /// </summary>
        CustomerUser = 2,

        /// <summary>
        /// Order Resource
        /// </summary>
        Order = 3,

        /// <summary>
        /// Subscription Resource
        /// </summary>
        Subscription = 4,

        /// <summary>
        /// License Resource
        /// </summary>
        License = 5,

        /// <summary>
        /// Third party add-on Resource
        /// </summary>
        ThirdPartyAddOn = 6,

        /// <summary>
        /// MPN association Resource
        /// </summary>
        MpnAssociation = 7,

        /// <summary>
        /// Transfer Resource
        /// </summary>
        Transfer = 8,

        /// <summary>
        /// Application Resource
        /// </summary>
        Application = 9,

        /// <summary>
        /// Application Credential Resource
        /// </summary>
        ApplicationCredential = 10,

        /// <summary>
        /// Partner User Resource
        /// </summary>
        PartnerUser = 11,

        /// <summary>
        /// Partner Relationship Resource
        /// </summary>
        PartnerRelationship = 12,

        /// <summary>
        /// Partner Referral Resource
        /// </summary>
        Referral = 13,

        /// <summary>
        /// Software Key Resource
        /// </summary>
        SoftwareKey = 14,

        /// <summary>
        /// Software Download Link Resource
        /// </summary>
        SoftwareDownloadLink = 15,

        /// <summary>
        /// Spending Limit Resource
        /// </summary>
        SpendingLimit = 16,

        /// <summary>
        /// Invoice Resource
        /// </summary>
        Invoice = 17,

        /// <summary>
        /// Agreement Resource
        /// </summary>
        Agreement = 18,

        /// <summary>
        /// Partner to customer relationship Resource
        /// </summary>
        PartnerCustomerRelationship = 19,

        /// <summary>
        /// Self serve policy Resource
        /// </summary>
        SelfServePolicy = 20,

        /// <summary>
        /// Granular Admin relationship Resource
        /// </summary>
        GranularAdminRelationship = 21,

        /// <summary>
        /// Granular Admin relationship access assignment Resource
        /// </summary>
        GranularAdminAccessAssignment = 22,

        /// <summary>
        /// Partner Customer DAP add or removal Resource
        /// </summary>
        PartnerCustomerDap = 23,

        /// <summary>
        /// Credit resource
        /// </summary>
        Credit = 24,

        /// <summary>
        /// Customer Directory Role
        /// </summary>
        CustomerDirectoryRole = 25,

        /// <summary>
        /// Software page
        /// </summary>
        SoftwarePage = 26,

        /// <summary>
        /// Device
        /// </summary>
        Device = 27,

        /// <summary>
        /// Policy
        /// </summary>
        Policy = 28,

        /// <summary>
        /// Overage
        /// </summary>
        Overage = 29,

        /// <summary>
        /// New-Commerce Migration
        /// </summary>
        NewCommerceMigration = 30,

        /// <summary>
        /// Azure Entitlement 
        /// </summary>
        AzureEntitlement = 31,

        /// <summary>
        /// Indirect Provider Indirect Reseller Dap Resource
        /// </summary>
        IndirectProviderIndirectResellerDap = 32,
    }
}
