using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2016_05_06_LayeredMvcDemo.DataAccess;
using _2016_05_06_LayeredMvcDemo.Domain.Models;

namespace _2016_05_06_LayeredMvcDemo.Application.Models
{
	public class CustomerServices
	{
		private readonly CustomerRepository _repository = new CustomerRepository();

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
