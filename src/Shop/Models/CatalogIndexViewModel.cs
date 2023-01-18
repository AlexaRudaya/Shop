using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shop.Models
{
    public sealed class CatalogIndexViewModel
    {
        public List<CatalogItemViewModel>? CatalogItems{ get; set; }
        public List<SelectListItem>? Brands { get; set; }
        public List<SelectListItem>? Types { get; set; }
        public int? BrandFilterApplied { get; set; } 
        public int? TypesFilterApplied { get; set; }
    }
}
