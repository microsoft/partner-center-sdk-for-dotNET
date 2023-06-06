//----------------------------------------------------------------
// <copyright file="OperationType.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//----------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Models.Auditing
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Store.PartnerCenter.Models.JsonConverters;
    using Newtonsoft.Json;

    /// <summary>
    /// Enumeration to represent type of
    /// Operation being performed
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Exception.")]
    [JsonConverter(typeof(EnumJsonConverter))]
    public enum OperationType
    {
        /// <summary>
        /// The undefined
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Update Customer Qualification
        /// </summary>
        UpdateCustomerQualification = 1,

        /// <summary>
        /// Updates an existing subscription.
        /// </summary>
        UpdateSubscription = 2,

        /// <summary>
        /// Transition a subscription.
        /// </summary>
        UpgradeSubscription = 3,

        /// <summary>
        /// Convert a trial subscription to a paid one.
        /// </summary>
        ConvertTrialSubscription = 4,

        /// <summary>
        /// Adding a Customer
        /// </summary>
        AddCustomer = 5,

        /// <summary>
        /// Update a Customer Billing Profile
        /// </summary>
        UpdateCustomerBillingProfile = 6,

        /// <summary>
        /// Update a Customer's Partner Contract Company Name
        /// </summary>
        UpdateCustomerPartnerContractCompanyName = 7,

        /// <summary>
        /// Updates a customer spending budget.
        /// </summary>
        UpdateCustomerSpendingBudget = 8,

        /// <summary>
        /// Deleting a Customer.
        /// Happens only in the sandbox integration accounts.
        /// </summary>
        DeleteCustomer = 9,

        /// <summary>
        /// Remove Partner Customer relationship.
        /// </summary>
        RemovePartnerCustomerRelationship = 10,

        /// <summary>
        /// Create a new order.
        /// </summary>
        CreateOrder = 11,

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        UpdateOrder = 12,

        /// <summary>
        /// Creates a customer user.
        /// </summary>
        CreateCustomerUser = 13,

        /// <summary>
        /// Deletes a customer user.
        /// </summary>
        DeleteCustomerUser = 14,

        /// <summary>
        /// Updates a customer user.
        /// </summary>
        UpdateCustomerUser = 15,

        /// <summary>
        /// Updates a customer user licenses.
        /// </summary>
        UpdateCustomerUserLicenses = 16,

        /// <summary>
        /// Reset customer user password.
        /// </summary>
        ResetCustomerUserPassword = 17,

        /// <summary>
        /// Update customer user UPN.
        /// </summary>
        UpdateCustomerUserPrincipalName = 18,

        /// <summary>
        /// Restore customer user.
        /// </summary>
        RestoreCustomerUser = 19,

        /// <summary>
        /// Create MPN association.
        /// </summary>
        CreateMpnAssociation = 20,

        /// <summary>
        /// Update MPN association.
        /// </summary>
        UpdateMpnAssociation = 21,

        /// <summary>
        /// Updates a Sfb customer user licenses.
        /// </summary>
        UpdateSfbCustomerUserLicenses = 22,

        /// <summary>
        /// Update transfer.
        /// </summary>
        UpdateTransfer = 23,

        /// <summary>
        /// Creates a partner relationship.
        /// </summary>
        CreatePartnerRelationship = 24,

        /// <summary>
        /// Add and registers an application.
        /// </summary>
        RegisterApplication = 25,

        /// <summary>
        /// Unregisters an application.
        /// </summary>
        UnregisterApplication = 26,

        /// <summary>
        /// Adds an application credential.
        /// </summary>
        AddApplicationCredential = 27,

        /// <summary>
        /// Adds an application credential.
        /// </summary>
        RemoveApplicationCredential = 28,

        /// <summary>
        /// Creates a partner user.
        /// </summary>
        CreatePartnerUser = 29,

        /// <summary>
        /// Updates a partner user.
        /// </summary>
        UpdatePartnerUser = 30,

        /// <summary>
        /// Removes a partner user.
        /// </summary>
        RemovePartnerUser = 31,

        /// <summary>
        /// Create a referral
        /// </summary>
        CreateReferral = 32,

        /// <summary>
        /// Update a referral
        /// </summary>
        UpdateReferral = 33,

        /// <summary>
        /// Get software Key
        /// </summary>
        GetSoftwareKey = 34,

        /// <summary>
        /// Get software download link
        /// </summary>
        GetSoftwareDownloadLink = 35,

        /// <summary>
        /// Increase spending limit
        /// </summary>
        IncreaseSpendingLimit = 36,

        /// <summary>
        /// Invoice is ready
        /// </summary>
        ReadyInvoice = 37,

        /// <summary>
        /// Agreement is created
        /// </summary>
        CreateAgreement = 38,

        /// <summary>
        /// Extend a relationship from an indirect partner to an indirect provider.
        /// </summary>
        ExtendRelationship = 39,

        /// <summary>
        /// Create transfer.
        /// </summary>
        CreateTransfer = 40,

        /// <summary>
        /// Create the self serve policy
        /// </summary>
        CreateSelfServePolicy = 41,

        /// <summary>
        /// Update the self serve policy
        /// </summary>
        UpdateSelfServePolicy = 42,

        /// <summary>
        /// Delete the self serve policy
        /// </summary>
        DeleteSelfServePolicy = 43,

        /// <summary>
        /// Remove partner relationship (ex: remove IP - IR relationship).
        /// </summary>
        RemovePartnerRelationship = 44,

        /// <summary>
        /// Deletes a customer from a sandbox partner account.
        /// </summary>
        DeleteTipCustomer = 45,

        /// <summary>
        /// Creates related referral
        /// </summary>
        CreateRelatedReferral = 46,

        /// <summary>
        /// Updates related referral
        /// </summary>
        UpdateRelatedReferral = 47,

        /// <summary>
        /// Approved a Granular Admin relationship
        /// </summary>
        GranularAdminRelationshipApproved = 48,

        /// <summary>
        /// Activated a Granular Admin relationship
        /// </summary>
        GranularAdminRelationshipActivated = 49,

        /// <summary>
        /// Requested termination of a Granular Admin relationship.
        /// </summary>
        GranularAdminRelationshipTerminationRequested = 50,

        /// <summary>
        /// Terminated a Granular Admin relationship.
        /// </summary>
        GranularAdminRelationshipTerminated = 51,

        /// <summary>
        /// Granular Admin relationship expired.
        /// </summary>
        GranularAdminRelationshipExpired = 52,

        /// <summary>
        /// Created a Granular Admin relationship access assignment.
        /// </summary>
        GranularAdminAccessAssignmentCreated = 53,

        /// <summary>
        /// Updated a Granular Admin relationship access assignment.
        /// </summary>
        GranularAdminAccessAssignmentUpdated = 54,

        /// <summary>
        /// Deleted a Granular Admin relationship access assignment.
        /// </summary>
        GranularAdminAccessAssignmentDeleted = 55,

        /// <summary>
        /// Dap Admin relationship approved
        /// </summary>
        DapAdminRelationshipApproved = 56,

        /// <summary>
        /// Dap Admin relationship Terminated
        /// </summary>
        DapAdminRelationshipTerminated = 57,

        /// <summary>
        /// Create credit.
        /// </summary>
        CreateCredit = 58,

        /// <summary>
        /// Update credit.
        /// </summary>
        UpdateCredit = 59,

        /// <summary>
        /// Cancel credit.
        /// </summary>
        CancelCredit = 60,

        /// <summary>
        /// Add a user member.
        /// </summary>
        AddUserMember = 61,

        /// <summary>
        /// Remove a user member.
        /// </summary>
        RemoveUserMember = 62,

        /// <summary>
        /// Activated a Granular Admin relationship access assignment.
        /// </summary>
        GranularAdminAccessAssignmentActivated = 63,

        /// <summary>
        /// Partner clicks on the Software Attestation checkbox
        /// </summary>
        SoftwareAttestation = 64,

        /// <summary>
        /// Cleaned up a Granular Admin relationship and associated objects.
        /// </summary>
        GranularAdminRelationshipCleanedUp = 65,

        /// <summary>
        /// Partner updates customer's company profile information.
        /// </summary>
        UpdateCompanyInfo = 66,

        /// <summary>
        /// Partner creates customer's policy profile information.
        /// </summary>
        PolicyCreated = 67,

        /// <summary>
        /// Partner updates customer's policy profile information.
        /// </summary>
        PolicyUpdated = 68,

        /// <summary>
        /// Partner deletes customer's policy profile information.
        /// </summary>
        PolicyDeleted = 69,

        /// <summary>
        /// Partner updates customer's device information.
        /// </summary>
        DeviceUpdated = 70,

        /// <summary>
        /// Partner updates customer's devices information.
        /// </summary>
        DevicesUpdateAttempted = 71,

        /// <summary>
        /// Partner deletes customer's devices information.
        /// </summary>
        DeviceDeleted = 72,

        /// <summary>
        /// Partner uploads customer's device to new batch.
        /// </summary>
        DevicesUploadToNewBatchAttempted = 73,

        /// <summary>
        /// Partner uploads customer's device to existing batch.
        /// </summary>
        DevicesUploadToExistingBatchAttempted = 74,

        /// <summary>
        /// Partner manages subscription overage for PayG.
        /// </summary>
        ManageOverage = 75,

        /// <summary>
        /// Dap Admin relationship Terminated By Microsoft.
        /// </summary>
        DapAdminRelationshipTerminatedByMicrosoft = 76,

        /// <summary>
        /// Azure Fraud Event Detected by Fraud Events Service.
        /// </summary>
        AzureFraudEventDetected = 77,

        /// <summary>
        /// Creating a new-commerce migration.
        /// </summary>
        CreateNewCommerceMigration = 78,
    }
}
