using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;

namespace ExternalServices.Common
{
    public class UpdateStockRequestEvent
    {
        public List<StockDto> StockDto { get; set; }
    }
}
