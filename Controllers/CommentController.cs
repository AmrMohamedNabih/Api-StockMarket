using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiStockMarket.DTO.Comment;
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
        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository commentRepo , IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var CommentsDto = comments.Select(c => c.ToCommentDto());

            return Ok(CommentsDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute]int stockId,[FromBody] CreateCommentDto comment)
        {
            if(!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock does not exists");
            }
            var commentModel = comment.ToCommentFromCreate(stockId);
            var Result = await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById) , new {Id = commentModel.Id} , commentModel.ToCommentDto());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id , [FromBody] UpdateCommentDto comment)
        {
            var commentModel = await _commentRepo.UpdateAsync(id , comment.ToCommentFromUpdate());
            return commentModel != null ? Ok(commentModel.ToCommentDto()) : NotFound("comment not found");
        }
    }
}