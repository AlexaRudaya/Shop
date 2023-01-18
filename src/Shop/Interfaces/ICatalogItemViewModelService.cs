using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Models;

namespace Shop.Interfaces
{
    public interface ICatalogItemViewModelService
    {
        void UpdateCatalogItem(CatalogItemViewModel viewModel);

        Task<CatalogIndexViewModel> GetCatalogItems(int? brandId, int? typeId);

        Task<IEnumerable<SelectListItem>> GetBrands();
        Task<IEnumerable<SelectListItem>> GetTypes();
    }
}
