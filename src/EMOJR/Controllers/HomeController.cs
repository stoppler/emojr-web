using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace EMOJR.Controllers
{
	/*
	* This in the controller class that makes the Emotion API calls.
	*/
    public class HomeController : Controller
    {
        // Project Oxford Emotion API key
        public const string _apiKey = "4af70ba6f3f14ea7891543204e3c82c4";

        // The base URL for the API
        public const string _apiUrl = "https://api.projectoxford.ai/emotion/v1.0/recognize";

		// Returns the default view
        public IActionResult Index()
        {
            return View();
        }

        // GET: Home/Upload
        public IActionResult Upload()
        {
            return View();
        }
        // POST: Home/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            using (var httpClient = new HttpClient())
            {
                //setup HttpClient
                httpClient.BaseAddress = new Uri(_apiUrl);
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));

                //setup data object
                HttpContent content         = new StreamContent(file.OpenReadStream());
                content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/octet-stream");

                //make request
                var response = await httpClient.PostAsync(_apiUrl, content);

                //read response and write to view
                var responseContent = await response.Content.ReadAsStringAsync();
                ViewData["Result"]  = responseContent;
            }

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
