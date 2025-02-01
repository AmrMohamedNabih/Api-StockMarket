using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiStockMarket.DTO.Comment;
using ApiStockMarket.Extensions;
using ApiStockMarket.Interfaces;
using ApiStockMarket.Mappers;
using ApiStockMarket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiStockMarket.Controllers
{
    [ApiController]
    [Route("comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly  UserManager<AppUser> _userManager;

        public CommentController(ICommentRepository commentRepo , IStockRepository stockRepo , UserManager<AppUser> userManager) 
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await _commentRepo.GetAllAsync();
            var CommentsDto = comments.Select(c => c.ToCommentDto());

            return Ok(CommentsDto);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute]int stockId,[FromBody] CreateCommentDto comment)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock does not exists");
            }
            var userName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(userName);
            var commentModel = comment.ToCommentFromCreate(stockId);
            commentModel.AppUserId = appUser.Id;
            var Result = await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById) , new {Id = commentModel.Id} , commentModel.ToCommentDto());
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id , [FromBody] UpdateCommentDto comment)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var commentModel = await _commentRepo.UpdateAsync(id , comment.ToCommentFromUpdate());
            return commentModel != null ? Ok(commentModel.ToCommentDto()) : NotFound("comment not found");
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var commentModel = await _commentRepo.DeleteAsync(id);
            return commentModel? NoContent() : NotFound("comment not found");
        }
    }
}