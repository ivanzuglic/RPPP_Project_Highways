using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RPPP12.Controllers
{
    public class ProbniController : Controller
    {
        public IActionResult Index()
        {
            return View("Layout");
        }
    }
}