using Repositories.Dto;

namespace Repositories.Interfaces
{
    public interface ISaleRepository
    {
        Task<IEnumerable<SaleDTO>> GetAll();
        Task<SaleDTO> GetById(int? Id);
        void Add(SaleDTO sale);
        void Update(UpdateSaleDTO sale);
        void Delete(SaleDTO sale);
    }
}
