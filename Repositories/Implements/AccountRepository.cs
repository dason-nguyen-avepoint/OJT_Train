using Dapper;
using Data.Helpers;
using Microsoft.VisualBasic;
using Repositories.Const;
using Repositories.DataAccess;
using Repositories.Dto;
using Repositories.Interfaces;
using System.Data;


namespace Repositories.Implements
{
     public class AccountRepository:DapperBase,IAccountRepository
    {
        public async Task<LoginDTO?> getUserforLogin(LoginDTO loginDTO)
        {
            return await WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                var jInput = JsonDeserializeHelper.SerializeObjectForDb(loginDTO);
                parameters.Add("@JInput", jInput, DbType.String, ParameterDirection.Input);
                var userlogin = await connection.QuerySingleOrDefaultAsync<LoginDTO>(StoreProcedureAccount.UspLogin, param: parameters, commandType: CommandType.StoredProcedure);
                return userlogin;
            });
        }
	
		public async Task<int> UpsRegistration(AccountDTO model)
        {
            return await WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                var jInput = JsonDeserializeHelper.SerializeObjectForDb(model);
                parameters.Add("@JInput", jInput, DbType.String, ParameterDirection.Input);
                int check = await connection.ExecuteScalarAsync<int>(StoreProcedureAccount.UpsRegistration, param: parameters, commandType: CommandType.StoredProcedure);
                return check;
            });
        }
        
        public async Task<int> UspContact(ContactDTO model)
        {
            return await WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                var jInput = JsonDeserializeHelper.SerializeObjectForDb(model);
                parameters.Add("@JInput", jInput, DbType.String, ParameterDirection.Input);
                int check = await connection.ExecuteAsync(StoreProcedureAccount.UspContactInfor, param: parameters, commandType: CommandType.StoredProcedure);
                return check;
            }
            );
        }

        public async Task<ForgotPasswordDTO?> UspForgetPassword(ForgotPasswordDTO model)
        {
            return await WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                var jInput = JsonDeserializeHelper.SerializeObjectForDb(model);
                parameters.Add("@JInput", jInput, DbType.String, ParameterDirection.Input);
                var result = await connection.QuerySingleOrDefaultAsync<ForgotPasswordDTO>(StoreProcedureAccount.UspForgetPassword, parameters, commandType: CommandType.StoredProcedure);
                return result;
            });
        }

        public async Task<AccountDTO?> UspGetProfile(int? id)
        {
			return await WithConnection(async connection =>
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("UserId", id, DbType.Int32);
				var result = await connection.QuerySingleOrDefaultAsync<AccountDTO>(StoreProcedureAccount.UspGetProfile, param: parameters, commandType: CommandType.StoredProcedure);
                return result;
			});	
		}
		public async void UpdateProfile(AccountDTO model)
		{
			await WithConnection(async connection =>
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("UserId", model.UserID);
				parameters.Add("Fullname", model.FullName);
				parameters.Add("Phone", model.Phone);
				parameters.Add("Address", model.Address);
				parameters.Add("DateOfBirth", model.DateOfBirth);
				await connection.ExecuteAsync(StoreProcedureAccount.UspUpdateInformation, param: parameters, commandType: CommandType.StoredProcedure);
			});
		}
        public async Task<int> ChangePassword(AccountDTO model, string repassword)
        {
            return await WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("UserId", model.UserID, DbType.Int32);
                parameters.Add("Password", model.Password, DbType.String);
                parameters.Add("Repassword", repassword, DbType.String);
                int check = await connection.ExecuteScalarAsync<int>(StoreProcedureAccount.UspPassword, param: parameters, commandType: CommandType.StoredProcedure);
                return check;
            });
        }
        public async Task ActivedAccount(int? id)
		{
			await WithConnection(async connection =>
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("UserId", id);
				await connection.ExecuteAsync(StoreProcedureAccount.UspUpdateActiveAccount, param: parameters, commandType: CommandType.StoredProcedure);
			});
		}

		public async Task<int> Uspgetuseridbyemail(AccountDTO model)
		{
			return await WithConnection(async connection =>
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("Email", model.Email);
				int check = await connection.ExecuteScalarAsync<int>(StoreProcedureAccount.Uspgetuseridbyemail, param: parameters, commandType: CommandType.StoredProcedure);
				return check;
			});
		}


	}
}
