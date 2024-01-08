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
        Task<ProductDTO> GetById(int? id);
        void Add(ProductDTO product);
        void Update(ProductDTO product);
        void Delete(ProductDTO product);

    }
}
