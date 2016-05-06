using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2016_05_06_LayeredMvcDemo.Domain.Models;

namespace _2016_05_06_LayeredMvcDemo.DataAccess
{
	public class SouthwindContext : DbContext
	{
		public SouthwindContext() : base("SouthwindDB")
		{
			Database.SetInitializer<SouthwindContext>(new SouthwindDBinitializer());
		}
		public DbSet<Customer> Customers { get; set; }
	}

	/// <summary>
	/// 自定义的数据初始化程序 - 用途是数据库不存在的时候自动建立数据库
	/// </summary>
	public class SouthwindDBinitializer : CreateDatabaseIfNotExists<SouthwindContext>
	{
		public override void InitializeDatabase(SouthwindContext context)
		{
			base.InitializeDatabase(context);

			context.Customers.Add(new Customer
			{
				Id = 1,
				CompanyName = "Microsoft",
				Contact = "Michael"
			});
			context.Customers.Add(new Customer
			{
				Id = 2,
				CompanyName = "Oracle",
				Contact = "vivid"
			});
			context.Customers.Add(new Customer
			{
				Id = 3,
				CompanyName = "SimonTech",
				Contact = "Simon"
			});
			context.Customers.Add(new Customer
			{
				Id = 4,
				CompanyName = "Iosee",
				Contact = "Mark"
			});
			context.SaveChanges();
		}
	}
}
