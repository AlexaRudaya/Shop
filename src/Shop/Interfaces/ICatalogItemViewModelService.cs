using Shop.Models;

namespace Shop.Interfaces
{
    public interface ICatalogItemViewModelService
    {
        void UpdateCatalogItem(CatalogItemViewModel viewModel);

        Task<IEnumerable<CatalogItemViewModel>> GetCatalogItems();
    }
}
