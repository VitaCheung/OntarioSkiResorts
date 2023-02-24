using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using Lesson1.Models;
using Lesson1.Models.ViewModels;
using System.Web.Script.Serialization;
using Resort = Lesson1.Models.Resort;

namespace Lesson1.Controllers
{
    public class ResortController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ResortController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44340/api/");
        }
        // GET: Resort/List
        public ActionResult List()
        {
            //communicate with resort data api to retrieve a list of resorts
            //curl https://localhost:44340/api/resortdata/listresorts

            
            string url = "resortdata/listresorts";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<ResortDto> resorts = response.Content.ReadAsAsync<IEnumerable<ResortDto>>().Result;
            //Debug.WriteLine("Number of resorts received: ");
            //Debug.WriteLine(resorts.Count());
            
            return View(resorts);
        }

        // GET: Resort/Details/5
        public ActionResult Details(int id)
        {
            DetailsResort ViewModel = new DetailsResort();
            //communicate with resort data api to retrieve a resort
            //curl https://localhost:44340/api/resortdata/findresort/{id}

            
            string url = "resortdata/findresort/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            ResortDto SelectedResort = response.Content.ReadAsAsync<ResortDto>().Result;
            Debug.WriteLine("resort received: ");
            Debug.WriteLine(SelectedResort.ResortName);

            ViewModel.SelectedResort = SelectedResort;

            //Show trails related to this resort
            url = "traildata/listtrailsforresort/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<TrailDto> RelatedTrail = response.Content.ReadAsAsync<IEnumerable<TrailDto>>().Result;

            ViewModel.RelatedTrail = RelatedTrail;


            //Show prices related to this resort
            url = "pricedata/listpricesforresort/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<PriceDto> RelatedPrice = response.Content.ReadAsAsync<IEnumerable<PriceDto>>().Result;

            ViewModel.RelatedPrice = RelatedPrice;


            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }


        // GET: Resort/New
        public ActionResult New()
        {
            //GET api/resortdata/listresorts

            string url = "resortdata/listresort";
            HttpResponseMessage response = client.GetAsync(url).Result;

            return View();
        }

        // POST: Resort/Create
        [HttpPost]
        public ActionResult Create(Resort resort)
        {
            Debug.WriteLine("the json payload is: ");
            //Debug.WriteLine(resort.ResortName);
            //Add a new resort into the system using the API
            //curl -H "Content-Type:application/json" -d @resort.json https://localhost:44340/api/resortdata/addresort
            string url = "resortdata/addresort";

            
            string jsonpayload = jss.Serialize(resort);

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

        // GET: Resort/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateResort ViewModel = new UpdateResort();
            string url = "resortdata/findresort/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ResortDto SelectedResort = response.Content.ReadAsAsync<ResortDto>().Result;
            ViewModel.SelectedResort = SelectedResort;
            
            return View(ViewModel);
            
        }

        // POST: Resort/Update/5
        [HttpPost]
        public ActionResult Update(int id, Resort Resort)
        {
            string url = "resortdata/updateresort/" + id; 
            string jsonpayload = jss.Serialize(Resort);    

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

        // GET: Resort/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "resortdata/findresort/" + id;           
            HttpResponseMessage response = client.GetAsync(url).Result;
            ResortDto SelectedResort = response.Content.ReadAsAsync<ResortDto>().Result;

            return View(SelectedResort);
        }

        // POST: Resort/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "resortdata/deleteresort/" + id;
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
