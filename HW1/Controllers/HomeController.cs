using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using HW1.ViewModel;

namespace HW1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Trans()
        {
            return View();
        }

        public ActionResult Receipt()
        {
            return View();
        }

        [HttpPost]

        // 進位轉換
        public ActionResult Trans([Bind(Exclude = "Result")] TransViewModel inputData, string btnCompute)
        {
            if (!ModelState.IsValid)
            {
                return View(inputData);
            }
            try
            {
                // 二進位轉換
                if (inputData.Type == "二進位")
                {
                    // 二轉八
                    if(btnCompute == "八進位") inputData.Result = Convert.ToString(Convert.ToInt32(inputData.Sets.ToString(), 2), 8);
                    // 二轉十
                    else if (btnCompute == "十進位")  inputData.Result = Convert.ToInt32(inputData.Sets.ToString(),2).ToString();
                    // 二轉十六
                    else if (btnCompute == "十六進位") inputData.Result = Convert.ToString(Convert.ToInt32(inputData.Sets.ToString(), 2), 16);
                    // 二轉二(不變)
                    else inputData.Result = inputData.Sets.ToString();
                }

                // 八進位轉換
                else if(inputData.Type == "八進位")
                {
                    // 八轉二
                    if (btnCompute == "二進位") inputData.Result = Convert.ToString(Convert.ToInt32(inputData.Sets.ToString(), 8), 2);
                    // 八轉十
                    else if (btnCompute == "十進位") inputData.Result = Convert.ToInt32(inputData.Sets.ToString(), 8).ToString();
                    // 八轉十六
                    else if (btnCompute == "十六進位") inputData.Result = Convert.ToString(Convert.ToInt32(inputData.Sets.ToString(), 8), 16);
                    // 八轉八(不變)
                    else inputData.Result = inputData.Sets.ToString();
                }

                // 十進位轉換
                else if (inputData.Type == "十進位")
                {
                    // 十轉二
                    if (btnCompute == "二進位") inputData.Result = Convert.ToString(Convert.ToInt32(inputData.Sets),2);
                    // 十轉八
                    else if (btnCompute == "八進位") inputData.Result = Convert.ToString(Convert.ToInt32(inputData.Sets), 8);
                    // 十轉十六
                    else if (btnCompute == "十六進位") inputData.Result = Convert.ToString(Convert.ToInt32(inputData.Sets), 16);
                    // 十轉十(不變)
                    else inputData.Result = inputData.Sets.ToString();
                }

                // 十六進位轉換
                else if (inputData.Type == "十六進位")
                {
                    // 十六轉二
                    if (btnCompute == "二進位") inputData.Result = Convert.ToString(Convert.ToInt32(inputData.Sets.ToString(), 16), 2);
                    // 十六轉八
                    else if (btnCompute == "八進位") inputData.Result = Convert.ToString(Convert.ToInt32(inputData.Sets.ToString(), 16), 8);
                    // 十六轉十
                    else if (btnCompute == "十進位") inputData.Result = Convert.ToInt32(inputData.Sets.ToString(), 16).ToString();
                    // 十六轉十六(不變)
                    else inputData.Result = inputData.Sets.ToString();
                }

                return View(inputData);
            }
            catch (Exception)
            {
                // 進位發生例外
                string str = "進位轉換不符合規定";
                inputData.Result = str;
                return View(inputData);
            }
        }

        [HttpPost]

        // 發票兌獎
        public ActionResult Receipt([Bind(Exclude = "Result")] ReceiptViewModel inputData, string btnCompute)
        {
            try
            {
                // 連結 財政部稅務入口網
                WebRequest myRequest = WebRequest.Create(@"http://invoice.etax.nat.gov.tw/");
                // 用 GET 方法對網站進行請求
                myRequest.Method = "GET";
                // 得到回應後給 myResponse
                WebResponse myResponse = myRequest.GetResponse();
                // 將回應資料變成 StreamReader
                StreamReader sr = new StreamReader(myResponse.GetResponseStream());
                // 得到的網頁原始碼再從 StreamReader 變成 string
                string result = sr.ReadToEnd();
                // 關閉 StreamReader
                sr.Close();
                // 關閉 WebResponse
                myResponse.Close();

                string keyWord = "t18Red"; // 在得到資料中與獎號有關的關鍵字
                int SpecialIndex = result.IndexOf(keyWord, 0); // 找到 特別獎 獎號位置
                int GrandIndex = result.IndexOf(keyWord, SpecialIndex + 1); // 找到 特獎 獎號位置
                int FirstIndex = result.IndexOf(keyWord, GrandIndex + 1); // 找到 頭獎 獎號位置
                int AdditionalIndex = result.IndexOf(keyWord, FirstIndex + 1); // 找到 增開六獎 獎號位置

                string Special = result.Substring(SpecialIndex + 8, 8); // 特別獎 獎號
                string Grand = result.Substring(GrandIndex + 8, 8); // 特獎 獎號
                string First1 = result.Substring(FirstIndex + 8, 8);  // 頭獎 第一個獎號
                string First2 = result.Substring(FirstIndex + 17, 8); // 頭獎 第二個獎號
                string First3 = result.Substring(FirstIndex + 26, 8); // 頭獎 第三個獎號
                string Additional1 = result.Substring(AdditionalIndex + 8, 3);  // 增開六獎 第一個獎號
                string Additional2 = result.Substring(AdditionalIndex + 12, 3); // 增開六獎 第二個獎號
                string Additional3 = result.Substring(AdditionalIndex + 16, 3); // 增開六獎 第三個獎號

                // 比對中了特別獎 1,000 萬元
                if (inputData.Sets.Equals(Special))
                    inputData.Result = "恭喜中了特別獎 1,000 萬元";
                // 比對中了特獎 200 萬元 
                else if (inputData.Sets.Equals(Grand))
                    inputData.Result = "恭喜中了特獎 200 萬元";
                // 比對中了頭獎 20 萬元
                else if (inputData.Sets.Equals(First1) || inputData.Sets.Equals(First2) || inputData.Sets.Equals(First3))
                    inputData.Result = "恭喜中了頭獎 20 萬元";
                // 比對最後七個數字中了二獎 4 萬元
                else if (inputData.Sets.Substring(1, 7).Equals(First1.Substring(1, 7)) ||
                        inputData.Sets.Substring(1, 7).Equals(First2.Substring(1, 7)) ||
                        inputData.Sets.Substring(1, 7).Equals(First3.Substring(1, 7)))
                    inputData.Result = "恭喜中了二獎 4 萬元";
                // 比對最後六個數字中了三獎 1 萬元
                else if (inputData.Sets.Substring(2, 6).Equals(First1.Substring(2, 6)) ||
                        inputData.Sets.Substring(2, 6).Equals(First2.Substring(2, 6)) ||
                        inputData.Sets.Substring(2, 6).Equals(First3.Substring(2, 6)))
                    inputData.Result = "恭喜中了三獎 1 萬元";
                // 比對最後五個數字中了四獎 4 千元
                else if (inputData.Sets.Substring(3, 5).Equals(First1.Substring(3, 5)) ||
                        inputData.Sets.Substring(3, 5).Equals(First2.Substring(3, 5)) ||
                        inputData.Sets.Substring(3, 5).Equals(First3.Substring(3, 5)))
                    inputData.Result = "恭喜中了四獎 4 千元";
                // 比對最後四個數字中了五獎 1 千元
                else if (inputData.Sets.Substring(4, 4).Equals(First1.Substring(4, 4)) ||
                        inputData.Sets.Substring(4, 4).Equals(First2.Substring(4, 4)) ||
                        inputData.Sets.Substring(4, 4).Equals(First3.Substring(4, 4)))
                    inputData.Result = "恭喜中了五獎 1 千元";
                // 比對最後三個數字中了六獎 2 百元
                else if (inputData.Sets.Substring(5, 3).Equals(First1.Substring(5, 3)) ||
                        inputData.Sets.Substring(5, 3).Equals(First2.Substring(5, 3)) ||
                        inputData.Sets.Substring(5, 3).Equals(First3.Substring(5, 3)))
                    inputData.Result = "恭喜中了六獎 2 百元";
                // 比對最後三個數字中了增開六獎 2 百元
                else if (inputData.Sets.Substring(5, 3).Equals(Additional1) ||
                        inputData.Sets.Substring(5, 3).Equals(Additional2) ||
                        inputData.Sets.Substring(5, 3).Equals(Additional3))
                    inputData.Result = "恭喜中了增開六獎 2 百元";
                // 比對沒中任何獎
                else inputData.Result = "沒有中獎";

                return View(inputData);
            }
            catch (Exception)
            {
                // 比對發生例外
                string str = "輸入不符合規定";
                inputData.Result = str;
                return View(inputData);
            }
        }
    }
}