using Dapper;
using Repositories.DataAccess;
using Repositories.Dto;
using Repositories.Interfaces;
using System.Data;

namespace Repositories.Implements
{
    public class ThongKeRepository : DapperBase, IThongKeRepository
    {
        public async Task<ThongKeDTO> GetInfo()
        {
            return await WithConnection(async connection =>
            {
                var thongke = await connection.QueryFirstOrDefaultAsync<ThongKeDTO>("THONGKE", null, commandType: CommandType.StoredProcedure);
                return thongke;
            });
        }
    }
}
