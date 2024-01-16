using Dapper;
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

        public async Task<IEnumerable<Categories>> GetPublishedCate()
        {
            return await WithConnection(async connection =>
            {
                var categories = await connection.QueryAsync<Categories>(StoreProcedureCategory.GetPublishedCategories, null, commandType: CommandType.StoredProcedure);
                return categories.ToList();
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
        public async Task<IEnumerable<CategoryProductDTO>> GetAllCategory()
        {
            return await WithConnection(async connection =>
            {
                var listCategory = await connection.QueryAsync<CategoryProductDTO>(StoreProcedureCategoryProduct.UspAllCategory, null, commandType: CommandType.StoredProcedure);
                return listCategory.ToList();
            });
        }

        public async void AddCateExcel(string jsonString)
        {
            await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("CategoryData", jsonString);
                await connection.ExecuteAsync("InsertCategories", param: parameter, commandType: CommandType.StoredProcedure);
            });
        }
    }
}
