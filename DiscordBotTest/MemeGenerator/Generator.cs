using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.IO;

namespace DiscordBotTest.MemeGenerator
{
    class Generator
    {
       public static async Task<resp> GetFromApiAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
               // var serializer = new DataContractJsonSerializer(typeof(List<memes>));
                var response = await client.GetAsync(url);
                var respstring = await response.Content.ReadAsStringAsync();
                var collection= JsonConvert.DeserializeObject<resp>(respstring);

                return collection;
            }
          
        }
        public static async Task downloadfile(string url)
        {
            using(var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var bytes=  response.Content.ReadAsByteArrayAsync();
                File.WriteAllBytes("temp.jpg", bytes.Result);
            }
        }

    }
}
