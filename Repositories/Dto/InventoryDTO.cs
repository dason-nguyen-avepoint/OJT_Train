using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Dto
{
    public class InventoryDTO
    {
        public string? ProductName { get; set; }
        public int TotalImport { get; set; }
        public int Delivered { get; set; }
        public int Remaining { get; set; }
    }
}
