using Repositories.Dto;

namespace Repositories.Interfaces
{
    public interface ISaleRepository
    {
        Task<IEnumerable<SaleDTO>> GetAll();
        void Delete(SaleDTO sale);
    }
}
