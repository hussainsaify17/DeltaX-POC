using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using DeltaX.Models;
using System.Collections.Generic;

namespace DeltaX.Controllers
{
    public class PersonsController : ApiController
    {
        private DeltaXEntities db = new DeltaXEntities();

        // GET: api/Persons
        public IQueryable<Persons> GetPersons()
        {
            return db.Persons;
        }

        // GET: api/Persons/personType
        [ResponseType(typeof(Persons))]
        public IHttpActionResult GetPersons([FromUri] string personType)
        {
            dynamic res ;
            if (personType == "producer")
            {
                res = db.Persons.Where(x => x.Designation == personType).Select(y => new { PersonID = y.PersonsID, Name = y.Name,Bio = y.Bio,DOB = y.DOB,Sex = y.Sex }).ToList();
            }
            else 
            {
                res = db.Persons.Where(x => x.Designation == personType).Select(y => new { PersonID = y.PersonsID, Name = y.Name, Bio = y.Bio, DOB = y.DOB, Sex = y.Sex }).ToList();
            }
            if (res == null)
            {
                return NotFound();
            }

            return Ok(res);
        }

        // PUT: api/Persons/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersons(int id, Persons persons)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != persons.PersonsID)
            {
                return BadRequest();
            }
            persons.DOB.ToShortDateString();

            db.Entry(persons).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonsExists(id))
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

        // POST: api/Persons
        [ResponseType(typeof(Persons))]
        public IHttpActionResult PostPersons(Persons persons)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Persons.Add(persons);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = persons.PersonsID }, persons);
        }

        // DELETE: api/Persons/5
        [ResponseType(typeof(Persons))]
        public IHttpActionResult DeletePersons(int id)
        {
            Persons persons = db.Persons.Find(id);
            if (persons == null)
            {
                return NotFound();
            }

            dynamic res = db.MoviesActors1.Where(x => x.PersonID == id).ToList();

            if(res.Count == 0)
            res = db.Movies1.Where(x => x.Producer == id).ToList();

            if (res.Count >0)
            {
                return Json("Please delete Movies related to this person..!!");
            }

            db.Persons.Remove(persons);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonsExists(int id)
        {
            return db.Persons.Count(e => e.PersonsID == id) > 0;
        }
    }
}