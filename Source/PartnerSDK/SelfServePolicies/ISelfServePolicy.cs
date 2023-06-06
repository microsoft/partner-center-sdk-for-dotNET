// -----------------------------------------------------------------------
// <copyright file="ISelfServePolicy.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.SelfServePolicies
{
    using Microsoft.Store.PartnerCenter.GenericOperations;
    using Microsoft.Store.PartnerCenter.Models.SelfServePolicies;

    /// <summary>
    /// Encapsulates a customer user behavior.
    /// </summary>
    public interface ISelfServePolicy : IEntityDeleteOperations<SelfServePolicy>, IEntityPutOperations<SelfServePolicy>
    {
    }
}
