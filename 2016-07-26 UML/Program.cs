using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_07_26_UML
{
	public class BuilderPattern
	{
		public static void Main()
		{
			var director = new Director();
			var productA = director.GetProductA();
			var productB = director.GetProductB();
			productA.ShowProduct();
			productB.ShowProduct();

			Product productC = new Product();    // Builder - 创建, 初始化的逻辑
			productC.Name = "奔驰";                // Builder, Director - 进行初始化
			productC.Type = "A7";                // 建造者比工厂多了一个Director
			productC.ShowProduct();                // 测试
		}
	}

	/// <summary>
	/// 创建复杂类型
	/// </summary>
	public class Product
	{
		public string Name { get; set; }
		public string Type { get; set; }

		public void ShowProduct()
		{
			Debug.WriteLine($"Name: {Name}");
			Debug.WriteLine($"Type: {Type}");
		}
	}

	/// <summary>
	/// 抽象构造类 - 将建造的具体过程交给子类(拓展性)
	/// </summary>
	public abstract class AbstractBuilder
	{
		/// <summary>
		/// 建造产品
		/// </summary>
		/// <param name="name"></param>
		/// <param name="type"></param>
		public abstract void SetPart(string name, string type);
		/// <summary>
		/// 返回产品
		/// </summary>
		/// <returns></returns>
		public abstract Product GetProduct();
	}

	/// <summary>
	/// 构造类 - 实现构造Product
	/// </summary>
	public class Builder : AbstractBuilder
	{
		private Product _product;

		public override void SetPart(string name, string type)
		{
			_product = new Product { Name = name, Type = type };
		}

		public override Product GetProduct()
		{
			return _product;
		}
	}

	public class Director
	{
		private readonly AbstractBuilder _builder = new Builder();

		public Product GetProductA()
		{
			_builder.SetPart("宝马", "X7");
			return _builder.GetProduct();
		}

		public Product GetProductB()
		{
			_builder.SetPart("奥迪", "Q5");
			return _builder.GetProduct();
		}
	}


}
