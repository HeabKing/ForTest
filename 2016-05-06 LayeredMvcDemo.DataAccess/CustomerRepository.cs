using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2016_05_06_LayeredMvcDemo.Domain.Models;

namespace _2016_05_06_LayeredMvcDemo.DataAccess
{
	/// <summary>
	/// 提供Customer的数据存取操作
	/// </summary>
	public class CustomerRepository : ICustomerRepository
	{
		private readonly SouthwindContext _db = new SouthwindContext();

		public Customer GetCustomerById(int id)
		{
			var query = from t in _db.Customers
						where t.Id == id
						select t;
			return query.FirstOrDefault();
		}

		public IEnumerable<Customer> GetCustomerList(Func<Customer, bool> filter)
		{
			var query = from t in _db.Customers
						select t;
			return query.Where(filter);
		}
	}
}
