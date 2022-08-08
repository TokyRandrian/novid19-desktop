using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Novid.Model
{
    public class Test
    {
        public string IDTest { get; set; }
        public DateTime DateTest { get; set; }
        public string CentreID { get; set; }
        public string PersonneID { get; set; }
        public int EtatTest { get; set; }


        private static string baseURL = "https://calm-mesa-49918.herokuapp.com/api/";

      

        public async Task<String> Insert(Test toInsert)
        {
            HttpClient client = new HttpClient();

            var values = new Dictionary<string, string>
                {
                    { "date_test", toInsert.DateTest.ToString() },
                    { "centre_id", toInsert.CentreID },
                    { "personne_id", toInsert.PersonneID },
                    { "etat_test", toInsert.EtatTest.ToString() },
                };


            //var values = new Dictionary<string, string>
            //    {
            //        { "date_test", "2022-01-02" },
            //        { "centre_id", "62e95c971114a12a8c34c452" },
            //        { "personne_id", "62e974b27187d900161e7cf7" },
            //        { "etat_test", "1" },
            //    };


            var content = new FormUrlEncodedContent(values);
            client.BaseAddress = new Uri(baseURL);
            client.DefaultReques­tHeaders.Accept.Clea­r();
            var response = await client.PostAsy­nc(baseURL + "test", content);

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



    }
}
