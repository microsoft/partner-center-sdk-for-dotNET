// -----------------------------------------------------------------------
// <copyright file="ParameterValidator.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Utilities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Helper class for validating parameters on method calls.
    /// </summary>
    internal static class ParameterValidator
    {
        /// <summary>
        /// Validates that a parameter is not null or empty.
        /// </summary>
        /// <param name="parameterValue">The value of the method parameter.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <exception cref="ArgumentNullException">Thrown if the parameter is not set.</exception>
        public static void Required(object parameterValue, string errorMessage)
        {
            ValidationAttribute required = new RequiredAttribute();

            if (!required.IsValid(parameterValue))
            {
                throw new ArgumentNullException(errorMessage);
            }
        }

        /// <summary>
        /// Validates that a parameter is in the given range.
        /// </summary>
        /// <typeparam name="T">The parameter type.</typeparam>
        /// <param name="min">The minimum valid value.</param>
        /// <param name="max">The maximum valid value.</param>
        /// <param name="parameterValue">The given parameter value.</param>
        /// <param name="errorMessage">The error message to set in the thrown exception if the parameter was not in range.</param>
        /// <exception cref="ArgumentException">Thrown if the parameter is not in the given range.</exception>
        public static void IsInclusive<T>(T min, T max, T parameterValue, string errorMessage) where T : IComparable<T>
        {
            Type type = typeof(T);
            ValidationAttribute inRange = new RangeAttribute(type, min.ToString(), max.ToString());

            if (!inRange.IsValid(parameterValue))
            {
                throw new ArgumentException(errorMessage);
            }
        }

        /// <summary>
        /// Ensures that a given country code is valid.
        /// </summary>
        /// <param name="countryCode">The country code to validate.</param>
        public static void ValidateCountryCode(string countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
            {
                throw new ArgumentException("Country can't be empty");
            }
            else if (countryCode.Length != 2)
            {
                throw new ArgumentException("Country has to be a 2 letter string");
            }
        }
    }
}
