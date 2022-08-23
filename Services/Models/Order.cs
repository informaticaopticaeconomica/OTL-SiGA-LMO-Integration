using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public class Order
    {

        public string OrderNumber { get; set; }
        public string BoxNumber { get; set; }
        public string CodigoProductoOD { get; set; }
        public string CodigoProductoOI { get; set; }
        public string OPCRightEye { get; set; }
        public string OPCLeftEye { get; set; }

    }
}
