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
        public async void Add(CategoryDTO cate)
        {
            await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("categoryName", cate.CategoryName);
                parameter.Add("isPublished", cate.IsPublished);
                await connection.ExecuteAsync(StoreProcedureCategory.AddCategory, param: parameter, commandType: CommandType.StoredProcedure);
            });
        }

        public async void Delete(CategoryDTO cate)
        {
            await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("categoryId", cate.CategoryId);
                await connection.ExecuteAsync(StoreProcedureCategory.DeleteCategory, param: parameter, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            return await WithConnection(async connection =>
            {
                var listCategory = await connection.QueryAsync<CategoryDTO>(StoreProcedureCategory.GetAllCategory, null, commandType: CommandType.StoredProcedure);
                return listCategory.ToList();
            });
        }

        public async void Update(CategoryDTO cate)
        {
            await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("categoryId", cate.CategoryId);
                parameter.Add("categoryName", cate.CategoryName);
                parameter.Add("isPublished", cate.IsPublished);
                await connection.ExecuteAsync(StoreProcedureCategory.UpdateCategory, param: parameter, commandType: CommandType.StoredProcedure);
            });
        }
    }
}
