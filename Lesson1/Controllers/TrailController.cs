using Lesson1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Lesson1.Models.ViewModels;

namespace Lesson1.Controllers
{
    public class TrailController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static TrailController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44340/api/");
        }
        // GET: Trail/List     
        public ActionResult List()
        {
            //communicate with trail data api to retrieve a list of trails
            //curl https://localhost:44340/api/traildata/listtrails


            string url = "traildata/listtrails";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<TrailDto> trails = response.Content.ReadAsAsync<IEnumerable<TrailDto>>().Result;


            return View(trails);
        }

        // GET: Trail/Details/5
        public ActionResult Details(int id)
        {
            DetailsTrail ViewModel = new DetailsTrail();
            //communicate with trail data api to retrieve trails
            //curl https://localhost:44340/api/traildata/findtrail/{id}

            string url = "traildata/findTrail/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            TrailDto SelectedTrail = response.Content.ReadAsAsync<TrailDto>().Result;
            ViewModel.SelectedTrail = SelectedTrail;

            //Show the related resort
            //url = "resortdata/findresort/" + id;
            //response = client.GetAsync(url).Result;
            //ResortDto RelatedResort = response.Content.ReadAsAsync <ResortDto>().Result;
            
            //ViewModel.RelatedResort = RelatedResort;


            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }


        // GET: Trail/New
        public ActionResult New()
        {
            //string url = "traildata/listtrail";
            string url = "resortdata/listresorts";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<ResortDto> ResortOptions = response.Content.ReadAsAsync<IEnumerable<ResortDto>>().Result; 
            
            return View(ResortOptions);
        }

        // POST: Trail/Create
        [HttpPost]
        public ActionResult Create(Trail trail)
        {
            Debug.WriteLine("the json payload is: ");         
            //Add new trail details into the system using the API
            
            string url = "traildata/addtrail";


            string jsonpayload = jss.Serialize(trail);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }



        }

        // GET: Trail/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateTrail ViewModel = new UpdateTrail();

            string url = "traildata/findtrail/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TrailDto SelectedTrail = response.Content.ReadAsAsync<TrailDto>().Result;
            ViewModel.SelectedTrail = SelectedTrail;


            //all resorts to choose from when updating
            url = "resortdata/listresorts";
            response = client.GetAsync(url).Result;
            IEnumerable<ResortDto> ResortOptions = response.Content.ReadAsAsync<IEnumerable<ResortDto>>().Result;

            ViewModel.ResortOptions = ResortOptions;


            return View(ViewModel);

        }

        // POST: Trail/Update/5
        [HttpPost]      
        public ActionResult Update(int id, Trail trail)
        {
            string url = "traildata/updatetrail/" + id;
            string jsonpayload = jss.Serialize(trail);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        // GET: Trail/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "traildata/findtrail/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TrailDto SelectedTrail = response.Content.ReadAsAsync<TrailDto>().Result;

            return View(SelectedTrail);
        }

        // POST: Trail/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "traildata/deletetrail/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
