using Dapper;
using Repositories.Const;
using Repositories.DataAccess;
using Repositories.Dto;
using Repositories.Interfaces;
using System.Data;
using System.Reflection.Metadata;

namespace Repositories.Implements
{
    public class SaleRepository : DapperBase, ISaleRepository
    {
        public async void Add(SaleDTO sale)
        {
            await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("productId", sale.ProductId, DbType.Int32);
                parameters.Add("quantity", sale.Quantity, DbType.Int32);
                parameters.Add("unitPrice", sale.UnitPrice, DbType.Int32);
                await connection.QueryAsync<SaleDTO>(StoreProcedureSale.AddSale, param: parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async void Delete(SaleDTO sale)
        {
            await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("saleId", sale.SaleId, DbType.Int32);
                parameters.Add("quantity", sale.Quantity, DbType.Int32);
                parameters.Add("productId", sale.ProductId, DbType.Int32);
                await connection.QueryAsync<SaleDTO>(StoreProcedureSale.DeleteSale, param: parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<IEnumerable<SaleDTO>> GetAll()
        {
            return await WithConnection(async connection =>
            {
                var listSales = await connection.QueryAsync<SaleDTO>(StoreProcedureSale.GetAllSale, null, commandType: CommandType.StoredProcedure);
                return listSales.ToList();
            });
        }

        public async Task<SaleDTO> GetById(int? Id)
        {
            return await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("saleId", Id, DbType.Int32);
                var product = await connection.QueryFirstOrDefaultAsync<SaleDTO>(StoreProcedureSale.GetSaleById, parameter, commandType: CommandType.StoredProcedure);
                return product;
            });
        }

        public async void Update(UpdateSaleDTO sale)
        {
            await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("saleId", sale.SaleId, DbType.Int32);
                parameter.Add("productId", sale.ProductId, DbType.Int32);
                parameter.Add("oldQuantity", sale.OldQuantity, DbType.Int32);
                parameter.Add("newQuantity", sale.NewQuantity, DbType.Int32);   
                parameter.Add("unitPrice", sale.UnitPrice, DbType.Int32);
                await connection.QueryAsync<UpdateSaleDTO>(StoreProcedureSale.UpdateSale, param: parameter, commandType: CommandType.StoredProcedure);
            });
        }
    }
}
