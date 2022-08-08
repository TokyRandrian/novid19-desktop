using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Novid.Model
{
    class Centre
    {
        public String _id { get; set; }
        public String Nom_centre { get; set; }
        public String Adresse_centre { get; set; }
        public String Coordonnees_centre { get; set; }



        private static string baseURL = "https://calm-mesa-49918.herokuapp.com/api/";


        public async Task<List<Centre>> GetAllCentre()
        {

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(base­URL + "centre"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var fileJsonString = await response.Content.ReadAsStringAsync();
                        var data = (JObject)JsonConvert.DeserializeObject(fileJsonString);
                        var docs = data["docs"].ToString();
                        var json = JsonConvert.DeserializeObject<Centre[]>(docs).ToList();
                        return json;
                    }
                }
            }

            return null;
        }

    }
}
