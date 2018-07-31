using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project5.Models
{
    public class Customer
    {
        [ScaffoldColumn(false)]
        public int Customer_Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 2 до 20 символов")]
        [Display(Name = "Имя")]
        public string Customer_First_name { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 2 до 20 символов")]
        [Display(Name = "Фамилия")]
        public string Customer_Last_Name { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [RegularExpression(@"[0 - 9]{11}", ErrorMessage = "Некорректный номер")]
        [Display(Name = "Номер телефона")]
        public string Phone_number { get; set; }
    }
}