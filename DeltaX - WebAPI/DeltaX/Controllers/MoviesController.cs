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
using DeltaX.Models;

namespace DeltaX.Controllers
{
    public class MoviesController : ApiController
    {
        private DeltaXEntities db = new DeltaXEntities();

        // GET: api/Movies
        public IEnumerable<Object> GetMovies1()
        {
            return db.Movies1.Select(x => new { MoviesID = x.MoviesID,
                Name = x.Name, YEAROFRELEASE = x.YEAROFRELEASE, PLOT = x.PLOT, PosterImage = x.PosterImage, Producer = db.Persons.Where(y => y.PersonsID == x.Producer).Select(z=> new { Name=z.Name , PersonID=z.PersonsID}).FirstOrDefault()}).ToList<Object>();
        }

        // GET: api/Movies/5
        [ResponseType(typeof(Movies))]
        public IHttpActionResult GetMovies(int id)
        {
            Movies movies = db.Movies1.Find(id);
            if (movies == null)
            {
                return NotFound();
            }

            return Ok(movies);
        }
        
        [ResponseType(typeof(Persons))]
        public IHttpActionResult GetMovies([FromUri] string name)
        {
            var res = db.Persons.Where(x => x.Name == name).Select(x=>new { PersonID = x.PersonsID, Name = x.Name, DOB = x.DOB }).FirstOrDefault() ;
            if (res == null)
            {
                return NotFound();
            }

            return Ok(res);
        }

        // PUT: api/Movies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMovies(int id, Movies movies)
            {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movies.MoviesID)
            {
                return BadRequest();
            }

            db.Entry(movies).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviesExists(id))
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

        // POST: api/Movies
        [ResponseType(typeof(Movies))]
        public IHttpActionResult PostMovies(Movies movies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Movies1.Add(movies);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = movies.MoviesID }, movies);
        }

        // DELETE: api/Movies/5
        [ResponseType(typeof(Movies))]
        public IHttpActionResult DeleteMovies(int id)
        {
            Movies movies = db.Movies1.Find(id);
            if (movies == null)
            {
                return NotFound();
            }
            db.Movies1.Remove(movies);

            var res = db.MoviesActors1.Where(x => x.MovieID == id);
            foreach (var item in res)
            {
                db.MoviesActors1.Remove(item);
            }
            db.SaveChanges();

            return Ok(movies);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MoviesExists(int id)
        {
            return db.Movies1.Count(e => e.MoviesID == id) > 0;
        }
    }
}