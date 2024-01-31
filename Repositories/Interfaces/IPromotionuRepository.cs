using Repositories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
	public interface IPromotionuRepository
	{
		Task<Promotionu> UspGetPromotionu(string coupon);
		Task<IEnumerable<PromotionDTO>> GetPromo(int pageNumber, int pageSize);
		Task<int> TotalCoupon();
		Task<PromotionDTO> CheckValidCoupon(string code);
		void Update(int id, string code, int value);
        void Delete(int id);
    }
}
