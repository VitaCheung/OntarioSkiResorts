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
using Price = Lesson1.Models.Price;

namespace Lesson1.Controllers
{
    public class PriceDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //public int PriceId { get; private set; }
        //public int ResortId { get; private set; }

        //public decimal DAY1Hour { get; private set; }
        //public decimal DAY2Hours { get; private set; }
        //public decimal DAY3Hours { get; private set; }
        //public decimal DAY4Hours { get; private set; }

        //public decimal NIGHT1Hour { get; private set; }
        //public decimal NIGHT2Hours { get; private set; }
        //public decimal NIGHT3Hours { get; private set; }
        //public decimal NIGHT4Hours { get; private set; }

        // GET: api/PriceData/ListPrice
        [HttpGet]
        [ResponseType(typeof(PriceDto))]
        public IHttpActionResult ListPrices()
        {
            List<Price> Prices = db.Prices.ToList();
            List<PriceDto> PriceDtos = new List<PriceDto>();

            Prices.ForEach(a => PriceDtos.Add(new PriceDto()
            {
                PriceId = a.PriceId,
                ResortId = a.ResortId,
                ResortName = a.Resort.ResortName,
                DAY1Hour = a.DAY1Hour,
                DAY2Hours = a.DAY2Hours,
                DAY3Hours = a.DAY3Hours,
                DAY4Hours = a.DAY4Hours,
                NIGHT1Hour = a.NIGHT1Hour,
                NIGHT2Hours = a.NIGHT2Hours,
                NIGHT3Hours = a.NIGHT3Hours,
                NIGHT4Hours = a.NIGHT4Hours
            }));
            return Ok(PriceDtos);
        }

        [HttpGet]
        [ResponseType(typeof(PriceDto))]
        public IHttpActionResult ListPricesForResort(int id)
        {
            List<Price> Prices = db.Prices.Where(p => p.ResortId == id).ToList();
            List<PriceDto> PriceDtos = new List<PriceDto>();

            Prices.ForEach(p => PriceDtos.Add(new PriceDto()
            {
                PriceId = p.PriceId,
                ResortId = p.ResortId,
                DAY1Hour = p.DAY1Hour,
                DAY2Hours = p.DAY2Hours,
                DAY3Hours = p.DAY3Hours,
                DAY4Hours = p.DAY4Hours,
                NIGHT1Hour = p.NIGHT1Hour,
                NIGHT2Hours = p.NIGHT2Hours,
                NIGHT3Hours = p.NIGHT3Hours,
                NIGHT4Hours = p.NIGHT4Hours

            }));
            return Ok(PriceDtos);
        }

        // GET: api/PriceData/FindPrice/5 
        [ResponseType(typeof(PriceDto))]
        [HttpGet]
        public IHttpActionResult FindPrice(int id)
        {
            Price Price = db.Prices.Find(id);
            //Resort Resort = (Resort)db.Resorts.Where(a => a.ResortId == id);
            PriceDto PriceDto = new PriceDto()
            {
                PriceId = Price.PriceId,
                ResortId = Price.ResortId,
                ResortName = Price.Resort.ResortName,
                DAY1Hour = Price.DAY1Hour,
                DAY2Hours = Price.DAY2Hours,
                DAY3Hours = Price.DAY3Hours,
                DAY4Hours = Price.DAY4Hours,
                NIGHT1Hour = Price.NIGHT1Hour,
                NIGHT2Hours = Price.NIGHT2Hours,
                NIGHT3Hours = Price.NIGHT3Hours,
                NIGHT4Hours = Price.NIGHT4Hours,

            };
            if (Price == null)
            {
                return NotFound();
            }

            return Ok(PriceDto);
        }
        
        // POST: api/PriceData/UpdatePrice/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePrice(int id, Price price)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != price.PriceId)
            {
                return BadRequest();
            }

            db.Entry(price).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceExists(id))
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

        // POST: api/PriceData/AddPrice
        [ResponseType(typeof(Price))]
        [HttpPost]
        public IHttpActionResult AddPrice(Price price)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Prices.Add(price);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = price.PriceId }, price);
        }

       
        // POST: api/PriceData/DeletePrice/5
        [ResponseType(typeof(Price))]
        [HttpPost]
        public IHttpActionResult DeletePrice(int id)
        {
            Price price = db.Prices.Find(id);
            if (price == null)
            {
                return NotFound();
            }

            db.Prices.Remove(price);
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

        private bool PriceExists(int id)
        {
            return db.Prices.Count(e => e.PriceId == id) > 0;
        }
    }
}