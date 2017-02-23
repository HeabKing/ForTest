using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _2017_02_22_Swagger.Controllers
{
	/// <summary>
	/// WebApi - Values
	/// </summary>
    //[Authorize]
    public class ValuesController : ApiController
    {
		/// <summary>
		/// 获取一个集合值
		/// </summary>
		/// <returns>返回一个集合</returns>
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
		/// 获取单个值
		/// </summary>
		/// <param name="id">传入要获取的元素的Id</param>
		/// <returns>返回要获取的值</returns>
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
