using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newebpay.Services
{
    public class NewebPayService
    {
        /// <summary>
        /// 將model 轉換為KeyValuePair List, null值不轉
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="model">欲轉換之Model</param>
        /// <returns></returns>
        public static List<KeyValuePair<string, string>> ModelToKeyValuePairList<T>(T model)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            try
            {
                Type t = model.GetType();
                foreach (var p in t.GetProperties())
                {
                    string name = p.Name;
                    object value = p.GetValue(model, null);
                    if (value != null)
                    {
                        result.Add(new KeyValuePair<string, string>(name, value.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex} : ModelToKeyValuePair失敗");
            }
            return result;
        }
    }
}
