using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Lesson1.Models;
using Microsoft.Owin.BuilderProperties;
using System.Diagnostics;
using Lesson1.Migrations;
using Trail = Lesson1.Models.Trail;



namespace Lesson1.Controllers
{
    public class TrailDataController : ApiController
    {
        //public int TrailId { get; set; }

        //public int ResortId { get; set; }

        //public int BeginnerTrails { get; set; }
        //public int IntermediateTrails { get; set; }
        //public int AdvancedTrails { get; set; }
        //public int TerrainPark { get; set; }
        //public int TubingPark { get; set; }
        

        private ApplicationDbContext db = new ApplicationDbContext();
        

        // GET: api/TrailData/ListTrail
        [HttpGet]
        [ResponseType(typeof(TrailDto))]
        public IHttpActionResult ListTrails()
        {
            List<Trail> Trails = db.Trails.ToList();
            List<TrailDto> TrailDtos = new List<TrailDto>();

            Trails.ForEach(t => TrailDtos.Add(new TrailDto()
            {   
                TrailId = t.TrailId,
                ResortId = t.ResortId,
                ResortName = t.Resort.ResortName,
                BeginnerTrails = t.BeginnerTrails,
                IntermediateTrails = t.IntermediateTrails,
                AdvancedTrails = t.AdvancedTrails,
                TerrainPark = t.TerrainPark,
                TubingPark = t.TubingPark
               
            }));
            Debug.WriteLine(TrailDtos);
            return Ok(TrailDtos);
        }

        [HttpGet]
        [ResponseType(typeof(TrailDto))]      
        public IHttpActionResult ListTrailsForResort(int id)
        {
            List<Trail> Trails = db.Trails.Where(t => t.ResortId == id).ToList();
            List<TrailDto> TrailDtos = new List<TrailDto>();

            Trails.ForEach(t => TrailDtos.Add(new TrailDto()
            {
                TrailId = t.TrailId,
                ResortId = t.ResortId,
                BeginnerTrails = t.BeginnerTrails,
                IntermediateTrails = t.IntermediateTrails,
                AdvancedTrails = t.AdvancedTrails,
                TerrainPark = t.TerrainPark,
                TubingPark = t.TubingPark

            }));
            return Ok(TrailDtos);
        }

        // GET: api/TrailData/FindTrail/5
        [ResponseType(typeof(TrailDto))]
        [HttpGet]
        public IHttpActionResult FindTrail(int id)
        {
            Trail Trail = db.Trails.Find(id);
            //Resort Resort = (Resort)db.Resorts.Where(a => a.ResortId == id);
            TrailDto TrailDto = new TrailDto()
            {
                TrailId = Trail.TrailId,
                ResortId = Trail.ResortId,
                ResortName = Trail.Resort.ResortName,
                BeginnerTrails = Trail.BeginnerTrails,
                IntermediateTrails = Trail.IntermediateTrails,
                AdvancedTrails = Trail.AdvancedTrails,
                TerrainPark = Trail.TerrainPark,
                TubingPark = Trail.TubingPark,
  

            };
            if (Trail == null)
            {
                return NotFound();
            }

            return Ok(TrailDto);
        }

        // POST: api/TrailData/UpdateTrail/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTrail(int id, Trail trail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trail.TrailId)
            {
                return BadRequest();
            }

            db.Entry(trail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrailExists(id))
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

        // POST: api/TrailData/AddTrail
        [ResponseType(typeof(Trail))]
        [HttpPost]
        public IHttpActionResult AddTrail(Trail trail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trails.Add(trail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = trail.TrailId }, trail);
        }

        // DELETE: api/TrailData/DeleteTrail/5
        [ResponseType(typeof(Trail))]
        [HttpPost]
        public IHttpActionResult DeleteTrail(int id)
        {
            Trail trail = db.Trails.Find(id);
            if (trail == null)
            {
                return NotFound();
            }

            db.Trails.Remove(trail);
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

        private bool TrailExists(int id)
        {
            return db.Trails.Count(e => e.TrailId == id) > 0;
        }
    }
}