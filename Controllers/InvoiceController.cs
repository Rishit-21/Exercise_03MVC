using Exercise_03.Models;
using Exercise_03.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise_03.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IPartyAssignRepository _partyAssignRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public InvoiceController(IPartyAssignRepository partyAssignRepository, IInvoiceRepository invoiceRepository, IWebHostEnvironment webHostEnviroment)
        {


            _webHostEnviroment = webHostEnviroment;
            _invoiceRepository = invoiceRepository;
            _partyAssignRepository = partyAssignRepository;

        }

        [HttpGet]
        public async Task<IActionResult> Invoice(long id, long grandTotal = 0, bool isSuccess = false)
        {
            ViewBag.isSuccess = isSuccess;
            if (isSuccess == true)
            {
                ViewBag.InvoiceTable = await _invoiceRepository.ShowInvoice(id);
                ViewBag.Display = true;
                ViewBag.grandTotal = grandTotal;
                return View();
            }
            else
            {
                ViewBag.Display = false;
                ViewBag.grandTotal = grandTotal;
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Invoice(long PartyId, InvoiceModel model)
        {

            long GrandTotal = 0;
            if (ModelState.IsValid)
            {
                GrandTotal = await _invoiceRepository.AddInvoice(model, PartyId);

            }
            return RedirectToAction(nameof(Invoice), new { isSuccess = true, grandTotal = GrandTotal, id = PartyId });





        }

        [HttpGet]
        public async Task<IActionResult> Close()
        {
            return RedirectToAction(nameof(Invoice), new { isSuccess = false, grandTotal = 0 });
        }


        [HttpGet]
        public async Task<JsonResult> BindProductDetails(long PartyId)
        {
            //List<prodList> products = new List<prodList>();

            var products = await _invoiceRepository.BindProductAsync(PartyId);
            return Json(products);
        }
        [HttpGet]
        public async Task<JsonResult> BindProductRateDetails(long ProductId)
        {
            //List<prodList> products = new List<prodList>();
            var rates = await _invoiceRepository.BindRate(ProductId);
            return Json(rates);
        }
    }
}
