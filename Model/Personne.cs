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
    class Personne
    {
        public String _id { get; set; }
        public String Nom { get; set; }
        public String Prenom { get; set; }
        public String Mail { get; set; }
        public DateTime Date_naissance { get; set; }
        public String Adresse { get; set; }
        public int Sexe { get; set; }
        public String Cin { get; set; }

        private static string baseURL = "https://calm-mesa-49918.herokuapp.com/api/";
        public async Task<List<Personne>> GetAllPersonne()
        {

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(base­URL + "personne"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var fileJsonString = await response.Content.ReadAsStringAsync();

                        var data = (JObject)JsonConvert.DeserializeObject(fileJsonString);
                        var docs = data["docs"].ToString();

                        var json = JsonConvert.DeserializeObject<Personne[]>(docs).ToList();
                        return json;
                    }
                }
            }

            return null;
        }


    }

}
