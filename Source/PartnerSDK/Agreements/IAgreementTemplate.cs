// -----------------------------------------------------------------------
// <copyright file="IAgreementTemplate.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Agreements
{
    /// <summary>
    /// This interface represents operations on an agreement template.
    /// </summary>
    public interface IAgreementTemplate : IPartnerComponent<AgreementTemplateContext>
    {
        /// <summary>
        /// Retrieves the agreement document.
        /// </summary>
        /// <returns>The agreement document.</returns>
        IAgreementDocument Document { get; }
    }
}
