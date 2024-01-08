using Repositories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDTO>> GetAll();
        Task<IEnumerable<Categories>> GetPublishedCate();
        void Add(CategoryDTO cate);
        void Update(CategoryDTO cate);
        void Delete(CategoryDTO cate);
    }
}
