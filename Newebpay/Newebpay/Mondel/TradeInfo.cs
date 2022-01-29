using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newebpay.Mondel
{
    public class TradeInfo
    {
        /// <summary>
        /// * 商店代號
        /// </summary>
        public string MerchantID { get; set; }

        /// <summary>
        /// * 回傳格式
        /// <para>JSON 或是 String</para>
        /// </summary>
        public string RespondType { get; set; }

        /// <summary>
        /// * TimeStamp
        /// <para>自從 Unix 纪元（格林威治時間 1970 年 1 月 1 日 00:00:00）到當前時間的秒數。</para>
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// * 串接程式版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// * 商店訂單編號
        /// <para>限英、數字、_ 格式</para>
        /// <para>長度限制為20字</para>
        /// <para>同一商店中此編號不可重覆。</para>
        /// </summary>
        public string MerchantOrderNo { get; set; }

        /// <summary>
        /// * 訂單金額
        /// </summary>
        public int Amt { get; set; }

        /// <summary>
        /// * 商品資訊
        /// <para>1.限制長度為50字。</para>
        /// <para>2.編碼為Utf-8格式。</para>
        /// <para>3.請勿使用斷行符號、單引號等特殊符號，避免無法顯示完整付款頁面。</para>
        /// <para>4.若使用特殊符號，系統將自動過濾。</para>
        /// </summary>
        public string ItemDesc { get; set; }

        /// <summary>
        /// 支付完成 返回商店網址
        /// <para>1.交易完成後，以 Form Post 方式導回商店頁面。</para>
        /// <para>2.若為空值，交易完成後，消費者將停留在智付通付款或取號完成頁面。</para>
        /// <para>3.只接受80與443 Port。</para>
        /// </summary>
        public string ReturnURL { get; set; }

        /// <summary>
        /// 支付通知網址
        /// <para>1.以幕後方式回傳給商店相關支付結果資料</para>
        /// <para>2.只接受80與443 Port。</para>
        /// </summary>
        public string NotifyURL { get; set; }

        /// <summary>
        /// * 付款人電子信箱
        /// <para>於交易完成或付款完成時，通知付款人使用。</para>
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// * 智付通會員
        /// <para>0=不須登入智付通會員</para>
        /// <para>1=須要登入智付通會員</para>
        /// </summary>
        public int LoginType { get; set; }

        /// <summary>
        /// WEBATM啟用
        /// <para>設定是否啟用WEBATM支付方式。</para>
        /// <para>1 =啟用</para>
        /// <para>0 或者未有此參數，即代表不開啟</para>
        /// </summary>
        public int? WEBATM { get; set; }

        /// <summary>
        /// ATM 轉帳啟用
        /// <para>設定是否啟用ATM 轉帳支付方式。</para>
        /// <para>1 =啟用</para>
        /// <para>0 或者未有此參數，即代表不開啟</para>
        /// </summary>
        public int? VACC { get; set; }

        /// <summary>
        /// 超商代碼繳費啟用
        /// <para>設定是否啟用超商代碼繳費支付方式。</para>
        /// <para>1 =啟用</para>
        /// <para>0 或者未有此參數，即代表不開啟</para>
        /// <para>當該筆訂單金額小於 30 元或超過 2 萬元時，即使此參數設定為啟用，MPG 付款頁面仍不會顯示此支付方式選項。</para>
        /// </summary>
        public int? CVS { get; set; }

        /// <summary>
        /// 超商條碼繳費啟用
        /// <para>設定是否啟用超商條碼繳費支付方式。</para>
        /// <para>1 =啟用</para>
        /// <para>0 或者未有此參數，即代表不開啟</para>
        /// <para>當該筆訂單金額小於 20 元或超過 4 萬元時，即使此參數設定為啟用，MPG 付款頁面仍不會顯示此支付方式選項。</para>
        /// </summary>
        public int? BARCODE { get; set; }

    }
}
