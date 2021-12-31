using System.Runtime.Serialization;
using System;
using System.Net;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace BAYCNFT_Downloader
{
    class Program
    {
        static void Main(string[] args)
        {
            const string assetContractAddress = "https://api.opensea.io/api/v1/asset/0xbc4ca0eda7647a8ab7c2061c2e118a18a936f13d";
            int assetID = 0;
            string uri = $"{assetContractAddress}/{assetID}";
            bool status = CheckURLStatus("uri");

            // while (status == true)
            // {
            //     var client = new RestClient("uri");
            //     var request = new RestRequest(Method.GET);
            //     IRestResponse response = client.Execute(request);
            // }

            var client = new RestClient(uri);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            JObject o = JObject.Parse(response.Content);
            string imageURL = (string)o.SelectToken("image_url");
            DownloadImage(imageURL, assetID);
        }
        public static bool CheckURLStatus(string website)
        {
            try
            {
                var request = WebRequest.Create(website) as HttpWebRequest;
                request.Method = "HEAD";
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch
            {
                return false;
            }
        }
        public static void DownloadImage(string imageUrl, int assetID)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(imageUrl), @$"C:\Users\Joe\Pictures\BAYC_NFTs\BAYC_{assetID}.png");
            }
        }
    }
}