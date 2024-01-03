using Dapper;
using Repositories.Const;
using Repositories.DataAccess;
using Repositories.Dto;
using Repositories.Interfaces;
using System.Data;

namespace Repositories.Implements
{
    public class ProductRepository : DapperBase, IProductRepository
    {
        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            return await WithConnection(async connection =>
            {
                var listProduct = await connection.QueryAsync<ProductDTO>(StoreProcedureProduct.GetAllProduct, null, commandType: CommandType.StoredProcedure);
                return listProduct.ToList();
            });
        }
    }
}
