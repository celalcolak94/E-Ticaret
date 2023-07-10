﻿using E_Ticaret.Data.Entities;

namespace E_Ticaret.Models
{
    public class OrderDetailsModel
    {
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public EnumOrderState? OrderState { get; set; }

        public string AdresBasligi { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Semt { get; set; }
        public string Mahalle { get; set; }
        public string? PostaKodu { get; set; }

        public virtual List<OrderLineModel> OrderLines { get; set; }
    }

    public class OrderLineModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
