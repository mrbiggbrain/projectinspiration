using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;


namespace ProjectInspiration.SDK.Shared.Web
{
    static class APIRequestor
    {
        //static T Get<T>(IServiceObject service, String controller, String action)
        //{
        //
        //}

        public static T Post<T,O>(IServiceObject service, String controller, String action, O obj)
        {
            return Post<T>(service, controller, action, JsonConvert.SerializeObject(obj, Formatting.Indented));
        }

        public static T Post<T>(IServiceObject service, String controller, String action, String JSON)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(APIPath(service.ServiceRoot, controller, action));
            request.Method = "POST";
            request.Headers.Add("APIKEY", service.APIKey);

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
                        return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
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
            if(!String.IsNullOrEmpty(controller))
            {
                finalAddress += $"/{controller}";

                if(!String.IsNullOrEmpty(action))
                {
                    finalAddress += $"/{action}";
                }
            }

            return finalAddress;
        }
    }
}
