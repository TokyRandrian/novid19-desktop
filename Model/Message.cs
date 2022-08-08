using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Novid.Model
{
    class Message
    {
        public String  IDMessage { get; set; }
        public String MessageContenu { get; set; }
        public String PersonneID { get; set; }
        public String CentreID { get; set; }
        public DateTime DateEnvoi { get; set; }

        private static string baseURL = "https://calm-mesa-49918.herokuapp.com/api/";

        public async Task<String> Insert(Message toInsert)
        {
            HttpClient client = new HttpClient();

            var values = new Dictionary<string, string>
                {
                    { "message", toInsert.MessageContenu },
                    { "personne_id", toInsert.PersonneID },
                    { "centre_id", toInsert.CentreID },
                    { "date_envoi", DateTime.Now.ToString() },
                };

            var content = new FormUrlEncodedContent(values);
            client.BaseAddress = new Uri(baseURL);
            client.DefaultReques­tHeaders.Accept.Clea­r();
            var response = await client.PostAsy­nc(baseURL + "message", content);

            using (HttpContent resp = response.Content)
            {
                string data = await resp.ReadAsStringAsy­nc();
                if (data != null)
                {
                    return data;
                }
            }
            return String.Empty;
        }

    }
}
