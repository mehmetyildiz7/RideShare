using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdessoRideShare.Database.Models
{
    public class City
    {
        public int CityId { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }
    }
}
