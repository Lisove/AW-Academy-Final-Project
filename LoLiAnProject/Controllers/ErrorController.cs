using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLiAnProject.Controllers
{
    public class ErrorController : Controller
    {
        [Route("error/exception")]
        public IActionResult ServerError()
        {
            return View();
        }

        [Route("error/http/{id}")]
        public IActionResult HttpError(int id)
        {
            return View(id);
        }
    }
}
