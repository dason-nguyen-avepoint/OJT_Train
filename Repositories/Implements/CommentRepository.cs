using Dapper;
using Data.Helpers;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Repositories.Implements
{
    public class CommentRepository:DapperBase,ICommentRepository
    {
        public async Task<IEnumerable<CommentDTO>> GetAllComment(int id)
        {
            return await WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ProductId", id, DbType.Int32);
                var listComment = await connection.QueryAsync<CommentDTO>(StoreProcedureComment.UspgetListComentProduct, parameters, commandType: CommandType.StoredProcedure);
                return listComment.ToList();
            });
        }
		public async void UspAddComment(int ProductId,int UserId, string CommentContent)
		{
			 await WithConnection(async connection =>
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("ProductId", ProductId);
                parameters.Add("UserId", UserId);
                parameters.Add("CommentContent", CommentContent);
				await connection.ExecuteAsync(StoreProcedureComment.UspAddComment, param: parameters, commandType: CommandType.StoredProcedure);
			});
		}
	}
}
