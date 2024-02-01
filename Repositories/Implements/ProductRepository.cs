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

        public async Task<IEnumerable<ValidProductDTO>> GetValidProduct()
        {
            return await WithConnection(async connection =>
            {
                var products = await connection.QueryAsync<ValidProductDTO>(StoreProcedureProduct.GetValidProducts, null, commandType: CommandType.StoredProcedure);
                return products.ToList();
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
        public async Task<IEnumerable<ProductInUserViewDTO>> GetAllProduct()
        {
            return await WithConnection(async connection =>
            {
                var listProduct = await connection.QueryAsync<ProductInUserViewDTO>(StoreProcedureCategoryProduct.UspAllProduct, null, commandType: CommandType.StoredProcedure);
                return listProduct.ToList();
            });
        }

        public async Task<IEnumerable<ProductInUserViewDTO>> GetProductByCategory(int? id)
        {
            return await WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("CategoryId", id, DbType.Int32);
                var listproduct = await connection.QueryAsync<ProductInUserViewDTO>(StoreProcedureCategoryProduct.UspGetListProductByCategory, param: parameters, commandType: CommandType.StoredProcedure);
                return listproduct.ToList();
            });
        }
        public async Task<ProductInUserViewDTO?> GetProductByID(int? id)
        {
            return await WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ProductId", id, DbType.Int32);
                var product = await connection.QuerySingleOrDefaultAsync<ProductInUserViewDTO>(StoreProcedureCategoryProduct.UspGetProductDetails, param: parameters, commandType: CommandType.StoredProcedure);
                return product;
            });
        }
        public async Task<IEnumerable<ProductDetailImgDto>> GetProductByIDIMG(int? id)
        {
            return await WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ProductId", id, DbType.Int32);
                var product = await connection.QueryAsync<ProductDetailImgDto>(StoreProcedureCategoryProduct.UspgetProductDetailImg, param: parameters, commandType: CommandType.StoredProcedure);
                return product.ToList();
            });
        }
		public async Task<IEnumerable<ProductInUserViewDTO>> TopfiveProductbyPriceNew()
		{
			return await WithConnection(async connection =>
			{
				var product = await connection.QueryAsync<ProductInUserViewDTO>(StoreProcedureCategoryProduct.uspTopFIVEProduct, null, commandType: CommandType.StoredProcedure);
				return product.ToList();
			});
		}
		public async Task<IEnumerable<ProductInUserViewDTO>> TopfiveProductbyMemories()
		{
			return await WithConnection(async connection =>
			{
				var product = await connection.QueryAsync<ProductInUserViewDTO>(StoreProcedureCategoryProduct.uspTopFiveProductbyMemory, null, commandType: CommandType.StoredProcedure);
				return product.ToList();
			});
		}
	}
}
