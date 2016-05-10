using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using _2016_05_06_LayeredMvcDemo.Application.Models;
using _2016_05_06_LayeredMvcDemo.DataAccess;
using _2016_05_06_LayeredMvcDemo.Web.Controllers;

namespace _2016_05_06_LayeredMvcDemo.Web.App_Start
{
	public class MyControllerFactory : DefaultControllerFactory, IControllerFactory
	{
		/// <summary>
		/// 将[组合根]放在这里, 导致表示层需要引用所有层, 因为[组合根]需要参考所有实现类型才有办法依赖类型的实例
		/// </summary>
		/// <param name="requestContext"></param>
		/// <param name="controllerName"></param>
		/// <returns></returns>
		public override IController CreateController(RequestContext requestContext, string controllerName)
		{
			if (controllerName == "Customer")
			{
				// 建立依赖对象并注入至新建立的Controller
				var service = new CustomerService();
				var controller = new CustomerController(service);
				return controller;
			}
			// 其他不需要特殊处理的controller类型交给MVC自带的工厂来建立
			return base.CreateController(requestContext, controllerName);
		}

		public override void ReleaseController(IController controller)
		{
			// 如需要释放其他对象资源, 可写在这里
			base.ReleaseController(controller);
		}
	}
}