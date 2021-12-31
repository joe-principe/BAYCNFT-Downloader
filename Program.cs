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

            for (int assetID = 0; assetID < 10000; assetID++)
            {
                string url = $"{assetContractAddress}/{assetID}";
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                JObject o = JObject.Parse(response.Content);
                string imageURL = (string)o.SelectToken("image_url");

                DownloadImage(imageURL, assetID);
            }
        }
        /// <summary>
        /// Checks to see if the given website URL is valid
        /// No longer used, but still useful if I decide to rewrite this code
        /// </summary>
        /// <param name="website">A string containing the website URL</param>
        /// <returns>A boolean indicating whether or not the website exists</returns>
        /* public static bool CheckURLStatus(string website)
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
                Console.WriteLine("The URL does not exist.");
                return false;
            }
        } */

        /// <summary>
        /// Saves the image as a .png to a directory in the Pictures directory
        /// </summary>
        /// <param name="imageUrl">A string containing the image URL</param>
        /// <param name="assetID">An integer containing the ID of the NFT</param>
        public static void DownloadImage(string imageUrl, int assetID)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(imageUrl), @$"C:\Users\Joe\Pictures\BAYC_NFTs\BAYC_{assetID}.png");
                Console.WriteLine($"Monkey NFT #{assetID} has been downloaded");
            }
        }
    }
}