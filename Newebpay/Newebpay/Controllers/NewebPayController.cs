using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newebpay.Mondel.Util;
using Newebpay.Services;
using Newebpay.Mondel;
using System.Net.Http;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Newebpay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewebPayController : ControllerBase
    {
        [HttpGet]
        [Route("PostResponse")]
        public ActionResult Get()
        {
            return Ok("交易完成");
        }


        // POST api/<NewebPayController>
        [HttpPost]
        public SpgatewayOutputDataModel Post([FromBody] string value)
        {
            string version = "2.0";
            int amount = 100;
            string merchantID = "MS1558957485";
            string hashKey = "************************************************";
            string hashIV = "*********************";
            string url = "https://core.newebpay.com/MPG/mpg_gateway";

            // 目前時間轉換 +08:00, 防止傳入時間或Server時間時區不同造成錯誤
            DateTimeOffset taipeiStandardTimeOffset = DateTimeOffset.Now.ToOffset(new TimeSpan(8, 0, 0));

            TradeInfo tradeInfo = new TradeInfo()
            {
                // * 商店代號
                MerchantID = merchantID,
                // * 回傳格式
                RespondType = "JSON",
                // * TimeStamp
                TimeStamp = UnixDateTimeUtil.GetUNIX(),
                // * 串接程式版本
                Version = version,
                // * 商店訂單編號
                //MerchantOrderNo = $"T{DateTime.Now.ToString("yyyyMMddHHmm")}",
                MerchantOrderNo = $"T{DateTime.Now.ToString("yyyyMMddHHmm")}",
                // * 訂單金額
                Amt = amount,
                // * 商品資訊
                ItemDesc = "DonationPoints",
                // 繳費有效期限(適用於非即時交易)
                ReturnURL = "https://localhost:443/api/NewebPay/PostResponse",
                // 支付通知網址
                NotifyURL = "https://localhost:443/api/NewebPay/PostResponse",
                // * 付款人電子信箱
                Email = "ahhwayee666@gmail.com",
                // 付款人電子信箱 是否開放修改(1=可修改 0=不可修改)
                // WEBATM啟用(1=啟用、0或者未有此參數，即代表不開啟)
                WEBATM = 1,
                // ATM 轉帳啟用(1=啟用、0或者未有此參數，即代表不開啟)
                VACC = 0,
                // 超商代碼繳費啟用(1=啟用、0或者未有此參數，即代表不開啟)(當該筆訂單金額小於 30 元或超過 2 萬元時，即使此參數設定為啟用，MPG 付款頁面仍不會顯示此支付方式選項。)
                CVS = 0,
                // 超商條碼繳費啟用(1=啟用、0或者未有此參數，即代表不開啟)(當該筆訂單金額小於 20 元或超過 4 萬元時，即使此參數設定為啟用，MPG 付款頁面仍不會顯示此支付方式選項。)
                BARCODE = 0
            };

            AtomGeneric<string> result = new AtomGeneric<string>()
            {
                IsSuccess = true
            };

            var inputModel = new SpgatewayInputModel
            {
                MerchantID = merchantID,
                Version = version
            };

            // 將TradeInfo model 轉換為List<KeyValuePair<string, string>>, null值不轉
            List<KeyValuePair<string, string>> tradeData = NewebPayService.ModelToKeyValuePairList<TradeInfo>(tradeInfo);
            // 將List<KeyValuePair<string, string>> 轉換為 key1=Value1&key2=Value2&key3=Value3...
            string tradeQueryParameter = string.Join('&', tradeData.Select(eachTradeInfo => $"{eachTradeInfo.Key}={eachTradeInfo.Value}"));
            // AES 加密
            inputModel.TradeInfo = CryptoUtil.EncryptAESHex(tradeQueryParameter, hashKey, hashIV);
            // SHA256 加密
            inputModel.TradeSha = CryptoUtil.EncryptSHA256($"HashKey={hashKey}&{inputModel.TradeInfo}&HashIV={hashIV}");
            // SpgatewayInputModel model 轉換為List<KeyValuePair<string, string>>, null值不轉
            List<KeyValuePair<string, string>> postData = NewebPayService.ModelToKeyValuePairList<SpgatewayInputModel>(inputModel);
            //將 List<KeyValuePair> postData 轉為 Form Data
            FormUrlEncodedContent formDataContent = new FormUrlEncodedContent(postData);
            //HTTPCLIENT POST
            string responseBody = HttpService.PostForm(url, formDataContent);

            //在此回傳網頁HTML(測試可由後台執行、正式串接須由前端執行上面的程式碼後，再開啟並執行另一個回傳參數API
            Console.WriteLine(responseBody);

            //POST 回傳訊息，因為還未正式執行付款，所以拉回來的資料只是HTML，應等付完款後，藍新在call以下程式碼，也就是創一個API給他打
            SpgatewayOutputDataModel response = new SpgatewayOutputDataModel();
            if (!string.IsNullOrWhiteSpace(responseBody))
            {
                var theResponse = JsonConvert.DeserializeObject(responseBody);

                if (response.Status.Equals("SUCCESS"))
                {
                    return response;
                }
                else
                {
                    throw new Exception($"查詢失敗");
                }
            }
            return response;
        }
    }
}





//XmlDocument doc = new XmlDocument();
//doc.LoadXml(responseBody);
//var theResponse = JsonConvert.SerializeXmlNode(doc);

//response = JsonConvert.DeserializeObject<SpgatewayOutputDataModel>(theResponse);
