using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using System.Globalization;

namespace RestApi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello");
            List<Class1> ListOfCampaigns = getCampaigns();
            
            //sorts the json by total amount field and prints title, totalAmount, backersCount, endDate
            var SortedCampaignsByTotalAmount = ListOfCampaigns.OrderBy(x => x.totalAmount);
            foreach (var campaign in SortedCampaignsByTotalAmount)
            {
                Console.WriteLine(campaign.title + " " + campaign.totalAmount + " " + campaign.backersCount + " " + campaign.endDate);
            }

            var todaysDate = DateTime.Today;

            //filtered records using FindAll to get Active Campaigns and campaigns created in last 30 days
            List<Class1> ListOfActiveCampaigns = new List<Class1>();
            
            ListOfActiveCampaigns = ListOfCampaigns.FindAll(x => DateTime.Compare(x.endDate,todaysDate) == 1 );
            foreach (var campaign in ListOfActiveCampaigns)
                Console.WriteLine(campaign.endDate +" "+campaign.title+" "+campaign.created);

            List<Class1> CampaignsCreatedwithin30days =  new List<Class1>();
            CampaignsCreatedwithin30days = ListOfActiveCampaigns.FindAll(x => (todaysDate - x.created).TotalDays <= 30);
            foreach (var campaign in CampaignsCreatedwithin30days)
                Console.WriteLine(campaign.code + " " + campaign.created + " " + campaign.title + " " + campaign.endDate);

          

            //for loop to find if a campaign is active by comparing endDate field with today
            // also to find list of campaigns for which procuredAmount is greater than or equal tp TotalAmount
            List<Class1> ListOfClosedCampaigns = new List<Class1>();
            List<Class1> ProcuredGreaterthanTotalAmount = new List<Class1>();
            foreach (var campaign in ListOfCampaigns)
            {
                int result = DateTime.Compare(campaign.endDate, todaysDate);
                if (result < 1)
                    ListOfClosedCampaigns.Add(campaign);
                if (campaign.procuredAmount >= campaign.totalAmount)
                    ProcuredGreaterthanTotalAmount.Add(campaign);
            }
            Console.WriteLine(ListOfClosedCampaigns);
            Console.WriteLine(ProcuredGreaterthanTotalAmount);


        }

        public static List<Class1> getCampaigns()
        {
            var data = new RestClient("https://testapi.donatekart.com/api/");
            var request = new RestRequest("campaign");
            var response = data.Execute(request);
            List<Class1> result = null;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string JSONResponseStringFormat = response.Content;
                result = JsonConvert.DeserializeObject<List<Class1>>(JSONResponseStringFormat);
            }
            return result;
        }
    }
    public class Rootobject
    {
        public string status { get; set; }
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public string code { get; set; }
        public string title { get; set; }
        public bool featured { get; set; }
        public int priority { get; set; }
        public string shortDesc { get; set; }
        public string imageSrc { get; set; }
        public DateTime created { get; set; }
        public DateTime endDate { get; set; }
        public float totalAmount { get; set; }
        public float procuredAmount { get; set; }
        public float totalProcured { get; set; }
        public float backersCount { get; set; }
        public int categoryId { get; set; }
        public string ngoCode { get; set; }
        public string ngoName { get; set; }
        public int daysLeft { get; set; }
        public float percentage { get; set; }
    }
}
