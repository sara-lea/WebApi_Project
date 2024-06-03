using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTOs
{
    public class orderItemDTO
    {
        public int OrderItemId { get; set; }

        public int ProductId { get; set; }

        //public string? ProductName { get; set; }

        public int OrderId { get; set; }

        public int Quantity { get; set; }
    }
}
