
using Services.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Services
{
    public interface IServiceDB
    {

        //Get the Orders that had been generated
        public DataTable GetOrdersGenerated(
                                            int pAcctionExecute,
                                            DateTime pCurrentDate,
                                            DateTime pStartDate,
                                            DateTime pFinalDate
                                           );

        //Link SiGA to Insert Order
        public void InsertOrderIntoSiGA(Order order);

        //Get invoiced products in SiGA
        public List<ProductsFromSiGA> GetInvoicedProductsInSiGA();

        //Update Products inventory in LMO
        public List<ProductsFromSiGAUpdatedOut> UpdateProductsInventoryInLMO(List<ProductsFromSiGA> pProductsFromSiGA);

        //Insert/Update into SiGA systems the Reference numbers come from update LMO Inventory
        public void UpdateRerenceNumberFromLMOToSiGA(ProductsFromSiGAUpdatedOut pReferenceNumbers);

        //Will go to OTL to Write down 1.- Invoice number (OTL Field: Factura) 2.- Date of Invoice number (OTL Field: FechaFactura)
        public void UpdateInvoiceDataFieldsInOTL(ProductsFromSiGA pReferenceNumbers);


    }
}
