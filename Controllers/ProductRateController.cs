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
    public class ProductRateController : Controller
    {

        private readonly IProductRateRepository _productRateRepository;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public ProductRateController(IProductRateRepository productRateRepository, IWebHostEnvironment webHostEnviroment)
        {
            _productRateRepository = productRateRepository;

            _webHostEnviroment = webHostEnviroment;
        }

        public async Task<IActionResult> ProductRate()
        {
            var Data = await _productRateRepository.GetAllProductRate();

            return View(Data);
        }


        [HttpGet]
        public async Task<ViewResult> AddProductRate(int isSuccess = 2, long id = 0)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductRate(ProductRateModel model, [FromQuery] long Partyid)
        {

            if (ModelState.IsValid)
            {
                long id = await _productRateRepository.AddProductRate(model);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddProductRate), new { isSuccess = 0, Partyid = id });
                }
                return RedirectToAction(nameof(AddProductRate), new { isSuccess = 1, Partyid = id });

            }

            return View();
        }


        [HttpGet("UpdateProductRate/{id}/{RateProductName}/{ProductRate}")]
        public IActionResult UpdateProductRate([FromRoute] long id, [FromRoute] long RateProductName, [FromRoute] double ProductRate, int isSuccess = 2)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.id = id;
            //ViewBag.AssignPartyName = AssignPartyName;
            //ViewBag.AssignProductName = AssignProductName;

            ProductRateModel productRateModel = new ProductRateModel()
            {
                ProductId = RateProductName,
                ProductRates = ProductRate,
                DateOfRate = DateTime.Now,
            };


            return View("AddProductRate", productRateModel);
        }


        [HttpPost("UpdateProductRate/{id}/{RateProductName}/{ProductRate}")]
        public async Task<IActionResult> UpdateProductRate([FromRoute] long id, ProductRateModel model)
        {

            if (ModelState.IsValid)
            {
                long Id = await _productRateRepository.UpdateProductRate(model, id);
                if (Id > 0)
                {
                    return RedirectToAction(nameof(ProductRate), new { isSuccess = 0, Partyid = id });
                }
                return RedirectToAction(nameof(ProductRate), new { isSuccess = 1, Partyid = id });
            }

            return View("ProductRate");
        }

        public async Task<IActionResult> DeLProductRate(long id, ProductRateModel model)
        {

            bool isDeleted = await _productRateRepository.DeleteProductRate(model, id);


            if (isDeleted)
            {
                return RedirectToAction(nameof(ProductRate));

            }
            return null;
        }


    }
}
