namespace E_Ticaret.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

        public string UserName { get; set; }
        public string AdresBasligi { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Semt { get; set; }
        public string Mahalle { get; set; }
        public string? PostaKodu { get; set; }

        public virtual List<OrderLine> OrderLines { get; set; }
    }

    public class OrderLine
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
