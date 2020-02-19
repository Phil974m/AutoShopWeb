using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShop_Shared.Models
{
     public class AppSettings
    {
        public string FilePath { get; set; }
        public string BadgesFileName { get; set; }
        public string UsersFileName { get; set; }
        public string CosmosDBUrl { get; set; }
        public string CosmosDBKey { get; set; }
        public string CosmosDatabase { get; set; }
        public string CosmosContainer { get; set; }

        public string QuerySelect { get; set; }
        public string QueryWhere { get; set; }
    }
}
