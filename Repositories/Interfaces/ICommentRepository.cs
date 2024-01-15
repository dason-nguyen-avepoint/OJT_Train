using Repositories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<CommentDTO>> GetAllComment(int id);
        void UspAddComment(int ProductId, int UserId, string CommentContent);

	}
}
