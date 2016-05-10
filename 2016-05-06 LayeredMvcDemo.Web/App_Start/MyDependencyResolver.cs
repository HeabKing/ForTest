using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2016_05_06_LayeredMvcDemo.Application.Models;
using _2016_05_06_LayeredMvcDemo.Web.Controllers;

namespace _2016_05_06_LayeredMvcDemo.Web.App_Start
{
	public class MyDependencyResolver: System.Web.Mvc.IDependencyResolver
	{
		public object GetService(Type serviceType)
		{
			// 观察MVC框架有那些服务会通过dependency resolver来解析
			System.Diagnostics.Debug.WriteLine(serviceType.FullName);

			// 解析特定Controller
			if (serviceType == typeof(CustomerController))
			{
				var customerSvc = new CustomerService();
				var controller = new CustomerController(customerSvc);
				return controller;
			}
			// 不需要在此解析的类型, 必须返回null, 不可抛出异常
			return null;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			// 没有特别要解析的类型, 故返回空集合, 不可抛出异常
			return new List<object>();
		}
	}
}