using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiStockMarket.Interfaces;
using ApiStockMarket.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace ApiStockMarket.Controllers
{
    [ApiController]
    [Route("comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        public CommentController(ICommentRepository commentRepo )
        {
            _commentRepo = commentRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var CommentsDto = comments.Select(c => c.ToCommentDto());

            return Ok(CommentsDto);
        }
    }
}