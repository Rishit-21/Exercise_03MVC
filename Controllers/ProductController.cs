using Exercise_03.Models;
using Exercise_03.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise_03.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public ProductController(IProductRepository productRepository, IWebHostEnvironment webHostEnviroment)
        {
            _productRepository = productRepository;

            _webHostEnviroment = webHostEnviroment;
        }

        public async Task<IActionResult> Product(int isSort = 0)
        {
            var products = await _productRepository.GetAllProducts();
            if (isSort == 1)
            {
            return View(products.OrderBy(x=>x.productName));

            }
            return View(products);
        }

        [HttpGet]
        public async Task<ViewResult> AddProduct(int isSuccess = 2, int id = 0)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.id = id;
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductModel model, [FromQuery] long Productid)
        {

            if (ModelState.IsValid)
            {
                long id = await _productRepository.AddProduct(model);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddProduct), new { isSuccess = 0, Productid = id });
                }
                return RedirectToAction(nameof(AddProduct), new { isSuccess = 1, Productid = id });
            }

            return View();
        }


        [HttpGet("/EditProduct/{id}/{productName}")]
        public async Task<IActionResult> EditProduct([FromRoute] long id, [FromRoute] string name, ProductModel model, int isSuccess = 2)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.id = id;


            ProductModel productModel = new ProductModel()
            {
                productName = name
            };
            //ViewBag.name = name;

            return View("AddProduct", productModel);
        }


        [HttpPost("EditProduct/{id}/{name}")]
        public async Task<IActionResult> EditProduct(ProductModel model, [FromRoute] long id, [FromRoute] string name)
        {

            if (ModelState.IsValid)
            {
                long Id = await _productRepository.UpdateProduct(model, id);
                if (Id > 0)
                {
                    return RedirectToAction(nameof(EditProduct), new { isSuccess = 0, Partyid = id });
                }
                return RedirectToAction(nameof(EditProduct), new { isSuccess = 1, Partyid = id });
            }

            return View("EditProduct");
        }

        public async Task<IActionResult> DelProduct(long id, ProductModel model)
        {

            bool isDeleted = await _productRepository.DeleteProduct(model, id);


            if (isDeleted)
            {
                return RedirectToAction(nameof(Product));

            }
            return null;
        }

        [HttpGet("SortProductId")]
        public IActionResult SortProductId()
        {

            return RedirectToAction("Product", new { isSort = 0 });
        }
        [HttpGet("SortProduct")]
        public IActionResult SortProduct()
        {
            return RedirectToAction("Product", new { isSort = 1 });
        }

    }
}
