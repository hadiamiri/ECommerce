using System;
using System.ComponentModel;

namespace Shop.Areas.Admin.ViewModels
{
    public class OrderIndexViewModel
    {
        [DisplayName("شماره فاکتور")]
        public int Id { get; set; }
        [DisplayName("تاریخ")]
        public DateTime Date { get; set; }
        [DisplayName("قیمت")]
        public decimal Price { get; set; }
        [DisplayName("نام مشتری")]
        public string UserName { get; set; }
        [DisplayName("وضعیت")]
        public string Status { get; set; }
    }
}