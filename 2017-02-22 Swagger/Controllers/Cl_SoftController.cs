using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using _2017_02_22_Swagger.Models;

namespace _2017_02_22_Swagger.Controllers
{
    public class Cl_SoftController : ApiController
    {
        private ZxxkEntities db = new ZxxkEntities();

        // GET: api/Cl_Soft
        public IQueryable<Cl_Soft> GetCl_Soft()
        {
            return db.Cl_Soft;
        }

        // GET: api/Cl_Soft/5
        [ResponseType(typeof(Cl_Soft))]
        public async Task<IHttpActionResult> GetCl_Soft(int id)
        {
            Cl_Soft cl_Soft = await db.Cl_Soft.FindAsync(id);
            if (cl_Soft == null)
            {
                return NotFound();
            }

            return Ok(cl_Soft);
        }

        // PUT: api/Cl_Soft/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCl_Soft(int id, Cl_Soft cl_Soft)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cl_Soft.SoftID)
            {
                return BadRequest();
            }

            db.Entry(cl_Soft).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Cl_SoftExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cl_Soft
        [ResponseType(typeof(Cl_Soft))]
        public async Task<IHttpActionResult> PostCl_Soft(Cl_Soft cl_Soft)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cl_Soft.Add(cl_Soft);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cl_Soft.SoftID }, cl_Soft);
        }

        // DELETE: api/Cl_Soft/5
        [ResponseType(typeof(Cl_Soft))]
        public async Task<IHttpActionResult> DeleteCl_Soft(int id)
        {
            Cl_Soft cl_Soft = await db.Cl_Soft.FindAsync(id);
            if (cl_Soft == null)
            {
                return NotFound();
            }

            db.Cl_Soft.Remove(cl_Soft);
            await db.SaveChangesAsync();

            return Ok(cl_Soft);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Cl_SoftExists(int id)
        {
            return db.Cl_Soft.Count(e => e.SoftID == id) > 0;
        }
    }
}