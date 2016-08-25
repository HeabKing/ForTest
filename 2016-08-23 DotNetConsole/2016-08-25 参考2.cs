// 使用 System.CodeDom 动态创建.Net程序 http://www.c-sharpcorner.com/article/dynamically-creating-applications-using-system-codedom/
// 应用场景: 创建一个灵活的可拓展的应用程序
using System;
using System.CodeDom;	// System.CodeDom 包含创建程序集的类和类型
using System.CodeDom.Compiler;

using System.Linq;

namespace _2016_08_23_DotNetConsole
{
	/// <summary>
	/// dotnet console 1.1 support mutiple expression excute
	/// </summary>
	public class DotNetConsole11
	{
		public static void Main()
		{
			var generator = new CodeGenerator();
			generator.CreateNamespace();
			generator.CreateUsing();
			generator.CreateClass();
			generator.CreateMember();
			generator.CreateProperty();
			generator.CreateMethod();
			generator.CreateEntryPoint();
			generator.SaveAssembly();
		}
	}

	public class CodeGenerator
	{
		// 用于 添加/创建 命名空间
		// 在这个里面添加的命名空间会用于 dotnet console
		private CodeNamespace _myNameSpace;
		private CodeTypeDeclaration _myClass;
		private CodeCompileUnit _myAssembly;

		public void CreateNamespace()
		{
			_myNameSpace = new CodeNamespace("MyNamespace");
		}

		public void CreateUsing()
		{
			_myNameSpace.Imports.Add(new CodeNamespaceImport("System"));
			_myNameSpace.Imports.Add(new CodeNamespaceImport("System.Drawing"));
			_myNameSpace.Imports.Add(new CodeNamespaceImport("System.Windows.Forms"));
		}

		public void CreateClass()
		{
			// 创建一个类
			_myClass = new CodeTypeDeclaration
			{
				Name = "MyClass",   // 类名
				IsClass = true,
				Attributes = MemberAttributes.Public
			};
			// 将类添加到我们的命名空间中
			_myNameSpace.Types.Add(_myClass);
		}

		public void CreateMember()
		{
			// 创建一个字段
			var field = new CodeMemberField(typeof(string), "strMessage");
			// 将字段添加到类中
			_myClass.Members.Add(field);
		}

		public void CreateProperty()
		{
			// 创建一个属性
			var property = new CodeMemberProperty
			{
				Name = "Message",	// 属性名
				Type = new CodeTypeReference(typeof(string)),
				Attributes = MemberAttributes.Public
			};
			// 将属性添加到类中
			_myClass.Members.Add(property);
			// 将代码片段添加到属性中
			// 如果需要, 我们可以添加自定义的校验代码
			// 使用代码片段表达式类
			// getter
			var getSnippet = new CodeSnippetExpression("return strMessage");
			// setter
			var setSnippet = new CodeSnippetExpression("strMessage = value");
			// 将代码片段添加到属性中
			property.GetStatements.Add(getSnippet);
			property.SetStatements.Add(setSnippet);
		}

		/// <summary>
		/// 创建一个方法, 请仔细一点, 因为任何语法错误都是在运行时发生的, 所以我们没有任何办法发现和处理他们, 编译器会不分青红皂白的直接将错误当成异常抛出来
		/// </summary>
		public void CreateMethod()
		{
			var method = new CodeMemberMethod
			{
				Name = "AddNumbers",
			};
			// 创建两个参数
			var cpd1 = new CodeParameterDeclarationExpression(typeof(int), "a");
			var cpd2 = new CodeParameterDeclarationExpression(typeof(int), "b");
			// 将两个参数添加到方法中
			method.Parameters.AddRange(new CodeParameterDeclarationExpression[] {cpd1, cpd2});
			// 添加方法的返回值
			method.ReturnType = new CodeTypeReference(typeof(int));
			// 添加方法的定义
			var snippet1 = new CodeSnippetExpression("System.Console.WriteLine(\" Adding:\" + a + \" And \" + b)");
			// 将结果返回
			var snippet2 = new CodeSnippetExpression("return a + b");
			// 将代码片段转化为表达式语句
			var stmt1 = new CodeExpressionStatement(snippet1);
			var stmt2 = new CodeExpressionStatement(snippet2);
			// 将表达式语句添加到方法中
			method.Statements.Add(stmt1);
			method.Statements.Add(stmt2);
			// 访问
			method.Attributes = MemberAttributes.Public;
			// 将方法添加到类中
			_myClass.Members.Add(method);
		}

		/// <summary>
		/// 创建Main方法
		/// </summary>
		public void CreateEntryPoint()
		{
			// 创建一个对象并将其命名为Main (Entry Point, 入口点)
			var main = new CodeEntryPointMethod
			{
				Name = "Main",
				Attributes = MemberAttributes.Public | MemberAttributes.Static,
			};
			// 定义Main方法
			// 创建我们声明的类的一个对象, 并调用其方法
			var exp1 = new CodeSnippetExpression("MyClass cls = new MyClass()");
			var exp2 = new CodeSnippetExpression("cls.Message = \"Hello World\"");
			var exp3 = new CodeSnippetExpression("Console.WriteLine(cls.Message)");
			var exp4 = new CodeSnippetExpression("Console.WriteLine(\"Answer: {0}\", cls.AddNumbers(10, 20))");
			var exp5 = new CodeSnippetExpression("Console.ReadLine()");
			//Create expression statements for the snippets
			var ces1 = new CodeExpressionStatement(exp1);
			var ces2 = new CodeExpressionStatement(exp2);
			var ces3 = new CodeExpressionStatement(exp3);
			var ces4 = new CodeExpressionStatement(exp4);
			var ces5 = new CodeExpressionStatement(exp5);
			//Add the expression statements to the main method. 
			main.Statements.Add(ces1);
			main.Statements.Add(ces2);
			main.Statements.Add(ces3);
			main.Statements.Add(ces4);
			main.Statements.Add(ces5);
			//Add the main method to the class
			_myClass.Members.Add(main);
		}

		/// <summary>
		/// 编译此类并创建为程序集
		/// </summary>
		public void SaveAssembly()
		{
			// 创建一个Assembly对象
			_myAssembly = new CodeCompileUnit();
			// 将命名空间添加到程序集
			_myAssembly.Namespaces.Add(_myNameSpace);
			// 添加如下的编译参数
			var comParam = new CompilerParameters(new string[] {"mscorlib.dll"});
			comParam.ReferencedAssemblies.Add("System.dll");
			comParam.ReferencedAssemblies.Add("System.Drawing.dll");
			comParam.ReferencedAssemblies.Add("System.Windows.Forms.dll");
			// 标识编译器是否需要输出到内存
			comParam.GenerateInMemory = false;
			// 标识输出是否可以执行
			comParam.GenerateExecutable = true;
			// 提供有入口函数Main的类的名字
			comParam.MainClass = "MyNamespace.MyClass";
			// 生成的程序集存在的位置, 同名会替换
			comParam.OutputAssembly = @"C:\Users\HeabK\Desktop\dotnetConsole.exe";
			// 创建C#编译器实例, 将此Assembly对象传递进去
			var ccp = new Microsoft.CSharp.CSharpCodeProvider();
			ICodeCompiler icc = ccp.CreateCompiler();
			// CompileAssemblyFromDom 会返回错误列表(如果有)或创建的各个程序集的路径
			var compres = icc.CompileAssemblyFromDom(comParam, _myAssembly);
			if (compres.Errors.Count > 0)
			{
				compres.Errors.Cast<object>().ToList().ForEach(Console.WriteLine);
			}
			else
			{
				
			}
		}
	}
}
