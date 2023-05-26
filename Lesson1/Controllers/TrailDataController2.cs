using System;
using System.IO;
using System.Web;
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
using Trail = Lesson1.Models.Trail;



namespace Lesson1.Controllers
{
    public class TrailDataController : ApiController
    {

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
                TubingPark = t.TubingPark,
                TrailHasPic = t.TrailHasPic,
                PicExtension = t.PicExtension

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
                TubingPark = t.TubingPark,
                TrailHasPic = t.TrailHasPic,
                PicExtension = t.PicExtension

            }));
            return Ok(TrailDtos);
        }

        // GET: api/TrailData/FindTrail/5
        [ResponseType(typeof(TrailDto))]
        [HttpGet]
        public IHttpActionResult FindTrail(int id)
        {
            Trail Trail = db.Trails.Find(id);
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
                TrailHasPic = Trail.TrailHasPic,
                PicExtension = Trail.PicExtension


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
            // Picture update is handled by another method
            db.Entry(trail).Property(t => t.TrailHasPic).IsModified = false;
            db.Entry(trail).Property(t => t.PicExtension).IsModified = false;

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

        // POST: api/TrailData/UploadTrailPic/3
        [HttpPost]
        public IHttpActionResult UploadTrailPic(int id)
        {

            bool haspic = false;
            string picextension;
            if (Request.Content.IsMimeMultipartContent())
            {
                Debug.WriteLine("Received multipart form data.");

                int numfiles = HttpContext.Current.Request.Files.Count;
                Debug.WriteLine("Files Received: " + numfiles);

                //Check if a file is posted
                if (numfiles == 1 && HttpContext.Current.Request.Files[0] != null)
                {
                    var trailPic = HttpContext.Current.Request.Files[0];
                    //Check if the file is empty
                    if (trailPic.ContentLength > 0)
                    {
                        //establish valid file types (can be changed to other file extensions if desired!)
                        var valtypes = new[] { "jpeg", "jpg", "png", "gif" };
                        var extension = Path.GetExtension(trailPic.FileName).Substring(1);
                        //Check the extension of the file
                        if (valtypes.Contains(extension))
                        {
                            try
                            {
                                //file name is the id of the image
                                string fn = id + "." + extension;

                                //get a direct file path to ~/Content/animals/{id}.{extension}
                                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/img/trailPic/"), fn);

                                //save the file
                                trailPic.SaveAs(path);

                                //if these are all successful then we can set these fields
                                haspic = true;
                                picextension = extension;

                                //Update the animal haspic and picextension fields in the database
                                Trail SelectedTrail = db.Trails.Find(id);
                                SelectedTrail.TrailHasPic = haspic;
                                SelectedTrail.PicExtension = extension;
                                db.Entry(SelectedTrail).State = EntityState.Modified;

                                db.SaveChanges();

                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("Image was not saved successfully.");
                                Debug.WriteLine("Exception:" + ex);
                                return BadRequest();
                            }
                        }
                    }

                }

                return Ok();
            }
            else
            {
                //not multipart form data
                return BadRequest();

            }

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

            if (trail.TrailHasPic && trail.PicExtension != "")
            {
                //also delete image from path
                string path = HttpContext.Current.Server.MapPath("~/Content/img/trailPic/" + id + "." + trail.PicExtension);
                if (System.IO.File.Exists(path))
                {
                    Debug.WriteLine("File exists... preparing to delete!");
                    System.IO.File.Delete(path);
                }
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