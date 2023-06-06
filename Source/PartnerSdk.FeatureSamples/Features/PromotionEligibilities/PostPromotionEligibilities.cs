// -----------------------------------------------------------------------
// <copyright file="PostPromotionEligibilities.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.FeatureSamples.Features.PromotionEligibilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Store.PartnerCenter.Models;
    using Microsoft.Store.PartnerCenter.Models.Products;
    using Microsoft.Store.PartnerCenter.Models.PromotionEligibilities;
    using BillingCycleType = Models.PromotionEligibilities.Enums.BillingCycleType;

    /// <summary>
    /// Posting promotion eligibilities to see if customer is eligible for given promotions.
    /// </summary>
    internal class PostPromotionEligibilities : IUnitOfWork
    {
        /// <summary>
        /// Gets the unit of work title.
        /// </summary>
        public string Title
        {
            get { return "Post Promotion Eligibilities"; }
        }

        /// <summary>
        /// Testing the post promotion eligibilities operation.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations</param>
        /// <param name="state">The state hash table. This is used as the communication mechanism between units of work.</param>
        public void Execute(IAggregatePartner partnerOperations, IDictionary<string, object> state)
        {
            var selectedCustomerId = state[FeatureSamplesApplication.SelectedCustomerKey] as string;

            var productPromotions = this.GetProductPromotions(partnerOperations);

            // Take the first productPromotion
            var promotion = productPromotions.Items.FirstOrDefault();

            if (promotion == null && promotion.RequiredProducts != null)
            {
                Console.WriteLine("No existing promotions found.");
                return;
            }
            else
            {
                Console.Out.WriteLine("Promotion Id: {0}", promotion.Id);

                // Take the first required product and get the availability.
                var availability = this.GetAvailability(partnerOperations, promotion.RequiredProducts.First().ProductId, promotion.RequiredProducts.First().SkuId);

                if (availability != null)
                {
                    Console.Out.WriteLine("Catalog Item Id: {0}", availability.CatalogItemId);

                    // Convert the billing cycle on the term to the proper enum.
                    Enum.TryParse<BillingCycleType>(promotion.RequiredProducts.First().Term.BillingCycle, out var billingCycle);

                    // Build the promotion elibities request.
                    var promotionEligibilitiesRequest = new PromotionEligibilitiesRequest()
                    {
                        Items = new List<PromotionEligibilitiesRequestItem>()
                        {
                            new PromotionEligibilitiesRequestItem()
                            {
                                Id = 0,
                                CatalogItemId = availability.CatalogItemId,
                                TermDuration = promotion.RequiredProducts.First().Term.Duration,
                                BillingCycle = billingCycle,
                                Quantity = 25,
                                PromotionId = promotion.Id,
                            },
                        },
                    };

                    Console.Out.WriteLine("========================== Post Promotion Eligibilities ==========================");

                    var promotionEligibilities = partnerOperations.Customers.ById(selectedCustomerId).PromotionEligibilities.Post(promotionEligibilitiesRequest);

                    foreach (var eligibility in promotionEligibilities.Items)
                    {
                        Console.Out.WriteLine("Eligibility result for CatalogItemId: {0}", eligibility.CatalogItemId);
                        Console.Out.WriteLine("IsCustomerEligible: {0}", eligibility.Eligibilities.First().IsEligible.ToString());

                        if (!eligibility.Eligibilities.First().IsEligible)
                        {
                            Console.Out.WriteLine("Reasons for ineligibility:");
                            foreach (var error in eligibility.Eligibilities.First().Errors)
                            {
                                Console.Out.WriteLine("Type: {0}", error.Type);
                                Console.Out.WriteLine("Description: {0}", error.Description);
                            }
                        }
                    }

                    Console.Out.WriteLine("========================== Post Promotion Eligibilities ==========================");
                }
                else
                {
                    Console.WriteLine("Availability not found for promotion with id: {0}", promotion.Id);
                    return;
                }
            }
        }

        /// <summary>
        /// Retrieve an availability from the given productId.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <param name="productId">A productId.</param>
        /// <param name="skuId">A skuId.</param>
        /// <returns>Availability item.</returns>
        private Availability GetAvailability(IAggregatePartner partnerOperations, string productId, string skuId)
        {
            var availabilities = partnerOperations.Products.ByCountry("US").ById(productId).Skus.ById(skuId).Availabilities.ByTargetSegment("commercial").Get();
            return availabilities?.Items?.FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the product promotions for the partner.
        /// </summary>
        /// <param name="partnerOperations">A reference to the partner operations.</param>
        /// <returns>The collection of product promotions.</returns>
        private ResourceCollection<ProductPromotion> GetProductPromotions(IAggregatePartner partnerOperations)
        {
            return partnerOperations.ProductPromotions.ByCountry("US").BySegment("commercial").Get();
        }
    }
}