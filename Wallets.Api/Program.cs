using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Wallets.Api
{
    class Program
    {
        static void Main(string[] args)
        {
            //http://localhost:3000/api/Wallet
            string baseAddress = "http://localhost:3000/";

            Console.Title = $"API Listens on {baseAddress}";
            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                //HttpClient client = new HttpClient();

                //var response = client.GetAsync(baseAddress + "api/values").Result;

                //Console.WriteLine(response);
                //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine("Api Listening..");
                Console.ReadKey();
                
            }

        }
    }
}
