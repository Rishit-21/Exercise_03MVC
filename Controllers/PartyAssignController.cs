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
    public class PartyAssignController : Controller
    {
        private readonly IPartyAssignRepository _partyAssignRepository;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public PartyAssignController(IPartyAssignRepository partyAssignRepository, IWebHostEnvironment webHostEnviroment)
        {


            _webHostEnviroment = webHostEnviroment;
            _partyAssignRepository = partyAssignRepository;

        }

        public async Task<IActionResult> PartyAssign(int isSort=0)
        {
            var AssignParties = await _partyAssignRepository.GetAllAssignParty();
            if (isSort == 1)
            {
                return View(AssignParties.OrderBy(x=>x.Product.productName));
            }
            else if (isSort == 2)
            {
                return View(AssignParties.OrderBy(x => x.Party.partyName));
            }
            return View(AssignParties);
        }

        public async Task<ViewResult> AddAssignParty(int isSuccess = 2, long id = 0)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.id = id;
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> AddAssignParty(AssignPartyModel model, [FromQuery] long Partyid)
        {

            if (ModelState.IsValid)
            {
                long id = await _partyAssignRepository.AddAssignParty(model);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddAssignParty), new { isSuccess = 0, Partyid = id });
                }
                return RedirectToAction(nameof(AddAssignParty), new { isSuccess = 1, Partyid = id });
                // ViewBag.isSucces = true;
                // ViewBag.id = 0;
            }

            return View();
        }


        [HttpGet("UpdateAssignParty/{id}/{AssignPartyName}/{ProductId}")]
        public IActionResult UpdateAssignParty(int AssignPartyName, int ProductId, int isSuccess = 2, [FromRoute] int id = 0)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.id = id;
            //ViewBag.AssignPartyName = AssignPartyName;
            //ViewBag.AssignProductName = AssignProductName;

            AssignPartyModel assignPartyModel = new AssignPartyModel()
            {
                PartyId = AssignPartyName,
                ProductId = ProductId
            };

            return View("AddAssignParty", assignPartyModel);
        }


        [HttpPost("UpdateAssignParty/{id}/{AssignPartyName}/{ProductId}")]
        public async Task<IActionResult> UpdateAssignParty([FromRoute] long id, AssignPartyModel model)
        {

            if (ModelState.IsValid)
            {
                long Id = await _partyAssignRepository.UpdateAssignParty(model, id);
                if (Id > 0)
                {

                    return RedirectToAction(nameof(UpdateAssignParty), new { isSuccess = 0, Partyid = id });
                }
                return RedirectToAction(nameof(UpdateAssignParty), new { isSuccess = 1, Partyid = id });
            }

            return RedirectToAction("UpdateAssignParty");
        }

        public async Task<IActionResult> DelAassignParty(long id, AssignPartyModel model)
        {

            bool isDeleted = await _partyAssignRepository.DeleteAssignParty(model, id);


            if (isDeleted)
            {
                return RedirectToAction(nameof(PartyAssign));

            }
            return null;
        }

        [HttpGet]
        public async Task<JsonResult> NotAssigendProduct(long PartyId)
        {
            //List<prodList> products = new List<prodList>();

            var products = await _partyAssignRepository.getNotAssigendProduct(PartyId);
            return Json(products);
        }

        [HttpGet("SortByAssignId")]
        public IActionResult SortByAssignId()
        {

            return RedirectToAction("PartyAssign", new { isSort = 0 });
        }
        [HttpGet("SortByProduct")]
        public IActionResult SortByProduct()
        {
            return RedirectToAction("PartyAssign", new { isSort = 1 });
        }
        [HttpGet("SortByParty")]
        public IActionResult SortByParty()
        {
            return RedirectToAction("PartyAssign", new { isSort = 2 });
        }


    }
}
