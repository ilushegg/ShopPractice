using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.ViewModel;
using Shop.Service.Implementations;
using Shop.Service.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace Shop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> Products()
        {
            var response = await _productService.GetAllProducts();
            if(response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add() => View();


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.AddProduct(model);
                if(response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("Products");
                }
                return RedirectToAction("Error");
            }
            return RedirectToAction("Error");
        }
    }
}
