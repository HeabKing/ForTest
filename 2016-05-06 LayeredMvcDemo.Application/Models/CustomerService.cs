using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2016_05_06_LayeredMvcDemo.DataAccess;
using _2016_05_06_LayeredMvcDemo.Domain.Models;

namespace _2016_05_06_LayeredMvcDemo.Application.Models
{
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _repository;

		/// <summary>
		/// 使用构造函数注入使得Service不依赖特定的Repository实现类型
		/// </summary>
		/// <param name="repository"></param>
		public CustomerService(ICustomerRepository repository)
		{
			_repository = repository;
		}

		public Customer GetCustomerById(int id)
		{
			return _repository.GetCustomerById(id);
		}

		public List<Customer> GetCustomerList(Func<Customer, bool> filter)
		{
			return _repository.GetCustomerList(filter).ToList();
		}
	}
}
