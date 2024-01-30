using Dapper;
using Repositories.DataAccess;
using Repositories.Dto;
using Repositories.Interfaces;
using System.Data;

namespace Repositories.Implements
{
    public class AccountManageRepository : DapperBase, IAccountManageRepository
    {
        public async void Delete(int id)
        {
            await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("userId", id, DbType.Int32);
                await connection.ExecuteAsync("DeleteAccount", param: parameter, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<IEnumerable<AccountManageDTO>> GetAll(int pageNumber, int pageSize)
        {
            return await WithConnection(async connection =>
            { 
                var parameter = new DynamicParameters();
                parameter.Add("pageNumber", pageNumber, DbType.Int32);
                parameter.Add("pageSize", pageSize, DbType.Int32);
                var accounts = await connection.QueryAsync<AccountManageDTO>("GetAccountInfo", param:parameter, commandType: CommandType.StoredProcedure);
                return accounts.ToList();
            });
        }

        public async Task<AccountManageDTO> GetById(int id)
        {
            return await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("userId", id, DbType.Int32);
                var account = await connection.QueryFirstOrDefaultAsync<AccountManageDTO>("GetAccountById", param: parameter, commandType: CommandType.StoredProcedure);
                return account;
            });
        }

        public async Task<IEnumerable<RoleDTO>> GetRole()
        {
            return await WithConnection(async connection =>
            {
                var roles = await connection.QueryAsync<RoleDTO>("GetRole", null, commandType: CommandType.StoredProcedure);
                return roles.ToList();
            });
        }

        public async Task<int> TotalAccount()
        {
            return await WithConnection(async connection =>
            {
                int totalAccont = (int) await connection.ExecuteScalarAsync("TotalAccount", null, commandType: CommandType.StoredProcedure);
                return totalAccont;
            });
        }

        public async void Update(AccountManageDTO account)
        {
            await WithConnection(async connection =>
            {
                var parameter = new DynamicParameters();
                parameter.Add("userId", account.UserId, DbType.Int32);
                parameter.Add("userName", account.UserName);
                parameter.Add("fullName", account.FullName);
                parameter.Add("email", account.Email);
                parameter.Add("phone", account.Phone);
                parameter.Add("address", account.Address);
                parameter.Add("timeStamp", account.TimeStamp);
                parameter.Add("dateOfBirth", account.DateOfBirth);
                parameter.Add("isActived", account.IsActived);
                parameter.Add("isBlocked", account.IsBlocked);
                parameter.Add("roleId", account.RoleId, DbType.Int32);
                await connection.ExecuteAsync("UpdateAccount", param: parameter, commandType: CommandType.StoredProcedure);
            });
        }
    }
}
