using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopWebProject.Models
{
    public class EditFormModel
    {
        public string Id { get; set; }
        public string Tittle { get; set; }

        public int Price { get; set; }

        public int Year { get; set; }

        public string ImageUrl { get; set; }

        public string Company { get; set; }

        public string Description { get; set; }
        public string PlatformId { get; set; }

        public string CategoryId { get; set; }
    }
}
