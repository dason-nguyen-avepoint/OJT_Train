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
        public async void Add(ProductDTO product)
        {
            await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("productName", product.ProductName);
                parameter.Add("memory", product.Memory);
                parameter.Add("priceOld", product.PriceOld);
                parameter.Add("productDetail", product.ProductDetail);
                parameter.Add("isPublished", product.IsPublished);
                parameter.Add("categoryId", product.CategoryId);
                parameter.Add("imageProduct", product.ImageProduct);
                await connection.QueryAsync<ProductDTO>(StoreProcedureProduct.AddProduct, param: parameter, commandType: CommandType.StoredProcedure);
            });
        }

        public async void Delete(ProductDTO product)
        {
            await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("productId", product.ProductId, DbType.Int32);
                await connection.QueryAsync<ProductDTO>(StoreProcedureProduct.DeleteProduct, param: parameter, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            return await WithConnection(async connection =>
            {
                var listProduct = await connection.QueryAsync<ProductDTO>(StoreProcedureProduct.GetAllProduct, null, commandType: CommandType.StoredProcedure);
                return listProduct.ToList();
            });
        }

        public async Task<ProductDTO> GetById(int? id)
        {
            return await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("productId", id, DbType.Int32);
                var product = await connection.QueryFirstOrDefaultAsync<ProductDTO>(StoreProcedureProduct.GetProductById, parameter, commandType: CommandType.StoredProcedure);
                return product;
            });
        }

        public async void Update(ProductDTO product)
        {
            await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("productId", product.ProductId, DbType.Int32);
                parameter.Add("productName", product.ProductName);
                parameter.Add("memory", product.Memory);
                parameter.Add("priceOld", product.PriceOld);
                parameter.Add("productDetail", product.ProductDetail);
                parameter.Add("isPublished", product.IsPublished);
                parameter.Add("categoryId", product.CategoryId);
                parameter.Add("imageProduct", product.ImageProduct);
                await connection.QueryAsync<ProductDTO>(StoreProcedureProduct.UpdateProduct, param: parameter, commandType: CommandType.StoredProcedure);
            });
        }
    }
}
