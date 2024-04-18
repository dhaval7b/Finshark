using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Comment;
using api.Dtos.Comment;
using api.Mapper;
using api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository repository, IStockRepository stockRepo)
        {
            _commentRepo = repository;
            _stockRepo = stockRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var comments =await _commentRepo.GetAll();
            if(comments == null)
            {
                return NotFound();
            }
            var commentDTOs = comments.Select(s => s.TocommentDTO());
            return Ok(comments);
        }

         [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var comment =await _commentRepo.GetById(id);
            if(comment == null)
            {
                return NotFound();
            }
            return Ok(comment.TocommentDTO());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId,[FromBody] CreateCommentDto commentDTO)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            if(!await _stockRepo.DoesStockExist(stockId))
            {
                return BadRequest("Stock does not exist");
            }
            var comment =await _commentRepo.Create(commentDTO.ToCommentFromCreate(stockId));
            if(comment == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetById), new { id = comment.Id}, comment.TocommentDTO());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDTO updateCommentDTO)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var Comment = await _commentRepo.Update(id, updateCommentDTO.ToCommentFromUpdate());
            if(Comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(Comment.TocommentDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commentRepo.Delete(id);
            if(comment == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}