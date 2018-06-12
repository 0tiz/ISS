using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace ISS
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var webClient = new System.Net.WebClient())
            {
                var issTracking = webClient.DownloadString("http://api.open-notify.org/iss-now.json");



                //  Console.WriteLine(issTracking);

               
                var issData = JsonConvert.DeserializeObject<IssData>(issTracking);
                var onCheck = JsonConvert.DeserializeObject<OnCheck>(issTracking);
                string onlineCheck = onCheck.Message;

                if (onlineCheck == "success")
                {
                    Console.WriteLine("ISS online");
                }

                else
                {
                    Console.WriteLine("ISS offline");
                }

               string openMaps = string.Format(System.Globalization.CultureInfo.InvariantCulture.NumberFormat, "www.google.com/maps/place/{0},{1}", issData.IssPosition.Latitude, issData.IssPosition.Longitude);

                System.Diagnostics.Process.Start(openMaps);

            
               

           

                
                
                Console.WriteLine(issData.IssPosition.Latitude);
                Console.WriteLine(issData.IssPosition.Longitude);
                Console.WriteLine(onlineCheck);
   









                // result is {"iss_position": {"latitude": "45.9627", "longitude": "-88.9175"}, "timestamp": 1528489680, "message": "success"}


            }



        }

        class OnCheck
        {
            public string Message;
        }

        class IssData
        {
            [JsonProperty("iss_position")]
            public Coordinates IssPosition;
            public int Timestamp;
            
        }

        class Coordinates
        {
            public decimal Latitude;
            public decimal Longitude;
        }

       


    }
}
