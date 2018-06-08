using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ISS
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var webClient = new System.Net.WebClient())
            {
                var issTracking = webClient.DownloadString("http://api.open-notify.org/iss-now.json");

                JsonParser wert = new JsonParser();
                int foo = wert.timestamp;
                Console.WriteLine(foo);




                // result is {"iss_position": {"latitude": "45.9627", "longitude": "-88.9175"}, "timestamp": 1528489680, "message": "success"}


            }



        }


        public class JsonParser
        {
            public Iss_Position iss_position { get; set; }
            public int timestamp { get; set; }

            public string message { get; set; }
        }

        public class Iss_Position
        {
            public string latitude { get; set; }
            public string longitude { get; set; }
        }


    }
}
