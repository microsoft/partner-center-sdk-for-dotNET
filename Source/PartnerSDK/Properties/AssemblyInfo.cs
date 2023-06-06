// -----------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

// The Sdk will expose its internals to the extensions as well as the test projects to allow them to reuse its infrastructure: retries,
// error handling, logging and so forth...
using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

#if DEBUG
[assembly: InternalsVisibleTo("Microsoft.Store.PartnerCenter.Extensions")]

[assembly: InternalsVisibleTo("Microsoft.Store.PartnerCenter.Internal")]

[assembly: InternalsVisibleTo("Microsoft.Store.PartnerCenter.UnitTests")]

[assembly: InternalsVisibleTo("Microsoft.Store.PartnerCenter.Fakes")]

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

[assembly: InternalsVisibleTo("Microsoft.Store.PartnerCenter.Internal.Test")]

[assembly: InternalsVisibleTo("Microsoft.Store.PartnerCenter.0.0.0.1.Fakes")]
#endif

#if !DEBUG
[assembly: InternalsVisibleTo("Microsoft.Store.PartnerCenter.Extensions, publickey=" +
    "0024000004800000940000000602000000240000525341310004000001000100b5fc90e7027f67" +
    "871e773a8fde8938c81dd402ba65b9201d60593e96c492651e889cc13f1415ebb53fac1131ae0b" +
    "d333c5ee6021672d9718ea31a8aebd0da0072f25d87dba6fc90ffd598ed4da35e44c398c454307" +
    "e8e33b8426143daec9f596836f97c8f74750e5975c64e2189f45def46b2a2b1247adc3652bf5c3" +
    "08055da9")]

[assembly: InternalsVisibleTo("Microsoft.Store.PartnerCenter.Internal, publickey=" +
    "0024000004800000940000000602000000240000525341310004000001000100b5fc90e7027f67" +
    "871e773a8fde8938c81dd402ba65b9201d60593e96c492651e889cc13f1415ebb53fac1131ae0b" +
    "d333c5ee6021672d9718ea31a8aebd0da0072f25d87dba6fc90ffd598ed4da35e44c398c454307" +
    "e8e33b8426143daec9f596836f97c8f74750e5975c64e2189f45def46b2a2b1247adc3652bf5c3" +
    "08055da9")]

[assembly: InternalsVisibleTo("Microsoft.Store.PartnerCenter.UnitTests, publickey=" +
    "0024000004800000940000000602000000240000525341310004000001000100b5fc90e7027f67" +
    "871e773a8fde8938c81dd402ba65b9201d60593e96c492651e889cc13f1415ebb53fac1131ae0b" +
    "d333c5ee6021672d9718ea31a8aebd0da0072f25d87dba6fc90ffd598ed4da35e44c398c454307" +
    "e8e33b8426143daec9f596836f97c8f74750e5975c64e2189f45def46b2a2b1247adc3652bf5c3" +
    "08055da9")]

[assembly: InternalsVisibleTo("Microsoft.Store.PartnerCenter.Fakes, publickey=" +
    "0024000004800000940000000602000000240000525341310004000001000100e92decb949446f" +
    "688ab9f6973436c535bf50acd1fd580495aae3f875aa4e4f663ca77908c63b7f0996977cb98fcf" +
    "db35e05aa2c842002703cad835473caac5ef14107e3a7fae01120a96558785f48319f66daabc86" +
    "2872b2c53f5ac11fa335c0165e202b4c011334c7bc8f4c4e570cf255190f4e3e2cbc9137ca57cb687947bc")]

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2, publickey=" +
    "0024000004800000940000000602000000240000525341310004000001000100c547cac37abd99" +
    "c8db225ef2f6c8a3602f3b3606cc9891605d02baa56104f4cfc0734aa39b93bf7852f7d9266654" +
    "753cc297e7d2edfe0bac1cdcf9f717241550e0a7b191195b7667bb4f64bcb8e2121380fd1d9d46" +
    "ad2d92d2d15605093924cceaf74c4861eff62abf69b9291ed0a340e113be11e6a7d3113e92484cf7045cc7")]

[assembly: InternalsVisibleTo("Microsoft.Store.PartnerCenter.Internal.Test, publickey=" +
    "0024000004800000940000000602000000240000525341310004000001000100b5fc90e7027f67" +
    "871e773a8fde8938c81dd402ba65b9201d60593e96c492651e889cc13f1415ebb53fac1131ae0b" +
    "d333c5ee6021672d9718ea31a8aebd0da0072f25d87dba6fc90ffd598ed4da35e44c398c454307" +
    "e8e33b8426143daec9f596836f97c8f74750e5975c64e2189f45def46b2a2b1247adc3652bf5c3" +
    "08055da9")]

[assembly: InternalsVisibleTo("Microsoft.Store.PartnerCenter.0.0.0.1.Fakes, publickey=" +
    "0024000004800000940000000602000000240000525341310004000001000100b5fc90e7027f67" +
    "871e773a8fde8938c81dd402ba65b9201d60593e96c492651e889cc13f1415ebb53fac1131ae0b" +
    "d333c5ee6021672d9718ea31a8aebd0da0072f25d87dba6fc90ffd598ed4da35e44c398c454307" +
    "e8e33b8426143daec9f596836f97c8f74750e5975c64e2189f45def46b2a2b1247adc3652bf5c3" +
    "08055da9")]
#endif

[assembly: CLSCompliant(true)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: AssemblyTitle("Microsoft Partner Center SDK")]
[assembly: AssemblyDescription("SDK for accessing Microsoft Partner Center APIs.")]
[assembly: AssemblyCompany("Microsoft")]
[assembly: AssemblyProduct("PartnerSdk")]
[assembly: AssemblyCopyright("Copyright © 2023")]
[assembly: AssemblyVersion("3.4.0")]
[assembly: AssemblyFileVersion("3.4.0")]
