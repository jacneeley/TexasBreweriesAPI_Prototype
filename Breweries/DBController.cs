using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace Breweries
{
    public class DBController
    {
        
        public static void getBrewCity(GridView gv, string pCity)
        {
            DataClassesBreweryDataContext myDc = new DataClassesBreweryDataContext();
            //allow a user to query by city
            var selectBreweries = from aBrew in myDc.BrewTexas
                                  where aBrew.city == pCity
                                  select aBrew;

            gv.DataSource = selectBreweries;
            gv.DataBind();
        }
        public static void brewType(GridView gv, string pType)
        {
            DataClassesBreweryDataContext myDc = new DataClassesBreweryDataContext();
            var selectBreweries = from aBrew in myDc.BrewTexas
                                  where aBrew.brewery_type == pType
                                  select aBrew;
            gv.DataSource = selectBreweries;
            gv.DataBind();
        }
        public static void reset(GridView gv)
        {//allow a user to clear their query, start from scratch.
            //clear the data grid view
            DataClassesBreweryDataContext myDc = new DataClassesBreweryDataContext();
            gv.DataSource = null;

            //simply select all brews again
            var selectBreweries = from aBrew in myDc.BrewTexas

                                  select aBrew;

            gv.DataSource = selectBreweries;
            gv.DataBind();
        }
    }
}