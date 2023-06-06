// -----------------------------------------------------------------------
// <copyright file="IAgreementDocument.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Agreements
{
    using System.Threading.Tasks;
    using Models.Agreements;

    /// <summary>
    /// This interface represents operations on an agreement document.
    /// </summary>
    public interface IAgreementDocument : IPartnerComponent<AgreementDocumentContext>
    {
        /// <summary>
        /// Retrieves the agreement document.
        /// </summary>
        /// <returns>The agreement document.</returns>
        AgreementDocument Get();

        /// <summary>
        /// Asynchronously retrieves the agreement document.
        /// </summary>
        /// <returns>The agreement document.</returns>
        Task<AgreementDocument> GetAsync();

        /// <summary>
        /// Customizes operations based on the given language and locale.
        /// </summary>
        /// <param name="language">The language and locale to be used by the returned operations.</param>
        /// <returns>An operations interface customized for the provided language and locale.</returns>
        IAgreementDocument ByLanguage(string language);

        /// <summary>
        /// Customizes operations based on the given country.
        /// </summary>
        /// <param name="country">The ISO alpha-2 code of the country to be used by the returned operations.</param>
        /// <returns>An operations interface customized for the provided country.</returns>
        IAgreementDocument ByCountry(string country);
    }
}
