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
using System.Timers;

namespace Breweries
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        DataClassesBreweryDataContext myDc = new DataClassesBreweryDataContext();
       
        
        protected void Page_Load(object sender, EventArgs e)
        {
          
            ScheduleGettingData();
        }

        public void ScheduleGettingData()
        {
            //time variables
            DateTime dt = DateTime.Now;
            DateTime recordedTime;
            int timeDiff = DateTime.Compare(DateTime.Now,dt);
            while (timeDiff > 0)
            {//idea is to only collect when d1 > d2
                GetBreweryData();
                break;
            }
            recordedTime = dt;
            var selectBreweries = from aBrew in myDc.BrewTexas

                                  select aBrew;

            GridView1.DataSource = selectBreweries;
            GridView1.DataBind();


        }
        public void GetBreweryData()
        {
         const string URL = "https://api.openbrewerydb.org/breweries";
         
            //Get JSON data from API
         string urlParameters = "?by_state=texas";
            //Create instance of new HTTP CLIENT 
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json")); //tells the server to send data in JSON format

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            //GetAsync method sends the request, when the method returns, it returns and HTTPResponseMessage, which contains HTTP Response
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<Brew>>().Result;  //deserialize JSON
                //content will get and set response message
                //ReadAsAsync will deserialize JSON data to a Brew instance 
                //IEnumerable is important as it is an interface that will define one method. We will be able to dataObject in a foreach loop
                //Gives us access to a collection
                //.Result will get result of TASK<TResult>, in this case IEnumerable BREW. Result sets value removing the need for "await"
          


                //Delete existing data from table to avoid duplicates 
                //Everytime we run getbrewdata method, old data will be removed. Insures old data and new data will be added.
                myDc.ExecuteCommand("Delete From BrewTexas");//insures that new data will always come in, if there is infact new data, negates the need for any conditional statements

                foreach (var d in dataObjects)
                {
                    //  Console.WriteLine("{0}", d.id);
                    BrewTexa aBrew = new BrewTexa(); 
                    aBrew.id = d.id;
                    aBrew.name = d.name;
                    aBrew.brewery_type = d.brewery_type;
                    aBrew.street = d.street;
                    aBrew.address_2 = d.address_2;
                    aBrew.address_3 = d.address_3;
                    aBrew.city = d.city;
                    aBrew.state = d.state;
                    aBrew.county_province = d.county_province;
                    aBrew.postal_code = d.postal_code;
                    aBrew.country = d.country;
                    aBrew.longitude = d.longitude;
                    aBrew.latitude = d.latitude;
                    aBrew.phone = d.phone;
                    aBrew.website_url = d.website_url;
                    aBrew.updated_at = d.updated_at;
                    aBrew.created_at = d.created_at;
                    myDc.BrewTexas.InsertOnSubmit(aBrew);
                    myDc.SubmitChanges();
                }
            }
            else
            {
                // Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + response.ReasonPhrase + "');", true);
            }

            // Make any other calls using HttpClient here.

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text.Length > 0)
            {
                DBController.getBrewCity(GridView1, TextBox1.Text);

            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            DBController.reset(GridView1);
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            //implement drop box
            DBController.brewType(GridView1,DropDownList1.Text);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            //nothing
        }

    }
}