using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;

namespace ExternalServices.Common
{
    public class UpdatedStockResponseEvent
    {
        public UpdatedStockResponseEvent(Guid guid, string name, int orderId)
        {
            Guid = guid;
            OrderId = orderId;
            Name = name;
        }

        public Guid Guid { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
    }
}
