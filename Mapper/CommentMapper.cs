using System;
using System.Collections.Generic;
using System.Linq;
using api.DTO.Comment;
using api.Dtos.Comment;
using api.Models;

namespace api.Mapper
{
    public static class CommentMapper
    {
         public static CommentDTO TocommentDTO(this Comment comment)
         {
            return new CommentDTO
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
         }

        public static Comment ToCommentFromCreate(this CreateCommentDto createCommentDTO, int  stockId)
         {
            return new Comment
            {
            
                Title = createCommentDTO.Title,
                Content = createCommentDTO.Content,
                StockId = stockId
            };
         }

        public static Comment ToCommentFromUpdate(this UpdateCommentDTO updateCommentDTO)
         {
            return new Comment
            {
            
                Title = updateCommentDTO.Title,
                Content = updateCommentDTO.Content,
            };
         }

         public static Comment TocommentModel(this CommentDTO comment)
         {
            return new Comment
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
         }
    }
}