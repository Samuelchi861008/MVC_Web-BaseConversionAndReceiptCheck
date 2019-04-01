# Base Conversion And Receipt Check

使用 ASP.NET MVC 建立一個網頁，擁有兩項不同樣的功能，分別為『進制轉換』與『發票兌獎』。使用者可以透過『進制轉換』方便從任一進制轉換制另一進制；亦可以透過『發票兌獎』，可供使用者進行輸入自己的發票號碼並比對最新一期獎號，查看是否中獎。 

## User Interface 

* 首頁 

![image](https://github.com/Samuelchi861008/MVC_Web-BaseConversionAndReceiptCheck/blob/master/home.png) 

* 進制轉換 

![image](https://github.com/Samuelchi861008/MVC_Web-BaseConversionAndReceiptCheck/blob/master/conver.png) 

* 發票兌獎 

![image](https://github.com/Samuelchi861008/MVC_Web-BaseConversionAndReceiptCheck/blob/master/receipt.png) 

## Features 

* 使用 ASP.NET MVC 

![image](https://github.com/Samuelchi861008/MVC_Web-BaseConversionAndReceiptCheck/blob/master/MVC.png) 

* 運用 C# 爬網頁資料抓取最新一期統一發票獎號 

```C#
//最前面需先呼叫
using System.IO; // WebRequest、WebResponse
using System.Net; // StreamReader
```
```C#
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
``` 

* 時下最流行顏色-莫蘭迪色系

頁面頭部 header : 

```
rgb(105,100,123)
```   
![image](https://github.com/Samuelchi861008/MVC_Web-BaseConversionAndReceiptCheck/blob/master/header.png) 


頁面背景 : 

```
rgb(208,193,198)
```   
![image](https://github.com/Samuelchi861008/MVC_Web-BaseConversionAndReceiptCheck/blob/master/body.png) 


各項目側邊標籤 : 

```
rgb(166,142,118)
```   
![image](https://github.com/Samuelchi861008/MVC_Web-BaseConversionAndReceiptCheck/blob/master/side.png) 


按鈕 : 

```
rgb(123,139,111)
```   
![image](https://github.com/Samuelchi861008/MVC_Web-BaseConversionAndReceiptCheck/blob/master/btn.png) 


單選進位選項 radio : 

```
rgb(146,172,209)
```   
![image](https://github.com/Samuelchi861008/MVC_Web-BaseConversionAndReceiptCheck/blob/master/label.png) 


每頁主題 : 

```
rgb(177,122,125)
```   
![image](https://github.com/Samuelchi861008/MVC_Web-BaseConversionAndReceiptCheck/blob/master/title.png)


## Use step 

* Step 1 
```bash
$ git clone https://github.com/Samuelchi861008/MVC_Web-BaseConversionAndReceiptCheck.git
``` 

* Step 2 
```bash
$ cd MVC_Web-BaseConversionAndReceiptCheck
``` 

* Step 3 
```
Use 『 Visual Studio 』open project
```

* Step 4 
```
Use IIS run the project
```


## Reference 

* 爬網頁資料 

https://blog.xuite.net/gyes.gyes/blog/63294651-c%23+擷取網頁資料 

 
* 莫蘭迪色 

https://www.zcool.com.cn/work/ZMjk2NDAyMTI=.html 


* 發票資訊

http://invoice.etax.nat.gov.tw/ 
