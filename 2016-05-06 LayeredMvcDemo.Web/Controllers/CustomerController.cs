using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2016_05_06_LayeredMvcDemo.Application.Models;

namespace _2016_05_06_LayeredMvcDemo.Web.Controllers
{
    public class CustomerController : Controller
    {
	    private readonly CustomerServices _customerService = new CustomerServices();
		// GET: Customer
		public ActionResult Index()
		{
			var customers = _customerService.GetCustomerList(cust => cust.Id < 4);
            return View(customers);
        }
    }
}