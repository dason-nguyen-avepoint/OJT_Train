using Repositories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IAccountManageRepository
    {
        Task<IEnumerable<AccountManageDTO>> GetAll(int pageNumber, int pageSize);
        Task<AccountManageDTO> GetById(int id);
        void Update(AccountManageDTO account);
        void Delete(int id);
        Task<IEnumerable<RoleDTO>> GetRole();
        Task<int> TotalAccount(string? role, string? searchBy);
        Task<IEnumerable<AccountManageDTO>> InfoUsers(int pageNumber, int pageSize, string searchBy);
    }
}
