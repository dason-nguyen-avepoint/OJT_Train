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
        Task<IEnumerable<OrderById>> GetByUserId(int userId);
        void ShippingOrder(int id);
        Task<IEnumerable<OrderDetail>> GetOrderDetail(int id);
        void AddOrderandOrderDetail(decimal OrderPrice, string CreatedBy, string Address, int UserID, List<OrderU> model);
        Task<IEnumerable<OrderForcheckHistory>> GetOrdercheckhistory(int userid);
		void DeleteOrder(int id);
        Task UpdateOrder(int OrderId);
        void ShipCompleted(int id);
        Task<int> TotalOrder();
        Task<int> TotalShip();
        Task<IEnumerable<OrderDTO>> GetShips(int pageNumber, int pageSize);
    }
}
