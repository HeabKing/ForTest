using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2016_09_27_System.Linq.IGrouping
{
    public class Program
    {
        public static void Main(string[] args)
        {
			IEnumerable<IGrouping<int, Student>> stu0 = Students.GroupBy(m => m.Age);
			// IGrouping 就是IEnumerable添加了一个Key元素
            // IEnumerable 1
            //		Key: 22		yield [王兴宇]
			// IEnumerable 2
			//		Key: 23		yield [何士雄, 冯浩, 李宁超, 景建威]
			// IEnumerable 3	
			//		Key: 24		yield [杜鹏]
	        var stu0List = stu0.ToList();
			IEnumerable<IEnumerable<Student>> stu2 = stu0List.Select(g => g.Select(s => s));
			// IGrouping 就是IEnumerable添加了一个Key元素
			// IEnumerable 1
			//		yield [王兴宇]
			// IEnumerable 2
			//		yield [何士雄, 冯浩, 李宁超, 景建威]
			// IEnumerable 3	
			//		yield [杜鹏]
			IEnumerable<Student> stu1 = stu0List.SelectMany(g => g.Select(s => s));
			// yield [王兴宇, 何士雄, 冯浩, 李宁超, 景建威, 杜鹏]

        }

		private static readonly List<Student> Students = new List<Student>
        {
            new Student { Name = "何士雄", Age = 23},
            new Student { Name = "王兴宇", Age = 22},
            new Student { Name = "冯浩", Age = 23},
            new Student { Name = "李宁超", Age = 23},
            new Student { Name = "杜鹏", Age = 24},
            new Student { Name = "景建威", Age = 23}
        };

        private class Student
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
