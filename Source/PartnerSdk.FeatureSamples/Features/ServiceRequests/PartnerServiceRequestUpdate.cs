// <copyright file="PartnerServiceRequestUpdate.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.ServiceRequests
{
    using System;
    using System.Collections.Generic;
    using Models.ServiceRequests;

    /// <summary>
    /// Showcases customer creation.
    /// </summary>
    internal class PartnerServiceRequestUpdate : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get
            {
                return "Update Partner Service Request";
            }
        }

        /// <summary>
        /// Executes the unit of work.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            // serviceRequestId 615100291121013
            ServiceRequestNote note = new ServiceRequestNote { Text = "Sample Note" };

            object existingServiceRequestObject;
            state.TryGetValue("partnerServiceRequest", out existingServiceRequestObject);

            ServiceRequest existingServiceRequest = existingServiceRequestObject as ServiceRequest;

            if (existingServiceRequest != null)
            {
                ServiceRequest updatedServiceRequest = partnerOperations.ServiceRequests.ById(existingServiceRequest.Id).Patch(new ServiceRequest
                {
                    NewNote = note,
                    Status = ServiceRequestStatus.Closed
                });

                Console.WriteLine("Updated Service Request");
                Console.WriteLine("Id: {0}", updatedServiceRequest.Id);
                Console.WriteLine("Title: {0}", updatedServiceRequest.Title);
                Console.WriteLine("Description: {0}", updatedServiceRequest.Description);
                Console.WriteLine("CreatedDate: {0}", updatedServiceRequest.CreatedDate);
                Console.WriteLine("Severity: {0}", updatedServiceRequest.Severity);
                Console.WriteLine("Status: {0}", updatedServiceRequest.Status);
                Console.WriteLine("SupportTopicId: {0}", updatedServiceRequest.SupportTopicId);
                Console.WriteLine("SupportTopicName: {0}", updatedServiceRequest.SupportTopicName);

                if (updatedServiceRequest.PrimaryContact != null)
                {
                    Console.WriteLine("PrimaryContact.FirstName: {0}", updatedServiceRequest.PrimaryContact.FirstName);
                    Console.WriteLine("PrimaryContact.LastName: {0}", updatedServiceRequest.PrimaryContact.LastName);
                    Console.WriteLine("PrimaryContact.Id: {0}", updatedServiceRequest.PrimaryContact.ContactId);
                    Console.WriteLine("PrimaryContact.PhoneNumber: {0}", updatedServiceRequest.PrimaryContact.PhoneNumber);
                    Console.WriteLine("PrimaryContact.Email: {0}", updatedServiceRequest.PrimaryContact.Email);

                    if (updatedServiceRequest.PrimaryContact.Organization != null)
                    {
                        Console.WriteLine("PrimaryContact.Organization.Id: {0}", updatedServiceRequest.PrimaryContact.Organization.Id);
                        Console.WriteLine("updatedServiceRequest.PrimaryContact.Organization.Name:{0} ", updatedServiceRequest.PrimaryContact.Organization.Name);
                        Console.WriteLine("PrimaryContact.Organization.PhoneNumber: {0}", updatedServiceRequest.PrimaryContact.Organization.PhoneNumber);
                    }
                    else
                    {
                        Console.WriteLine("!! Missing Primary Contact Organization");
                    }
                }
                else
                {
                    Console.WriteLine("!! Missing Primary Contact");
                }

                if (updatedServiceRequest.Organization != null)
                {
                    Console.WriteLine("Organization.Id: {0}", updatedServiceRequest.Organization.Id);
                    Console.WriteLine("Organization.Name: {0}", updatedServiceRequest.Organization.Name);
                    Console.WriteLine("Organization.PhoneNumber: {0}", updatedServiceRequest.Organization.PhoneNumber);
                }
                else
                {
                    Console.WriteLine("!! Missing Incident Organization Details");
                }

                if (updatedServiceRequest.Notes != null)
                {
                    foreach (ServiceRequestNote serviceRequestNote in updatedServiceRequest.Notes)
                    {
                        Console.WriteLine("serviceRequestNote.Text: {0}", serviceRequestNote.Text);
                        Console.WriteLine("serviceRequestNote.CreatedByName: {0}", serviceRequestNote.CreatedByName);
                        Console.WriteLine("serviceRequestNote.CreatedDate: {0}", serviceRequestNote.CreatedDate);
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("!! Note was not updated to Service Request");
                }
            }
            else
            {
                Console.WriteLine("No existing Service Request");
            }
        }
    }
}
