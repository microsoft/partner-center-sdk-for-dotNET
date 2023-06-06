// -----------------------------------------------------------------------
// <copyright file="IAgreementTemplateCollection.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Agreements
{
    using GenericOperations;

    /// <summary>
    /// This interface represents the operations that can be done on agreement templates.
    /// </summary>
    public interface IAgreementTemplateCollection : IPartnerComponent, IEntitySelector<IAgreementTemplate>
    {
    }
}
