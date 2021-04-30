using LoLiAnProject.Models;
using LoLiAnProject.Models.Entities;
using LoLiAnProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static LoLiAnProject.Models.HomeService;

namespace LoLiAnProject.Controllers
{
    public class HomeController : Controller
    {
        HomeService service;

        public HomeController(HomeService service)
        {
            this.service = service;
        }

        //--------- Start page--------//
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }


        //--------- About --------//
        [Route("home/about")]
        public IActionResult About()
        {
            return View();
        }

        //---------Register our day--------//
        [Authorize]
        [Route("home/register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [Authorize]
        [Route("home/register")]
        [HttpPost]
        public IActionResult Register(RegisterVM registerVM, StatsTable table)
        {
            if (!ModelState.IsValid)
                return View(registerVM);
            service.RegisterYourDay(registerVM);
            return RedirectToAction(nameof(Details), new { date = registerVM.Date.ToShortDateString() });
        }
        //-----------Register Previous Days--------------//

        [Authorize]
        [Route("home/registerPreviousDay/{date}")]
        [HttpGet]
        public IActionResult RegisterPreviousDay(DateTime date)
        {
            RegisterPreviousDayVM registerPreviousDayVM = new RegisterPreviousDayVM
            {
                Date = date
            };
            return View(registerPreviousDayVM);
        }

        [Authorize]
        [Route("home/registerPreviousDay/{date}")]
        [HttpPost]
        public IActionResult RegisterPreviousDay(RegisterPreviousDayVM registerPreviousDayVM, StatsTable table)
        {
            if (!ModelState.IsValid)
                return View(registerPreviousDayVM);
            service.RegisterYourPreviousDay(registerPreviousDayVM);
            return RedirectToAction(nameof(Details), new { date = registerPreviousDayVM.Date.ToShortDateString() });
        }

        //------------RangeInfo Page-----------//

        [Authorize]
        [Route("home/rangeinfo")]
        [HttpGet]
        public IActionResult RangeInfo()
        {
            return View();

        }
        //------------Owerview Page-----------//
        [Authorize]
        [Route("home/overview")]
        [HttpGet]
        public IActionResult Overview()
        {
            var model = service.GetOverViewVM();
            return View(model);
        }


        //-------------Details----------------//
        [Authorize]
        [Route("home/details/{date}")]
        [HttpGet]
        public IActionResult Details(DateTime date)
        {
            var details = service.GetDetailsByDate(date);

            if (details != null)
            {
                var detailsVM = service.GetDetailsVM(details);
                return View(detailsVM);
            }
            else return RedirectToAction(nameof(RegisterPreviousDay), new { date = date.ToShortDateString() });
        }



        //------------Edit--------------//
        [Authorize]
        [Route("home/detailsedit/{date}")]
        [HttpGet]
        public IActionResult DetailsEdit(DateTime date)
        {
            var details = service.GetDetailsByDate(date);
            DetailsEditVM editVM = service.GetDetailsEditVM(details);
            return View(editVM);
        }

        [Authorize]
        [Route("home/detailsedit/{date}")]
        [HttpPost]
        public IActionResult DetailsEdit(DetailsEditVM editVM, DateTime date)
        {
            if (!ModelState.IsValid)
                return View(editVM);

            service.EditDetails(editVM, date);
            return RedirectToAction(nameof(Details), new { date = editVM.Date.ToShortDateString() });
        }


        //------------Delete-----------------//

        [Authorize]
        [Route("home/details/{date}")]
        [HttpPost]
        public IActionResult deleteDetailsModal(DateTime date)
        {
            service.DeleteDetailsByDateModal(date);
            return RedirectToAction(nameof(Overview));
        }

        //---------------------Charts----------------//
        [Authorize]
        [Route("home/charts")]
        public ActionResult Charts()
        {
            List<DataPoint> dataPointsMood = new List<DataPoint>();
            List<DataPoint> dataPointsSleep = new List<DataPoint>();
            List<DataPoint> dataPointsMentalHealth = new List<DataPoint>();
            List<DataPoint> dataPointsStress = new List<DataPoint>();
            List<DataPoint> dataPointsPhysicalActivity = new List<DataPoint>();
            List<DataPoint> dataPointsPhysicalHealth = new List<DataPoint>();

            var statesList = service.GetDataToCharts();

            if (statesList.Count > 0)
            {
                // JavScript's base date is 1970-01-01
                var baseDate = new DateTime(1970, 1, 1);

                foreach (var item in statesList)
                {
                    // Need to convert DateTime to elapsed milliseconds since 1970-01-01 to
                    // comply with JavaScript Date
                    TimeSpan timestamp = new(item.Date.Ticks - baseDate.Ticks);
                    dataPointsMood.Add(new DataPoint(timestamp.TotalMilliseconds, item.Mood));
                }

                foreach (var item in statesList)
                {
                    TimeSpan timestamp = new(item.Date.Ticks - baseDate.Ticks);
                    dataPointsSleep.Add(new DataPoint(timestamp.TotalMilliseconds, item.Sleep));
                }

                foreach (var item in statesList)
                {
                    TimeSpan timestamp = new(item.Date.Ticks - baseDate.Ticks);
                    dataPointsMentalHealth.Add(new DataPoint(timestamp.TotalMilliseconds, item.MentalHealth));
                }

                foreach (var item in statesList)
                {
                    TimeSpan timestamp = new(item.Date.Ticks - baseDate.Ticks);
                    dataPointsStress.Add(new DataPoint(timestamp.TotalMilliseconds, item.Stress));
                }

                foreach (var item in statesList)
                {
                    TimeSpan timestamp = new(item.Date.Ticks - baseDate.Ticks);
                    dataPointsPhysicalActivity.Add(new DataPoint(timestamp.TotalMilliseconds, item.PhysicalActivity));
                }

                foreach (var item in statesList)
                {
                    TimeSpan timestamp = new(item.Date.Ticks - baseDate.Ticks);
                    dataPointsPhysicalHealth.Add(new DataPoint(timestamp.TotalMilliseconds, item.PhysicalHealth));
                }


                ViewBag.DataPointsMood = JsonConvert.SerializeObject(dataPointsMood);
                ViewBag.DataPointsSleep = JsonConvert.SerializeObject(dataPointsSleep);
                ViewBag.DataPointsMentalHealth = JsonConvert.SerializeObject(dataPointsMentalHealth);
                ViewBag.DataPointsStress = JsonConvert.SerializeObject(dataPointsStress);
                ViewBag.DataPointsPhysicalActivity = JsonConvert.SerializeObject(dataPointsPhysicalActivity);
                ViewBag.DataPointsPhysicalHealth = JsonConvert.SerializeObject(dataPointsPhysicalHealth);


                return View();
            }
            else
            {
                return RedirectToAction(nameof(EmptyChart));
            }

        }

        [Authorize]
        [Route("home/emptyChart")]
        [HttpGet]
        public IActionResult EmptyChart()
        {
            return View();
        }

    }
}