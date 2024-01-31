using Dapper;
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

namespace Repositories.Implements
{
	public class PromotionuRepository: DapperBase, IPromotionuRepository
	{
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
