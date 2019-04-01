using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HW1.ViewModel
{
    public class TransViewModel
    {
        // 選擇輸入進制
        [DisplayName("數值進制：")]
        [Required(ErrorMessage = "請選擇欲輸入的進制!")]
        public string Type { get; set; }

        // 輸入框
        [DisplayName("數值：")]
        [Required(ErrorMessage = "請輸入數值!")]
        public string Sets { get; set; }

        // 結果框
        [DisplayName("結果：")]
        public string Result { get; set; }
    }
}