using System;
using System.Net;

namespace BAYCNFT_Downloader
{
    class Program
    {
        static void Main(string[] args)
        {
            const string assetContractAddress = "https://opensea.io/assets/0xbc4ca0eda7647a8ab7c2061c2e118a18a936f13d";
            int assetID = 0;
            string uri = $"{assetContractAddress}/{assetID}";
            bool status = CheckURLStatus("uri");

            while (status == true)
            {
                var client = new RestClient("uri");
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
            }
        }
        public bool CheckURLStatus(string website)
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
    }
}