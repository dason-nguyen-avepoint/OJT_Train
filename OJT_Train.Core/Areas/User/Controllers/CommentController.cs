using Microsoft.AspNetCore.Mvc;
using OJT_Train.Core.Areas.User.Models;
using Repositories.Dto;
using Repositories.Implements;
using Repositories.Interfaces;

namespace OJT_Train.Core.Areas.User.Controllers
{
    [Area("User")]
    public class CommentController : Controller
    {

        private readonly ICommentRepository _commentRepository;
 
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository= commentRepository;
        }
        [HttpPost]
        public IActionResult AddComment(IFormCollection formconlection)
        {
            
            var commentcontent = formconlection["inputComment"];
            var  userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
            var productId = Convert.ToInt32(formconlection["productId"]);

            _commentRepository.UspAddComment(productId, userId,  commentcontent);

			return RedirectToAction("ViewProductDetail","Product", new { id = productId });
        }
    }
}
    