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
    public class PartyController : Controller
    {
        private readonly IPartyRepository _partyRepository = null;
        private readonly IWebHostEnvironment _webHostEnviroment = null;

        public PartyController(IPartyRepository partyRepository, IWebHostEnvironment webHostEnviroment)
        {
            _partyRepository = partyRepository;
            _webHostEnviroment = webHostEnviroment;
        }

        public async Task<IActionResult> Party(string isSort = "Ase0", string sort = "asec")
        {
            var parties = await _partyRepository.GetAllParties();
            if (isSort == "Ase1")
            {
                ViewBag.sort = sort;
                return View(parties.OrderBy(x => x.partyName));
            }
            if (isSort == "desc0")
            {
                ViewBag.sort = sort;
                return View(parties.OrderByDescending(x => x.Id));

            }
            if (isSort == "desc1")
            {
                ViewBag.sort = sort;
                return View(parties.OrderByDescending(x => x.partyName));

            }
            ViewBag.sort = sort;
            return View(parties);
        }

        //[HttpGet ("AddParty")]
        public async Task<ViewResult> AddParty(int isSuccess = 2, int id = 0)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddParty(partyModel model, [FromQuery] long Partyid)
        {

            if (ModelState.IsValid)
            {
                long id = await _partyRepository.AddParty(model);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddParty), new { isSuccess = 0, Partyid = id });
                }
                //ViewBag.isSucces = true;
                //ViewBag.id = 0;
                return RedirectToAction(nameof(AddParty), new { isSuccess = 1, Partyid = id });
            }
            return View("AddParty");
        }



        [HttpGet("EditParty/{id}/{PartyName}")]
        public IActionResult EditParty(long id, string PartyName, int isSuccess = 2)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.id = id;

            //ViewBag.PartyName = PartyName;

            var model = new partyModel()
            {

                partyName = PartyName
            };


            return View("AddParty", model);
        }


        [HttpPost]
        [Route("EditParty/{id}/{PartyName}")]
        public async Task<IActionResult> EditParty(partyModel model, [FromRoute] long id)
        {

            if (ModelState.IsValid)
            {
                long Id = await _partyRepository.UpdateParty(model, id);
                if (Id > 0)
                {
                    return RedirectToAction(nameof(EditParty), new { isSuccess = 0, Partyid = id });
                }
                return RedirectToAction(nameof(EditParty), new { isSuccess = 1, Partyid = id });
            }

            return View("AddParty");
        }


        //[Route("{Delid}")]
        public async Task<IActionResult> DelParty(long id, partyModel model)
        {

            bool isDeleted = await _partyRepository.DeleteParty(model, id);


            if (isDeleted)
            {
                return RedirectToAction(nameof(Party));

            }
            return null;
        }
        string sort = "dsec";
        [HttpGet("SortPartyId/{isSortId}")]
        public IActionResult SortPartyId(string isSortId)
        {

            if (isSortId == "desc")
            {

                return RedirectToAction("Party", new { isSort = "Ase0", sort = "asec" });
            }
            return RedirectToAction("Party", new { isSort = "desc0", sort = "desc" });

        }
        [HttpGet("SortParty/{isSortNme}")]
        public IActionResult SortParty(string isSortNme)
        {

            if (isSortNme == "descName")
            {
                return RedirectToAction("Party", new { isSort = "Ase1", sort = "ascName" });

            }
            return RedirectToAction("Party", new { isSort = "desc1", sort = "descName" });

        }


    }
}
