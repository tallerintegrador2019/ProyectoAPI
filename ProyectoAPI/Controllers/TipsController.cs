using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using ProyectoAPI.Models;

namespace ProyectoAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TipsController : ApiController
    {
        private todaviasirveDBEntities db = new todaviasirveDBEntities();

        // GET: api/Tips
        public IQueryable<Tips> GetTips()
        {
            return db.Tips;
        }

        // GET: api/Tips/5
        [ResponseType(typeof(Tips))]
        public IHttpActionResult GetTips(int id)
        {
            Tips tips = db.Tips.Find(id);
            if (tips == null)
            {
                return NotFound();
            }

            return Ok(tips);
        }

        // PUT: api/Tips/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTips(int id, Tips tips)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tips.id)
            {
                return BadRequest();
            }

            db.Entry(tips).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipsExists(id))
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

        // POST: api/Tips
        [ResponseType(typeof(Tips))]
        public IHttpActionResult PostTips(Tips tips)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tips.Add(tips);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tips.id }, tips);
        }

        // DELETE: api/Tips/5
        [ResponseType(typeof(Tips))]
        public IHttpActionResult DeleteTips(int id)
        {
            Tips tips = db.Tips.Find(id);
            if (tips == null)
            {
                return NotFound();
            }

            db.Tips.Remove(tips);
            db.SaveChanges();

            return Ok(tips);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipsExists(int id)
        {
            return db.Tips.Count(e => e.id == id) > 0;
        }
    }
}