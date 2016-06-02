using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _2016_05_30_Console_Core_Study
{
    public class Program
    {
        public static void Main(string[] args)
        {
	        string dirCurrent = Directory.GetCurrentDirectory();
			Console.WriteLine(dirCurrent);
	        Debug.WriteLine(dirCurrent);
	        System.Console.ReadKey();
        }
    }
}
