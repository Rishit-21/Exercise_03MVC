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

        public async Task<IActionResult> PartyAssign()
        {
            var AssignParties = await _partyAssignRepository.GetAllAssignParty();
            return View(AssignParties);
        }

        public async Task<ViewResult> AddAssignParty(int isSuccess = 2, int id = 0)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.id = id;
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> AddAssignParty(AssignPartyModel model, [FromQuery] int Partyid)
        {

            if (ModelState.IsValid)
            {
                int id = await _partyAssignRepository.AddAssignParty(model);
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


        [HttpGet("UpdateAssignParty/{id}/{AssignPartyName}/{AssignProductName}")]
        public IActionResult UpdateAssignParty(int AssignPartyName, int AssignProductName, int isSuccess = 2, [FromRoute] int id = 0)
        {
            //ViewBag.isSuccess = isSuccess;
            ViewBag.id = id;
            //ViewBag.AssignPartyName = AssignPartyName;
            //ViewBag.AssignProductName = AssignProductName;

            AssignPartyModel assignPartyModel = new AssignPartyModel()
            {
                PartyId = AssignPartyName,
                ProductId = AssignProductName
            };

            return View("AddAssignParty", assignPartyModel);
        }


        [HttpPost("UpdateAssignParty/{id}/{AssignPartyName}/{AssignProductName}")]
        public async Task<IActionResult> UpdateAssignParty([FromRoute] int id, AssignPartyModel model)
        {

            if (ModelState.IsValid)
            {
               int Id= await _partyAssignRepository.UpdateAssignParty(model, id);
                if (Id > 0)
                {

                    return RedirectToAction(nameof(UpdateAssignParty), new { isSuccess = 0, Partyid = id });
                }
                return RedirectToAction(nameof(UpdateAssignParty), new { isSuccess = 1, Partyid = id });
            }

            return RedirectToAction("UpdateAssignParty");
        }

        public async Task<IActionResult> DelAassignParty(int id, AssignPartyModel model)
        {

            bool isDeleted = await _partyAssignRepository.DeleteAssignParty(model, id);


            if (isDeleted)
            {
                return RedirectToAction(nameof(PartyAssign));

            }
            return null;
        }

        [HttpGet]
        public async Task<JsonResult> NotAssigendProduct(int PartyId)
        {
            //List<prodList> products = new List<prodList>();

            var products = await _partyAssignRepository.getNotAssigendProduct(PartyId);
            return Json(products);
        }

    }
}
