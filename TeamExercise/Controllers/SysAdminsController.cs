using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TeamExercise.Data;
using TeamExercise.Models;

namespace TeamExercise.Controllers
{
    public class SysAdminsController : ApiController
    {
        private TeamExerciseDbContext db = new TeamExerciseDbContext();

        // GET: api/SysAdmins
        public IQueryable<SysAdmin> GetSysAdmins()
        {
            return db.SysAdmins;
        }

        // GET: api/SysAdmins/5
        [ResponseType(typeof(SysAdmin))]
        public IHttpActionResult GetSysAdmin(int id)
        {
            SysAdmin sysAdmin = db.SysAdmins.Find(id);
            if (sysAdmin == null)
            {
                return NotFound();
            }

            return Ok(sysAdmin);
        }

        // PUT: api/SysAdmins/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSysAdmin(int id, SysAdmin sysAdmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sysAdmin.Id)
            {
                return BadRequest();
            }

            db.Entry(sysAdmin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SysAdminExists(id))
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

        // POST: api/SysAdmins
        [ResponseType(typeof(SysAdmin))]
        public IHttpActionResult PostSysAdmin(SysAdmin sysAdmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SysAdmins.Add(sysAdmin);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sysAdmin.Id }, sysAdmin);
        }

        // DELETE: api/SysAdmins/5
        [ResponseType(typeof(SysAdmin))]
        public IHttpActionResult DeleteSysAdmin(int id)
        {
            SysAdmin sysAdmin = db.SysAdmins.Find(id);
            if (sysAdmin == null)
            {
                return NotFound();
            }

            db.SysAdmins.Remove(sysAdmin);
            db.SaveChanges();

            return Ok(sysAdmin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SysAdminExists(int id)
        {
            return db.SysAdmins.Count(e => e.Id == id) > 0;
        }
    }
}