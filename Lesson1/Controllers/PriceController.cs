using Lesson1.Models;
using Lesson1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Lesson1.Controllers
{
    public class PriceController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static PriceController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44340/api/");
        }
        // GET: Price/List
        public ActionResult List()
        {
            //communicate with price data api to retrieve a list of prices
            //curl https://localhost:44340/api/pricedata/listprices


            string url = "pricedata/listprices";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<PriceDto> prices = response.Content.ReadAsAsync<IEnumerable<PriceDto>>().Result;
            

            return View(prices);
        }

        // GET: Price/Details/5
        public ActionResult Details(int id)
        {
            DetailsPrice ViewModel = new DetailsPrice();
            //communicate with price data api to retrieve price
            //curl https://localhost:44340/api/pricedata/findprice/{id}


            string url = "pricedata/findPrice/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PriceDto SelectedPrice = response.Content.ReadAsAsync<PriceDto>().Result;
            ViewModel.SelectedPrice = SelectedPrice;

            //Show the related resort
            //url = "resortdata/findresort/" + id;
            //response = client.GetAsync(url).Result;
            //ResortDto RelatedResort = response.Content.ReadAsAsync<ResortDto>().Result;

            //ViewModel.RelatedResort = RelatedResort;


            return View(ViewModel);


        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Price/New      
        public ActionResult New()
        {
            //GET api/pricedata/listprices
            //string url = "pricedata/listprice";

            //GET all resorts to choose from when creating
            string url = "resortdata/listresorts";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<ResortDto> ResortOptions = response.Content.ReadAsAsync<IEnumerable<ResortDto>>().Result;
            
            return View(ResortOptions);
        }

        // POST: Price/Create
        [HttpPost]
        public ActionResult Create(Price price)
        {
            Debug.WriteLine("the json payload is: ");
            //Debug.WriteLine(resort.ResortName);
            //Add a new price into the system using the API
            //curl -H "Content-Type:application/json" -d @resort.json https://localhost:44340/api/pricedata/addprice
            string url = "pricedata/addprice";
            string jsonpayload = jss.Serialize(price);
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

        // GET: Price/Edit/5
        public ActionResult Edit(int id)
        {
            UpdatePrice ViewModel = new UpdatePrice();

            string url = "pricedata/findprice/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PriceDto SelectedPrice = response.Content.ReadAsAsync<PriceDto>().Result;
            ViewModel.SelectedPrice = SelectedPrice;


            //all resorts to choose from when updating
            url = "resortdata/listresorts";
            response = client.GetAsync(url).Result;
            IEnumerable<ResortDto> ResortOptions = response.Content.ReadAsAsync<IEnumerable<ResortDto>>().Result;

            ViewModel.ResortOptions = ResortOptions;


            return View(ViewModel);
            //return View(SelectedPrice);

        }

        // POST: Price/Update/5

        [HttpPost]
        public ActionResult Update(int id, Price price)
        {
            string url = "pricedata/updateprice/" + id;
            string jsonpayload = jss.Serialize(price);

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


        // GET: Price/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "pricedata/findprice/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PriceDto SelectedPrice = response.Content.ReadAsAsync<PriceDto>().Result;

            return View(SelectedPrice);
        }

        // POST: Price/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "pricedata/deleteprice/" + id;
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
