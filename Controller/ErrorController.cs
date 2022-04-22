using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWeb
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        [Route("Error/{StatusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch(statusCode)
            {
                case 404:
                    ViewBag.ErrorMessege = "Sorry Your Request we can not found";
                    break;

            }
            return View("NotFound");
        }
    }
}
