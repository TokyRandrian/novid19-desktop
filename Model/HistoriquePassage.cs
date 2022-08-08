using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Novid.Model
{
    class HistoriquePassage
    {
       public String _id { get; set; }
       public String Lieu_id { get; set; }
       public String Personne_id { get; set; }
       public DateTime Date_passage { get; set; }


        private static string baseURL = "https://calm-mesa-49918.herokuapp.com/api/";

        public async Task<List<HistoriquePassage>> GetHistoriquePersonne(string parametre)
        {

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(base­URL + "historiquePersonne/" +parametre))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var fileJsonString = await response.Content.ReadAsStringAsync();
                        var json = JsonConvert.DeserializeObject<HistoriquePassage[]>(fileJsonString).ToList();
                        var dateNow = DateTime.Now;
                        var list48 = json.Where(x => x.Date_passage >= dateNow.AddDays(-2) && x.Date_passage < dateNow).ToList();
                        return list48;
                    }
                }
            }

            return null;
        }

        public async Task<List<HistoriquePassage>> GetHistoriqueLieu(string idLieu)
        {

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(base­URL + "historiqueLieux/"+idLieu))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var fileJsonString = await response.Content.ReadAsStringAsync();
                        var json = JsonConvert.DeserializeObject<HistoriquePassage[]>(fileJsonString).ToList();
                        var dateNow = DateTime.Now;
                        var list48 = json.Where(x => x.Date_passage >= dateNow.AddDays(-2) && x.Date_passage < dateNow).ToList();
                        return list48;
                    }
                }
            }

            return null;
        }
    }
}
