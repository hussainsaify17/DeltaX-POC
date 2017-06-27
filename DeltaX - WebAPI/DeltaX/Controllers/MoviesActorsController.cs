using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using DeltaX.Models;

namespace DeltaX.Controllers
{
    public class MoviesActorsController : ApiController
    {
        private DeltaXEntities db = new DeltaXEntities();        

        // GET: api/MoviesActors/MovieID
        [ResponseType(typeof(Persons))]
        public IHttpActionResult GetMoviesActors([FromUri] int movieID)
        {
            var res = db.MoviesActors1.Where(x => x.MovieID == movieID).Select(y=>new { Name = db.Persons.Where(z=>z.PersonsID == y.PersonID).Select(a=>a.Name).FirstOrDefault(), PersonID = db.Persons.Where(z => z.PersonsID == y.PersonID).Select(a => a.PersonsID).FirstOrDefault() });
            if (res == null)
            {
                return NotFound();
            }

            return Ok(res);
        }

        // PUT: api/MoviesActors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMoviesActors(int id, MoviesActors moviesActors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != moviesActors.MoviesActorsID)
            {
                return BadRequest();
            }

            db.Entry(moviesActors).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviesActorsExists(id))
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

        // POST: api/MoviesActors
        [ResponseType(typeof(MoviesActors))]
        public IHttpActionResult PostMoviesActors(MoviesActors moviesActors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MoviesActors1.Add(moviesActors);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = moviesActors.MoviesActorsID }, moviesActors);
        }

        // DELETE: api/MoviesActors/5
        [ResponseType(typeof(MoviesActors))]
        public IHttpActionResult DeleteMoviesActors(int id)
        {
            MoviesActors moviesActors = db.MoviesActors1.Find(id);
            if (moviesActors == null)
            {
                return NotFound();
            }

            db.MoviesActors1.Remove(moviesActors);
            db.SaveChanges();

            return Ok(moviesActors);
        }

        // DELETE: Remove actors from movie
        [ResponseType(typeof(MoviesActors))]
        public IHttpActionResult DeleteMoviesActors([FromUri]int movieId,int personID)
        {
            MoviesActors moviesActors = db.MoviesActors1.FirstOrDefault(x=>x.MovieID==movieId &&x.PersonID== personID);
            if (moviesActors == null)
            {
                return NotFound();
            }

            db.MoviesActors1.Remove(moviesActors);
            db.SaveChanges();

            return Ok(moviesActors);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MoviesActorsExists(int id)
        {
            return db.MoviesActors1.Count(e => e.MoviesActorsID == id) > 0;
        }
    }
}