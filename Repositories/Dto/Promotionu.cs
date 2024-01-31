using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Dto
{
	public class Promotionu
	{
		public string? Promotioncode { get; set; }	
		public int Promotionvalue { get; set; }
	}
	public class PromotionDTO
	{
        public int PromotionId { get; set; }
        public string? PromotionCode { get; set; }
        public int PromotionValue { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
