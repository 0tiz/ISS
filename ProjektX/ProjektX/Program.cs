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
                var peopleInSpace = webClient.DownloadString("http://api.open-notify.org/astros.json");
                //example {"iss_position": {"latitude": "1.3102", "longitude": "4.2245"}, "timestamp": 1528496959, "message": "success"}

                var issData = JsonConvert.DeserializeObject<IssData>(issTracking);
                var onCheck = JsonConvert.DeserializeObject<OnCheck>(issTracking);
                var peoples = JsonConvert.DeserializeObject<SpaceCowboys>(peopleInSpace);
                var onlineCheck = GetOnlineCheck(onCheck.Message);

                string openMaps = string.Format(System.Globalization.CultureInfo.InvariantCulture.NumberFormat, "www.google.com/maps/place/{0},{1}", issData.IssPosition.Latitude, issData.IssPosition.Longitude);
                

                Console.WriteLine(onlineCheck);
                Console.WriteLine(issData.IssPosition.Latitude);
                Console.WriteLine(issData.IssPosition.Longitude);
                Console.WriteLine("Currently are "+peoples.number+" peoples on the ISS ");
                Console.WriteLine("Do you want to see the postition from the ISS on Google Maps? Type YES or NO is the console");
             
                string userInput = Console.ReadLine();
                if (userInput == "YES")
                {
                    System.Diagnostics.Process.Start(openMaps);
                }
                else
                {
                    Console.WriteLine("OK");
                }



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
        class SpaceCowboys
        {
            public int number;
        }

        public static string GetOnlineCheck(string onlineCheck)
        {
            string check = ("");

            if (onlineCheck == "success")
            {
                check = ("ISS online");
            }

            else
            {
                check = ("ISS offline");
            }

            return check;

        }





    }


}
