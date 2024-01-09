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
        public async void Delete(SaleDTO sale)
        {
            await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("saleId", sale.SaleId);
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
    }
}
