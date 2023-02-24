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
using Lesson1.Models;
using System.Diagnostics;

namespace Lesson1.Controllers
{
    public class ResortDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //public int ResortId { get; private set; }
        //public string ResortName { get; private set; }
        //public string address { get; private set; }
        //public string description { get; private set; }

        // GET: api/ResortData/ListResorts
        [HttpGet]
        [ResponseType(typeof(ResortDto))]
        public IHttpActionResult ListResorts()
        {
            List<Resort> Resorts = db.Resorts.ToList();
            List<ResortDto> ResortDtos = new List<ResortDto>();

            Resorts.ForEach(a => ResortDtos.Add(new ResortDto()
            {
                ResortId = a.ResortId,
                ResortName = a.ResortName,
                address = a.address,
                description = a.description,
                NumberOfTrailsOpen = a.NumberOfTrailsOpen,
                Contact = a.Contact,
                WheelchairAccessible = a.WheelchairAccessible,
                NumberOfRestaurants = a.NumberOfRestaurants

            }));
            return Ok(ResortDtos);
        }


        // GET: api/ResortData/FindResort/5 
        [ResponseType(typeof(ResortDto))]
        [HttpGet]
        public IHttpActionResult FindResort(int id)
        {
            Resort Resort = db.Resorts.Find(id);
            ResortDto ResortDto = new ResortDto()
            {
                ResortId = Resort.ResortId,
                ResortName = Resort.ResortName,
                address = Resort.address,
                description = Resort.description,
                NumberOfTrailsOpen = Resort.NumberOfTrailsOpen,
                Contact = Resort.Contact,
                WheelchairAccessible = Resort.WheelchairAccessible,
                NumberOfRestaurants = Resort.NumberOfRestaurants

            };
            if (Resort == null)
            {
                return NotFound();
            }

            return Ok(ResortDto);
        }

        // POST: api/ResortData/UpdateResort/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateResort(int id, Resort resort)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resort.ResortId)
            {
                return BadRequest();
            }

            db.Entry(resort).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResortExists(id))
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

        // POST: api/ResortData/AddResort
        [ResponseType(typeof(Resort))]
        [HttpPost]

        public IHttpActionResult AddResort(Resort resort)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Resorts.Add(resort);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = resort.ResortId }, resort);
        }

        // POST: api/ResortData/DeleteResort/5
        [ResponseType(typeof(Resort))]
        [HttpPost]
        public IHttpActionResult DeleteResort(int id)
        {
            Resort resort = db.Resorts.Find(id);
            if (resort == null)
            {
                return NotFound();
            }

            db.Resorts.Remove(resort);
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

        private bool ResortExists(int id)
        {
            return db.Resorts.Count(e => e.ResortId == id) > 0;
        }
    }
}