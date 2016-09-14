using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2016_09_14_AspNetCore_Identity.Model.AccountViewModels
{
	public class RegisterViewModel
	{
		[Required]
		[Display(Name = "用户名")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "密码")]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "确认密码")]
		[Compare("Password", ErrorMessage = "两次输入密码不一致")]
		public string ComfirmPwd { get; set; }
	}
}
