using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Dto
{
    public class CommentDTO
    {
        public int CommentID { get; set; }

        public string? CommentContent { get; set; }
        public DateTime CreatedDate {  get; set; } 
        public string? UserName { get; set; }

		public int ProductID { get; set; }

		public int UserID { get; set; }
	}
}
