using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2016_05_06_LayeredMvcDemo.Domain.Models;

namespace _2016_05_06_LayeredMvcDemo.DataAccess
{
	/// <summary>
	/// 改变数据访问框架的时候使用 - 为了让CustomerService不依赖特定的Repository实现类型
	/// </summary>
	public interface ICustomerRepository
	{
		Customer GetCustomerById(int id);
		IEnumerable<Customer> GetCustomerList(Func<Customer, bool> filter);
	}
}
