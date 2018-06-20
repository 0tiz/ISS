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

                var issData = JsonConvert.DeserializeObject<IssData>(issTracking);
                var peoples = JsonConvert.DeserializeObject<SpaceCowboys>(peopleInSpace);
                var onlineCheck = GetOnlineCheck(issData.DidSucceed);
                string parseLon = GetCommaToDot(issData.IssPosition.Longitude);
                string parseLati = GetCommaToDot(issData.IssPosition.Latitude);
                //example {"iss_position": {"latitude": "1.3102", "longitude": "4.2245"}, "timestamp": 1528496959, "message": "success"}


                string upload = "http://api.open-notify.org/iss-pass.json?lat=52.520008&lon=13.404954";
                string openMaps = "www.google.com/maps/place/"+parseLati+","+parseLon;
                var getAltitude = webClient.DownloadString(upload);
                var altitude = JsonConvert.DeserializeObject<IssAltitude>(getAltitude);
                

                Console.WriteLine(onlineCheck);
                Console.WriteLine(issData.IssPosition.Latitude);
                Console.WriteLine(issData.IssPosition.Longitude);
                Console.WriteLine(getAltitude);
                Console.WriteLine("Currently are "+peoples.number+" people on the ISS ");
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

        abstract class ApiResponse
        {
            public Status Message;
            public bool DidSucceed => Message == Status.Success;
        }

        class IssData : ApiResponse
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
        class SpaceCowboys : ApiResponse
        {
            public int number;
        }

        class IssAltitude
        {
            public decimal altitude;
        }
        enum Status
        {
            None,
            Success,
        }

        public static string GetOnlineCheck(bool isOnline)
        {
            return isOnline ? "ISS online" : "ISS offline";

        }


        public static string GetCommaToDot(decimal toParse)
        {
            string str = toParse.ToString();

            return str.Replace(",", ".");
        }

        
    }


}
