using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Repositories.Const;
using Repositories.DataAccess;
using Repositories.Dto;
using Repositories.Interfaces;
using System.Data;
namespace Repositories.Implements
{
    public class CategoryRepository : DapperBase, ICategoryRepository
    {
        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            return await WithConnection(async connection =>
            {
                var listCategory = await connection.QueryAsync<CategoryDTO>(StoreProcedureCategory.GetCategory,null, commandType: CommandType.StoredProcedure);
                return listCategory.ToList();
            });
        }
    }
}
