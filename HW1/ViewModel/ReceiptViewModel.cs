using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HW1.ViewModel
{
    public class ReceiptViewModel
    {
        // 輸入框
        [DisplayName("發票號碼：")]
        [Required(ErrorMessage = "請輸入您的發票號碼!")]
        public string Sets { get; set; }

        // 結果框
        [DisplayName("結果：")]
        public string Result { get; set; }
    }
}