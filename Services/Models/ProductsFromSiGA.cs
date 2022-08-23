using System;

namespace Services.Models
{
    public class ProductsFromSiGA
    {
        public Int64 OrderNumber { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int InvoiceBranch { get; set; }
        public string OPC_Side { get; set; }
        public string OPC { get; set; }
        public decimal Quantity { get; set; }
        public int CompanyId { get; set; }
        public int AffiliationId { get; set; }
        public int WarehouseId { get; set; }
    }
}
