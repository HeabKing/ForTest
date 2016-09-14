using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2016_09_14_AspNetCore_Identity.Model
{
    public class LoginViewModel
    {
		[Required]
	    public string UserName { get; set; }
		[Required]
	    public string Password { get; set; }
    }
}
