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
		private readonly SouthwindContext db;

		public CustomerService()
		{
			// 提供默认的DbContext对象
			db = new SouthwindContext();
		}

		/// <summary>
		/// 调用方注入DbContext对象
		/// </summary>
		public CustomerService(SouthwindContext dbContext)
		{
			this.db = dbContext;
		}

		public Customer GetCustomerById(int id)
		{
			return db.Customers.Find(id);
		}

		public List<Customer> GetCustomerList(Func<Customer, bool> filter)
		{
			return db.Customers.Where(filter).ToList();
		}
	}
}
