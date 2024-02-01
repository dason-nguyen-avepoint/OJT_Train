using Dapper;
using Data.Helpers;
using Repositories.Const;
using Repositories.DataAccess;
using Repositories.Dto;
using Repositories.Interfaces;
using System.Data;

namespace Repositories.Implements
{
    public class OrderRepository : DapperBase, IOrderRepository
    {
        public async void DeleteOrder(int id)
        {
            await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("orderId", id, DbType.Int32);
                await connection.ExecuteAsync("DeleteOrderByAdmin", param: parameter, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<IEnumerable<OrderDTO>> GetAll(int pageNumber, int pageSize)
        {
            return await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("pageNumber", pageNumber, DbType.Int32);
                parameter.Add("pageSize", pageSize, DbType.Int32);
                var orders = await connection.QueryAsync<OrderDTO>("GetOrderInfo", param: parameter, commandType: CommandType.StoredProcedure);
                return orders.ToList();
            });
        }

        public async Task<OrderById> GetById(int id)
        {
            return await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("orderId", id, DbType.Int32);
                var order = await connection.QueryFirstOrDefaultAsync<OrderById>("GetOrderById", param: parameter, commandType: CommandType.StoredProcedure);
                return order;
            });
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetail(int id)
        {
            return await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("orderId", id, DbType.Int32);
                var orderDetails = await connection.QueryAsync<OrderDetail>("GetOrderDetail", param: parameter, commandType: CommandType.StoredProcedure);
                return orderDetails.ToList();
            });
        }

        public async void ShippingOrder(int id)
        {
            await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("orderId", id, DbType.Int32);
                await connection.ExecuteAsync("ShippingOrder", param: parameter, commandType: CommandType.StoredProcedure);
            });
        }
        public async void ShipCompleted(int id)
        {
            await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("orderId", id, DbType.Int32);
                await connection.ExecuteAsync("ShipCompleted", param: parameter, commandType: CommandType.StoredProcedure);
            });
        }
        public async Task<IEnumerable<OrderById>> GetByUserId(int userId)
        {
            return await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("userId", userId, DbType.Int32);
                var order = await connection.QueryAsync<OrderById>("GetOrderByUserId", param: parameter, commandType: CommandType.StoredProcedure);
                return order.ToList();
            });
        }
        public async Task<IEnumerable<OrderDTO>> GetShips(int pageNumber, int pageSize)
        {
            return await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("pageNumber", pageNumber, DbType.Int32);
                parameter.Add("pageSize", pageSize, DbType.Int32);
                var ships = await connection.QueryAsync<OrderDTO>("GetShipInfo", param: parameter, commandType: CommandType.StoredProcedure);
                return ships.ToList();
            });
        }
        public async Task<int> TotalOrder()
        {
            return await WithConnection(async connection =>
            {
                int orders = (int)await connection.ExecuteScalarAsync("TotalOrder", null, commandType: CommandType.StoredProcedure);
                return orders;
            });
        }
        public async Task<int> TotalShip()
        {
            return await WithConnection(async connection =>
            {
                int orders = (int)await connection.ExecuteScalarAsync("TotalShip", null, commandType: CommandType.StoredProcedure);
                return orders;
            });
        }

        #region HUNG
        //public async Task<int> UspOrder(int orderPrice,int userID, string createBy, string address)
        //{
        //	return await WithConnection(async connection =>
        //	{
        //		DynamicParameters parameters = new DynamicParameters();
        //		parameters.Add("orderPrice", orderPrice, DbType.Int32);
        //		parameters.Add("userID", userID, DbType.Int32);
        //		parameters.Add("createBy", createBy, DbType.String);
        //		parameters.Add("address", orderPrice, DbType.String);
        //		int check = await connection.ExecuteScalarAsync<int>(StoreProcedureOrderu.UspOrder, param: parameters, commandType: CommandType.StoredProcedure);
        //		return check;
        //	});
        //}

        public async void AddOrderandOrderDetail(decimal OrderPrice, string CreatedBy, string Address, int UserID, List<OrderU> model)
		{
			await WithConnection(async connection =>
			{
				var parameters = new DynamicParameters();
				parameters.Add("OrderPrice", OrderPrice, DbType.Decimal);
				parameters.Add("CreatedBy", CreatedBy, DbType.String);
				parameters.Add("Address", Address, DbType.String);
				parameters.Add("UserID", UserID, DbType.Int32);
				var jInput = JsonDeserializeHelper.SerializeObjectForDb(model);
				parameters.Add("@jInput", jInput, DbType.String, ParameterDirection.Input);
				await connection.ExecuteAsync(StoreProcedureOrderu.UspAddOrderAndOrderDetail, param: parameters, commandType: CommandType.StoredProcedure);
			});
		}
		public async Task<IEnumerable<OrderForcheckHistory>> GetOrdercheckhistory(int userid)
		{
			return await WithConnection(async connection =>
			{
				var parameter = new DynamicParameters();
				parameter.Add("userId", userid, DbType.Int32);
				var orderhistory = await connection.QueryAsync<OrderForcheckHistory>(StoreProcedureOrderu.UspOrderforcheckfistory, param: parameter, commandType: CommandType.StoredProcedure);
				return orderhistory.ToList();
			});
		}
        public async Task UpdateOrder(int OrderId)
        {
            await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
              
                parameters.Add("@OrderId", OrderId, DbType.Int32);     
                await connection.ExecuteAsync(StoreProcedureOrderu.UspDeleteOrder, param: parameters, commandType: CommandType.StoredProcedure);
            });
        }
        #endregion
        public async Task<int> TotalOrder()
		{
			return await WithConnection(async connection =>
			{
				int orders = (int)await connection.ExecuteScalarAsync("TotalOrder", null, commandType: CommandType.StoredProcedure);
				return orders;
			});
		}

		
	}
}
