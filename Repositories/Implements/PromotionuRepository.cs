using Dapper;
using Newtonsoft.Json.Linq;
using Repositories.Const;
using Repositories.DataAccess;
using Repositories.Dto;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Repositories.Implements
{
	public class PromotionuRepository: DapperBase, IPromotionuRepository
	{
        public async Task<PromotionDTO> CheckValidCoupon(string code)
        {
            return await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("code", code);
                var promos = await connection.QuerySingleOrDefaultAsync<PromotionDTO>("CheckPromo", param: parameter, commandType: CommandType.StoredProcedure);
                return promos;
            });
        }

        public async void Delete(int id)
        {
            await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("id", id, DbType.Int32);
                await connection.ExecuteAsync("DeletePromo", param: parameter, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<IEnumerable<PromotionDTO>> GetPromo(int pageNumber, int pageSize)
        {
			return await WithConnection(async connection =>
			{
				var parameter = new DynamicParameters();
				parameter.Add("pageNumber", pageNumber, DbType.Int32);
				parameter.Add("pageSize", pageSize, DbType.Int32);
				var promos = await connection.QueryAsync<PromotionDTO>("GetAllPromo", param: parameter, commandType: CommandType.StoredProcedure);
				return promos.ToList();
			});
        }

        public async Task<int> TotalCoupon()
        {
            return await WithConnection(async connection =>
            {
                var promos = (int) await connection.ExecuteScalarAsync("TotalPromo", null, commandType: CommandType.StoredProcedure);
				return promos;
            });
        }

        public async void Update(int id, string code, int value)
        {
            await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("id", id, DbType.Int32);
                parameter.Add("value", value, DbType.Int32);
                parameter.Add("code", code);
                await connection.ExecuteAsync("UpdatePromo", param: parameter, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<Promotionu> UspGetPromotionu(string coupon)
		{
			return await WithConnection(async connection =>
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("Promotioncode", coupon, DbType.String);
				var result = await connection.QuerySingleOrDefaultAsync<Promotionu>(StoreProcedurePromotionu.UspGetPromotionu, param: parameters, commandType: CommandType.StoredProcedure);
				return result;
			});
		}
	}
}
