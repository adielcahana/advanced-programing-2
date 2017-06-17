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
using WebServer.Models;

namespace WebServer.Controllers
{
    public class RanksController : ApiController
    {
        private UserContext db = new UserContext();

        // GET: api/Ranks
        public IQueryable<Rank> GetRanks()
        {
            return db.Ranks;
        }

        // GET: api/Ranks/5
        [ResponseType(typeof(Rank))]
        public IHttpActionResult GetRank(string id)
        {
            Rank rank = db.Ranks.Find(id);
            if (rank == null)
            {
                return NotFound();
            }

            return Ok(rank);
        }

        // PUT: api/Ranks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRank(string id, Rank rank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rank.Id)
            {
                return BadRequest();
            }

            db.Entry(rank).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RankExists(id))
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

        // POST: api/Ranks
        [ResponseType(typeof(Rank))]
        public IHttpActionResult PostRank(Rank rank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ranks.Add(rank);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RankExists(rank.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = rank.Id }, rank);
        }

        // DELETE: api/Ranks/5
        [ResponseType(typeof(Rank))]
        public IHttpActionResult DeleteRank(string id)
        {
            Rank rank = db.Ranks.Find(id);
            if (rank == null)
            {
                return NotFound();
            }

            db.Ranks.Remove(rank);
            db.SaveChanges();

            return Ok(rank);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RankExists(string id)
        {
            return db.Ranks.Count(e => e.Id == id) > 0;
        }
    }
}