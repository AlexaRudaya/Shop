﻿using Shop.Interfaces;
using Shop.Models;

namespace Shop.Services
{
    public sealed class CatalogItemViewModelService : ICatalogItemViewModelService
    {
        private readonly IRepository<CatalogItem> _catalogItemRepository;

        public CatalogItemViewModelService()
        {
            _catalogItemRepository = new LocalCatalogItemRepository();
        }
        public void UpdateCatalogItem(CatalogItemViewModel viewModel)
        {
            var existingCatalogItem = _catalogItemRepository.GetById(viewModel.Id);
            if (existingCatalogItem is null) throw new Exception($"Catalog item {viewModel.Id} was not found");


            CatalogItem.CatalogItemDetails detail = new(viewModel.Name, viewModel.Price);

            existingCatalogItem.UpdateDetails(detail);

            _catalogItemRepository.Update(existingCatalogItem);
        }
    }
}