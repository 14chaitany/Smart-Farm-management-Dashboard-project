using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CropPrediction
{
    public class Locator
    {
        public string Location( string lattitude,string longitude)
        {
            string loc = string.Empty;
            if (longitude.Trim().StartsWith("77.2"))
                loc = "Chamarajanagar";
            if (longitude.Trim().StartsWith("76.6"))
                loc = "Mysore";
            if (longitude.Trim().StartsWith("76.8"))
                loc = "Mandya";
            if (longitude.Trim().StartsWith("77.5"))
                loc = "Bangalore";
            return loc;
        }
    }
}