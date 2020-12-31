using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MedicalForm
{
    class MedicalForm
    {
        static void Main(string[] args)
        {
            RunAsync();
        }
        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = null;
                try
                {
                    response = client.PostAsync(
                            "https://forms.office.com/Pages/ResponsePage.aspx?id=F5GcB_FAnk2NTI9jxrCFW9ON9HWRBtdPoQx_4tpB4iJUQ1ZPUEpFOEpLUE5ZUlNDSzlFRFNDU1pHVi4u",
                            new StringContent(GetStringWithMyDetails(), Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Perfect");
                    }
                    else
                    {
                        Console.WriteLine("Process didn't succeed");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Process didn't succeed");
                }
            }
        }

        static public string GetStringWithMyDetails()
        {
            string json;
            using (StreamReader file = new StreamReader("myDetails.json"))
            {
                json = file.ReadToEnd();
                json = json.Replace("myPassport", "311186316");
                json = json.Replace("myName", "Shir Halevi");
                json = json.Replace("myTemp", string.Format("{0:0.0}", GetRandomNumber(36.0, 36.5)));
                json = json.Replace("myDate1", DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.888'Z'"));
                json = json.Replace("myDate2", DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.888'Z'"));
            }
            return json;
        }

        static public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
