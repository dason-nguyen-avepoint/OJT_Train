using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Repositories.Interfaces;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace OJT_Train.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PromotionController : Controller
    {
        private readonly IPromotionuRepository _repo;
        public PromotionController(IPromotionuRepository repo)
        {
            _repo = repo;
        }
        private async Task<IActionResult> CheckAdminAccess()
        {
            if (HttpContext.Session.GetString("RoleName") != "Admin")
            {
                return RedirectToAction("Unauthorized", "Error");
            }

            return null;
        }
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var accessResult = await CheckAdminAccess();
            if (accessResult != null)
            {
                return accessResult;
            }
            int pageSize = 3;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPage = (int)Math.Ceiling((await _repo.TotalCoupon()) / (double)pageSize);
            var promotions = await _repo.GetPromo(pageNumber, pageSize);
            return View(promotions);
        }
        public async Task<IActionResult> Editpromotion(int id, string code, int value)
        {
            var promo = await _repo.CheckValidCoupon(code);
            if (promo != null && promo.PromotionId != id)
            {
                var response = new { error = true, message = "Mã code đã tồn tại! Vui lòng thử lại", id = id, code = code, value = value };
                return Json(response);
            }
            _repo.Update(id, code, value);
            return Json(new { error = false });
        }
        [HttpPut]
        public IActionResult DeletePromotion([FromBody] int id)
        {
            _repo.Delete(id);
            return Json(new { success = true });
        }
    }
}
