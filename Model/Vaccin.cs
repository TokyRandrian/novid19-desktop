using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Novid.Model
{
    public class Vaccin
    {
        public String _id { get; set; }
        public String Nom_vaccin { get; set; }
        public String Centre_id { get; set; }
        public DateTime Date_vaccin { get; set; }
        public String Carte_id { get; set; }

        private static string baseURL = "https://calm-mesa-49918.herokuapp.com/api/";

   


        public async Task<String> Insert(Vaccin toInsert)
        {
            HttpClient client = new HttpClient();

            var values = new Dictionary<string, string>
                {
                    { "centre_id", toInsert.Centre_id },
                    { "nom_vaccin", toInsert.Nom_vaccin },
                    { "Date_vaccin", toInsert.Date_vaccin.ToString() },
                    { "Carte_id", toInsert.Carte_id }
                };


 

            var content = new FormUrlEncodedContent(values);
            client.BaseAddress = new Uri(baseURL);
            client.DefaultReques­tHeaders.Accept.Clea­r();
            var response = await client.PostAsy­nc(baseURL + "vaccin", content);

            using (HttpContent resp = response.Content)
            {
                string data = await resp.ReadAsStringAsy­nc();
                if (data != null)
                {
                    var result = data.Split(':')[1].Split(' ')[0].Split('\"')[1];

                    return result;
                }
            }
            return String.Empty;
        }

        public async Task<List<Vaccin>> GetVaccinPersonne(string idCarte)
        {

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(base­URL + "vaccinCarte/"+idCarte))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var fileJsonString = await response.Content.ReadAsStringAsync();
                        var json = JsonConvert.DeserializeObject<Vaccin[]>(fileJsonString).ToList();
   
                        return json;
                    }
                }
            }

            return null;
        }

    }
}
