using Repositories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderDTO>> GetAll(int pageNumber, int pageSize);
        Task<OrderById> GetById(int id);
        void ShippingOrder(int id);
        Task<IEnumerable<OrderDetail>> GetOrderDetail(int id);
        void DeleteOrder(int id);
        Task<int> TotalOrder();
    }
}
