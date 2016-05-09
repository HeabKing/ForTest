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
	    private readonly ICustomerService _customerService;

		/// <summary>
		/// 构造函数注入服务层
		/// </summary>
		/// <param name="customerService"></param>
	    public CustomerController(ICustomerService customerService)
	    {
		    _customerService = customerService;
	    }

	    // GET: Customer
		public ActionResult Index()
		{
			var customers = _customerService.GetCustomerList(cust => cust.Id < 4);
            return View(customers);
        }
    }
}