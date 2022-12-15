﻿using Shop.ApplicationCore.Interfaces;
using Shop.Interfaces;
using Shop.Models;

namespace Shop.Services
{
    public sealed class CatalogItemViewModelService : ICatalogItemViewModelService
    {
        private readonly IRepository<CatalogItem> _catalogItemRepository;

        private readonly IAppLogger<CatalogItemViewModelService> _logger;

        public CatalogItemViewModelService(IRepository<CatalogItem> catalogItemRepository,
            IAppLogger<CatalogItemViewModelService> logger)
        {
            _catalogItemRepository = catalogItemRepository;
            _logger = logger;
        }
        public void UpdateCatalogItem(CatalogItemViewModel viewModel)
        {
            var existingCatalogItem = _catalogItemRepository.GetById(viewModel.Id);
            if (existingCatalogItem is null)
            {
                var exception = new Exception($"Catalog item {viewModel.Id} was not found");
                _logger.LogError(exception, exception.Message);

                throw exception;
            }

            CatalogItem.CatalogItemDetails detail = new(viewModel.Name, viewModel.Price);
            existingCatalogItem.UpdateDetails(detail);

            _logger.LogInformation($"Updating catalog item {existingCatalogItem.Id}. " +
                $"Name {existingCatalogItem.Name}." +
                $" Price {existingCatalogItem.Price}");
            _catalogItemRepository.Update(existingCatalogItem);
        }
    }
}