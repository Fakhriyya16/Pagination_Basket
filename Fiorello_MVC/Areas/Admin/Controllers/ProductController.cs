using Fiorello_MVC.Helpers;
using Fiorello_MVC.Services.Interfaces;
using Fiorello_MVC.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var products = await _productService.GetAllPaginateAsync(page, 4);

            var mappedData = _productService.GetMappedDatas(products);

            int totalPage = await GetPageCountAsync(4);

            Paginate<ProductVM> paginatedData = new(mappedData, totalPage, page);

            return View(paginatedData);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _productService.GetCountAsync();

            return (int)Math.Ceiling((decimal)productCount / take);
        }
    }
}
