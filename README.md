# Partner Center .NET SDK

The Partner Center SDK (Software Development Kit) is a set of tools and resources provided by Microsoft for developers to integrate their applications with the Partner Center API. The Partner Center is a platform that allows Microsoft partners to manage their accounts, customers, and orders for various Microsoft products and services. The SDK provides libraries, sample code, and documentation that developers can use to build applications that can access and manipulate data in the Partner Center. This enables partners to automate their workflows, enhance their reporting capabilities, and manage their operations more efficiently.

This repository has been archived and Microsoft no longer maintains this project and no future releases are planned.

## Where is the latest version of the SDK?

The latest version of the .NET SDK for Partner Center can be found on the [here](https://www.nuget.org/packages/Microsoft.Store.PartnerCenter).

## Can I still use this code?

You can still use the code in this repository, but we cannot guarantee that it will work with the latest version of the Partner Center API or that it is secure. 

## What is included in this project

The features included in this Partner Center .NET SDK archival project are describred in the [.NET SDK release notes](https://learn.microsoft.com/en-us/partner-center/developer/dotnet-release-notes)

## Our recommendation?

Partner Center .NET SDK and archive it on GitHub. No further updates to the SDK will be made. Partners are encouraged to integrate through the [Partner Center REST APIs](https://learn.microsoft.com/en-us/partner-center/developer/partner-center-rest-api-reference) instead. The final version of the Partner Center .NET SDK will be available on Microsoft Archive on GitHub for local download and maintenance, but contributions will not be accepted.


# Setting up Partner Center SDK development environment

This guide will walk you through the steps to set up your development environment for using the Partner Center SDK

## Prerequisites

Before getting started, you will need:

 - Visual Studio 2019 or later installed on your computer
 - .NET Framework 4.6.1 or later installed on your computer
 - A Partner Center account

 ## Getting Started

1. Clone or download the Partner Center SDK repository from the GitHub repository.
    ```git
    git clone https://github.com/microsoft/partner-center-sdk-for-dotNET.git
    ```
2. Launch the Visual Studio IDE on your computer.
3. In the start window, select "***Open a project or solution***." You can also navigate to "**File** > **Open** > **Project/Solution**" from the menu bar. Select the "***PartnerCenterSdk.sln***" to open.
3. Once you have opened the solution, navigate to "**Build** > **Build Solution**" from the menu bar. This will compile your project and ensure that it is ready to run.
4. Next, set the startup project for your solution. You can do this by right-clicking on the "__PartnerSdk.FeatureSamples__" project in the Solution Explorer and selecting "***Set as Startup Project***."
    
    ### Note
    > The "__PartnerSdk.FeatureSamples__" feature sample project sample have snippets that demonstrate how to use the Microsoft Partner Center SDK with .NET-based applications. Feature sample provides examples of how to perform various tasks using the Partner Center API, such as creating and managing customers, retrieving customer subscriptions and usage data, and managing support tickets.Developers can use these code samples as a starting point for their own Partner Center API integration projects, or they can simply use them as a reference to learn more about how to work with the API. The "**Feature Sample** project is a valuable resource for developers who need to integrate with the Microsoft Partner Center API in their .NET-based applications.
---
5. Locate "__App.config__" file under "__PartnerSdk.FeatureSamples__" project and double click on it.
6. Add all the required values
    ```xml
    <?xml version="1.0" encoding="utf-8"?>
    <configuration>
    <startup>
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.6.1" />
    </startup>
    <appSettings>
        <add key="applicationSignIn" value="false"/>
        <add key="partnerServiceApiRoot" value="https://api.partnercenter.microsoft.com/"/>
        <add key="customerWithServiceCosts" value=""/>
        <add key="customerWithAgreements" value=""/>
        <add key="partnerUserIdForAgreement" value=""/>
        <add key="customerWithTrialOffer" value=""/>
        <add key="trialOfferId" value=""/>
        <add key="customerWithServiceRequests" value=""/>
        <add key="customerWithDevices" value=""/>
        <add key="customerWithProducts" value=""/>
        <add key="directResellerTestAccountCustomerTenantId" value=""/>
        <add key="directResellerTestAccountUserId" value=""/>
        <add key="directResellerIntegrationTestAccountUser" value=""/>
        <add key="offerWithAddonId" value=""/>
        <add key="offerAddonOneId" value=""/>
        <add key="offerAddonTwoId" value=""/>
        <add key="deviceBatchId" value="Prd1"/>
        <add key="defaultProductTargetView" value=""/> <!-- Example: azure -->
        <add key="defaultProductPromotionsSegment" value=""/> <!-- Example: commercial -->
        <add key="defaultProductCollection" value=""/> <!-- Example: azure -->
        <add key="deviceId" value=""/>
        <add key="configurationPolicyId" value=""/>
        <add key="trackingIdForDeviceDeployment" value=""/>
        <add key="customerForUsageDemo" value=""/>
        <add key="subscriptionForUsageDemo" value=""/>
        <add key="customerForRegistrationDemo" value=""/>
        <add key="subscriptionForRegistrationDemo" value=""/>
        <add key="customerServiceRequestId" value=""/> <!-- Example: 615121092223011 -->
        <add key="defaultMpnId" value=""/> <!-- Example: 1234567 -->
        <add key="defaultTenantId" value=""/>
        <add key="selectedInvoiceKey" value=""/> <!-- Example: D02005YFHI -->
        <add key="selectedReceiptKey" value=""/> <!-- Example: 8602768 -->
        <add key="customerForActivationLinksDemo" value=""/>
        <add key="subscriptionForActivationLinksDemo" value=""/>
        <add key="modernOrderIdDemo" value=""/> <!-- Example: 3WVl63zjolJvaVoNmJvMSIcaexSp5WvL1 -->
        <add key="orderIdForAttachments" value=""/> <!-- Example: 2297c718a6d7 -->
        <add key="orderAttachmentId" value=""/> <!-- Example: 0bde1dac54290b -->

        <!-- Modern Customers and Orders -->
        <add key="defaultCustomerId" value="" />
        <add key="customerIdForOrderSvc" value="" />
        <add key="productUpgradeCustomerId" value="" />
        <add key="productResourceName" value="" /> <!-- Example: Azure plan -->
        <add key="defaultBillingCycleType" value="" /> <!-- Example: OneTime -->
        <add key="azurePlanProductId" value="DZH318Z0BPS6" />
        <add key="azurePlanSkuId" value="0001" />
        <add key="defaultSubscriptionId" value="" />
        <add key="azuresubscriptionId" value="" />
        <add key="defaultProductFamily" value="azure" />
        <add key="aad.authority" value="https://login.windows.net" />
        <add key="aad.graphEndpoint" value="https://graph.windows.net" />
        <add key="aad.organizationsDomain" value="organizations" />

        <!-- DIRECT RESELLER TEST ACCOUNT FOR CUSTOMER USER LICENSE -->
        <add key="directResellerTestAccount.clientId" value="" />
        <add key="directResellerTestAccount.userName" value="" /> <!-- Example: testaccountusername@PRIMARYDOMAINNAME.onmicrosoft.com -->
        <add key="directResellerTestAccount.password" value="" />

        <!-- RI ACCOUNT -->
        <add key="aad.clientId" value="" />
        <add key="aad.userName" value="" /> <!-- Example: testaccountusername@PRIMARYDOMAINNAME.onmicrosoft.com -->
        <add key="aad.password" value="" />
        <add key="rIAccountPartnerTenantId" value="" />

        <add key="aad.resourceUrl" value="https://api.partnercenter.microsoft.com" />
        <add key="aad.redirectUri" value="http://localhost" />
        <add key="aad.applicationId" value="" />
        <add key="aad.applicationSecret" value="" />
        <add key="aad.applicationDomain" value="" />
        <add key="tipAccount.application.id" value="" />
        <add key="tipAccount.application.secret" value="" />
        <add key="tipAccount.application.domain" value="" />
        <add key="tipAccount.aad.clientId" value="" />
        <add key="tipAccount.aad.userName" value="" /> <!-- Example: testaccountusername@PRIMARYDOMAINNAME.onmicrosoft.com -->
        <add key="tipAccount.aad.password" value="" />
        <add key="TestMultiTierScenario" value="false" />
        <add key="TestDevicesScenario" value="false" />
        <add key="TestModernScenarios" value="false" />
        <add key="TestModernOfficeScenarios" value="true" />
        <add key="TestRIScenarios" value="false" />
        <add key="TestModernAzureScenarios" value="false" />
    </appSettings>
    <runtime>
        <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
        <dependentAssembly>
            <assemblyIdentity name="Microsoft.Identity.Client" publicKeyToken="31bf3856ad364e35" culture="neutral" />
            <bindingRedirect oldVersion="0.0.0.0-4.31.0.0" newVersion="4.31.0.0" />
        </dependentAssembly>
        </assemblyBinding>
    </runtime>
    </configuration>
    ```
7. Finally, you can run the project by selecting "**Debug** > **Start Debugging**" from the menu bar or by pressing the F5 key on your keyboard. This will launch the application and allow you to interact with it.

## Code of Conduct

This project has adopted the [Microsoft Open Source Code of Conduct](./CODE_OF_CONDUCT.md). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq) or contact opencode@microsoft.com with any additional questions or comments.

## Trademarks

This project may contain trademarks or logos for projects, products, or services. Authorized use of Microsoft 
trademarks or logos is subject to and must follow 
[Microsoft's Trademark & Brand Guidelines](https://www.microsoft.com/en-us/legal/intellectualproperty/trademarks/usage/general).
Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion or imply Microsoft sponsorship.
Any use of third-party trademarks or logos are subject to those third-party's policies.


## License

This code is licensed under the [MIT License](https://opensource.org/licenses/MIT). Feel free to use it as you wish.
