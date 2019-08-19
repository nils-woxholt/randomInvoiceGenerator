using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using invoice_generator.Models;

namespace invoice_generator.Controllers
{
    [Route("[controller]")]
    public class TemplateController : Controller
    {
        /// <summary>
        /// Get an order of the specified template
        /// </summary>
        /// <param name="templateID"></param>
        /// <returns></returns>

        [HttpGet("{templateID}")]
        public IActionResult Index(int templateID = 1)
        {

            var order = new OrderModel().GetRandomOrder();
            ViewBag.TemplateID = templateID;

            return View(order);
        }
       


    }
}