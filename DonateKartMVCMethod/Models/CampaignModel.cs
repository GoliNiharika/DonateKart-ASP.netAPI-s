using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonateKartMVCMethod.Models
{
    public class CampaignModel
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