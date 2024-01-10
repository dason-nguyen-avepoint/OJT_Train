using Dapper;
using Repositories.Const;
using Repositories.DataAccess;
using Repositories.Dto;
using Repositories.Interfaces;
using System.Data;

namespace Repositories.Implements
{
    public class InventoryRepository : DapperBase, IInventoryRepository
    {
        public async Task<IEnumerable<InventoryDTO>> GetInfor()
        {
            return await WithConnection(async connection =>
            {
                var inventories = await connection.QueryAsync<InventoryDTO>(StoreProcedureInventory.GetInforInventory, null, commandType: CommandType.StoredProcedure);
                return inventories.ToList();
            });
        }
    }
}
