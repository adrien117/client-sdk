using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Checkout.ApiServices.Shopping.Dto;
using Checkout.ApiServices.Shopping.Settings;
using Newtonsoft.Json;

namespace Checkout.ApiServices.Shopping
{
    public class ShopService
    {
        private WebRequest _httpClient;
        private NameValueCollection _queryString;

        public ShopService()
        {     
            _queryString = HttpUtility.ParseQueryString(string.Empty);
        }

        public  IEnumerable<ShopItemDto> GetAllShopItemAsync()
        {
            var uri = Configurations.ServiceApiUri + _queryString;
            _httpClient = WebRequest.Create(uri);
            _httpClient.Headers.Add("Ocp-Apim-Subscription-Key", Configurations.ServiceApiKey);
            _httpClient.Method = WebRequestMethods.Http.Get;
            var res = _httpClient.GetResponse();
            Stream stream = res.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string responseFromServer = reader.ReadToEnd();
            var results = JsonConvert.DeserializeObject<List<ShopItemDto>>(responseFromServer);
            return results;
        }

        public ShopItemDto GetShopItemById(string name)
        {
            var uri = Configurations.ServiceApiUri + name + "?"+ _queryString;
            _httpClient = WebRequest.Create(uri);
            _httpClient.Headers.Add("Ocp-Apim-Subscription-Key", Configurations.ServiceApiKey);
            _httpClient.Method = WebRequestMethods.Http.Get;
            var res = _httpClient.GetResponse();
            Stream stream = res.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string responseFromServer = reader.ReadToEnd();
            var result = JsonConvert.DeserializeObject<ShopItemDto>(responseFromServer);
            return result;
        }

        public ShopItemDto AddShopItem(ShopItemDto item)
        {
            var uri = Configurations.ServiceApiUri + "?" + _queryString;
            _httpClient = WebRequest.Create(uri);
            _httpClient.Headers.Add("Ocp-Apim-Subscription-Key", Configurations.ServiceApiKey);
            _httpClient.Method = WebRequestMethods.Http.Post;
            var x = JsonConvert.SerializeObject(item);
            var data = Encoding.ASCII.GetBytes(x);
            _httpClient.ContentLength = data.Length;
            _httpClient.ContentType = "application/json";

            using (var s = _httpClient.GetRequestStream())
            {
                s.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)_httpClient.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var result = JsonConvert.DeserializeObject<ShopItemDto>(responseString);
            return result;
        }

        public ShopItemDto UpdateShopItem(ShopItemDto item)
        {
            var x = JsonConvert.SerializeObject(item);
            var data = Encoding.ASCII.GetBytes(x);

            var uri = Configurations.ServiceApiUri + "?" + _queryString;
            _httpClient = WebRequest.Create(uri);
            _httpClient.Headers.Add("Ocp-Apim-Subscription-Key", Configurations.ServiceApiKey);
            _httpClient.Method = WebRequestMethods.Http.Put;
            _httpClient.ContentLength = data.Length;
            _httpClient.ContentType = "application/json";

            using (var s = _httpClient.GetRequestStream())
            {
                s.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)_httpClient.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var result = JsonConvert.DeserializeObject<ShopItemDto>(responseString);
            return result;

        }

        public void DeleteShopItem(string name)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Configurations.ServiceApiKey);

            var uri = Configurations.ServiceApiUri + name + "?" + _queryString;

            var response = client.DeleteAsync(uri);
        }
    }
}
