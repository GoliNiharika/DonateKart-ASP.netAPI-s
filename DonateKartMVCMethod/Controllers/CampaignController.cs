using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using DonateKartMVCMethod.Models;

namespace DonateKartMVCMethod.Controllers
{
    public class CampaignController : Controller
    {
        //IEnumerable<CampaignModel> campaigns = null;
        // GET: Campaign
        public ActionResult Index()
        {
            IEnumerable<CampaignModel> campaigns = null;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://testapi.donatekart.com/api/");
                var responseTask = client.GetAsync("campaign");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<IList<CampaignModel>>();
                    read.Wait();
                    campaigns = read.Result;
                }
                else
                {
                    campaigns = Enumerable.Empty<CampaignModel>();
                    ModelState.AddModelError(string.Empty, "Error occured");
                }
            }
            campaigns = campaigns.OrderBy(x => x.totalAmount);
            return View(campaigns);
        }

    //    public ActionResult GetSortedCampaigns()
    //    {
    //        campaigns = (IEnumerable<CampaignModel>)Index();
    //        IEnumerable<CampaignModel> SortedCampaigns = campaigns.OrderBy(x => x.totalAmount);
    //        return View(SortedCampaigns);
    //    }
    }
}