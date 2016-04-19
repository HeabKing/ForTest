using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace _2016_04_17_EmitMapperStudy
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var a = new A
			{
				ReferenceTypeA = "I Am Class A",
				ValueTypeA = 666,
				Id = 1,
				Id2 = "2",
				Name = null,
				Age = null,
				Gender = true
			};
			#region 默认转换规则 - 按照属性名进行映射, 只要能相互转换就会进行相互转换, 不管是string => int Or int => string, 如果不能正确转换就会报出异常
			// 获取映射器
			//var mapper = EmitMapper.ObjectMapperManager.DefaultInstance.GetMapper<A, B>();
			// 进行映射1
			//var b1 = mapper.Map(a);
			// 进行映射2
			//var b2 = new B();
			//mapper.Map(a, b2);
			#endregion
			#region 自定义转换规则 - 空值赋值规则

			var mapperForNullValue = EmitMapper
				.ObjectMapperManager
				.DefaultInstance.GetMapper<A, B>(new DefaultMapConfig()
					.ConvertUsing<object, int>((o1) => o1 == null ? 11111 : Convert.ToInt32(o1)).MatchMembers((m1, m2) =>
					{
						if (m1 == "ReferenceTypeA")
						{
							return m2 == "ReferenceTypeB";
						}
						else
						{
							return m1 == m2;
						}
					}).ConvertUsing<A, B>(aaa =>
					{
						var newb = new EmitMapper.ObjectMapperManager().GetMapper<A, B>(
							new DefaultMapConfig().NullSubstitution<int?, int>(s=>-1)).Map(a);
						newb.Name = aaa.ReferenceTypeA + "HAHAHAHA";
						return newb;
					}));

			var b3 = mapperForNullValue.Map(a);
			#endregion
		}

		public class A
		{
			public string ReferenceTypeA { get; set; }
			public int ValueTypeA { get; set; }
			public int Id { get; set; }
			public string Id2 { get; set; }
			public string Name { get; set; }
			public int? Age { get; set; }
			public bool Gender { get; set; }
		}

		public class B
		{
			public string ReferenceTypeB { get; set; }
			public int ValueTypeB { get; set; }
			public string Id { get; set; }
			public int Id2 { get; set; }
			public string Name { get; set; }
			public int Age { get; set; }
			public bool Gender { get; set; }
		}
	}
}
