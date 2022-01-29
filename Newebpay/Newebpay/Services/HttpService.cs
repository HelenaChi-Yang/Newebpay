using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace Newebpay.Services
{
    internal class HttpService
    {
        /// <summary>
        /// 執行HttpClient Post
        /// </summary>
        /// <param name="url">Post網址</param>
        /// <param name="formContent">form參數</param>
        /// <returns></returns>
        internal static string PostForm(string url, FormUrlEncodedContent formContent)
        {
            string responseBody = string.Empty;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //清除
                    client.DefaultRequestHeaders.Clear();

                    //安全協定
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    client.DefaultRequestHeaders.Accept.Clear();

                    //ACCEPT header，指示资源的media type
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Content-Type 用於宣告遞送給對方的文件型態
                    //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

                    //CONTENT-TYPE header???

                    //get the content returned
                    HttpResponseMessage response = client.PostAsync(url, formContent).Result;
                    //HttpResponseMessage response = client.PostAsJsonAsync(url, formContent).Result;
                    //Console.WriteLine(response);

                    if (response.IsSuccessStatusCode)
                    {
                        //Content:取得或設定 HTTP 回應訊息的內容，ReadAsStringAsync，以非同步作業方式將 HTTP 內容序列化為字串
                        responseBody = response.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch
            {
                throw;
            }

            return responseBody;
        }
    }
}
