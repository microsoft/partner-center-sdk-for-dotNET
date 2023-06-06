// -----------------------------------------------------------------------
// <copyright file="ICustomerLicensesDeploymentInsightsCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Analytics
{
    using GenericOperations;
    using Models;
    using Models.Analytics;

    /// <summary>
    /// Encapsulates the operations on the customer's licenses' deployment insights collection.
    /// </summary>
    public interface ICustomerLicensesDeploymentInsightsCollection : IPartnerComponent, IEntireEntityCollectionRetrievalOperations<CustomerLicensesDeploymentInsights, ResourceCollection<CustomerLicensesDeploymentInsights>>
    {
    }
}
