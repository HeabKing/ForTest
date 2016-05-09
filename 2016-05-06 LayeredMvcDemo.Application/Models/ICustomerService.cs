using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2016_05_06_LayeredMvcDemo.Domain.Models;

namespace _2016_05_06_LayeredMvcDemo.Application.Models
{
	/// <summary>
	/// 为了让表示层不直接依赖特定的服务类型
	/// </summary>
	public interface ICustomerService
	{
		Customer GetCustomerById(int id);
		List<Customer> GetCustomerList(Func<Customer, bool> filter);
	}
}
