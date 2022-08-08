using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Novid.Model
{
    public class Service
    {
        public string baseURL = "https://calm-mesa-49918.herokuapp.com/api/";



        public async Task<string> GetAsync( string parametre)
        {
          
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(base­URL + "lieu"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var fileJsonString = await response.Content.ReadAsStringAsync();

                           // var json = JsonConvert.DeserializeObject<ServerFileInformation[]>(fileJsonString).ToList();
                        }
                    }
                }

            return "";
        }
    }
}
