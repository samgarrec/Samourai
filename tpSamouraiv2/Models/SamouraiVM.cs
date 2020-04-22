using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tpSamouraiv2.Models
{
    public class SamouraiVM
    {
        public Samourai Samourai { get; set; }
        public List<SelectListItem> Arme { get; set; } = new List<SelectListItem>();

        public int? IdArme { get; set; }


        public SamouraiVM()
        {
            
                 
            Arme = Arme;
           
            Samourai = Samourai;
            IdArme = IdArme;

            
        }
    }
}