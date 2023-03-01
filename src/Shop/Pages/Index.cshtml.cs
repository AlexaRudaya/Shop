using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Interfaces;
using Shop.Models;

namespace Shop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogItemViewModelService _catalogViewModelService;

        public IndexModel(ICatalogItemViewModelService catalogViewModelService)
        {
            _catalogViewModelService = catalogViewModelService;
        }

        public CatalogIndexViewModel CatalogModel { get; set; } = new();

        public async Task OnGet(CatalogIndexViewModel catalogModel, int? pageId)
        {
            CatalogModel = await _catalogViewModelService.GetCatalogItems(pageId ?? 0, Constants.ITEMS_PER_PAGE, catalogModel.BrandFilterApplied, catalogModel.TypesFilterApplied);
        }
    }
}
