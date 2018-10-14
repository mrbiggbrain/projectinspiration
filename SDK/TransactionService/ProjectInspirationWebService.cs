using Newtonsoft.Json;
using ProjectInspiration.SDK.Shared.TransactionService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ProjectInspiration.SDK.Shared.TransactionService
{
    public class ProjectInspirationWebService : ITransactionService
    {

        private string baseAPIPath;
        private string apiKey;

        public ProjectInspirationWebService(String baseAPIPath, String apiKey)
        {
            this.baseAPIPath = baseAPIPath;
            this.apiKey = apiKey;
        }

        public T Get<T>(string controller = null, string action = null, IEnumerable<ITransactionParameter> parama = null)
        {
            throw new NotImplementedException();
        }

        public T Post<T, O>(O obj, string controller = null, string action = null, IEnumerable<ITransactionParameter> parama = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(APIPath(this.baseAPIPath, controller, action));
            request.Method = "POST";
            request.Headers.Add("APIKEY", this.apiKey);

            String JSON = JsonConvert.SerializeObject(obj, Formatting.Indented);

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(JSON);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                        //return reader.ReadToEnd();

                        String content = reader.ReadToEnd();

                        var settings = new JsonSerializerSettings()
                        {
                            TypeNameHandling = TypeNameHandling.All
                        };

                        Console.WriteLine(content);
                        return JsonConvert.DeserializeObject<T>(content, settings);
                    }
                }
            }
            catch (WebException ex)
            {
                // Log exception and throw as for GET example above
                throw;
            }
        }

        static public String APIPath(String servicePath, String controller, String action)
        {
            String finalAddress = servicePath;
            if (!String.IsNullOrEmpty(controller))
            {
                finalAddress += $"/{controller}";

                if (!String.IsNullOrEmpty(action))
                {
                    finalAddress += $"/{action}";
                }
            }

            return finalAddress;
        }
    }
}
