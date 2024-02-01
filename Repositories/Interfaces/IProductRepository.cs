using Repositories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDTO>> GetAll();
        Task<IEnumerable<ValidProductDTO>> GetValidProduct();
        Task<ProductDTO> GetById(int? id);
        void Add(ProductDTO product);
        void Update(ProductDTO product);
        void Delete(ProductDTO product);
        Task<IEnumerable<ProductInUserViewDTO>> GetAllProduct();
        Task<IEnumerable<ProductInUserViewDTO>> GetProductByCategory(int? id);
        Task<ProductInUserViewDTO?> GetProductByID(int? id);
        Task<IEnumerable<ProductDetailImgDto>> GetProductByIDIMG(int? id);
        Task<IEnumerable<ProductInUserViewDTO>> TopfiveProductbyPriceNew();
        Task<IEnumerable<ProductInUserViewDTO>> TopfiveProductbyMemories();
	}
}
