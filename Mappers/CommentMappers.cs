using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiStockMarket.DTO.Comment;
using ApiStockMarket.Models;

namespace ApiStockMarket.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto{
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId,
                Stock = comment.Stock
            };
        }
        public static Comment ToCommentFromCreate(this CreateCommentDto comment ,int StockId)
        {
            return new Comment{
                Title = comment.Title,
                Content = comment.Content,
                StockId = StockId,
            };
        }
        public static Comment ToCommentFromUpdate(this UpdateCommentDto comment)
        {
            return new Comment{
                Title = comment.Title,
                Content = comment.Content,
            };
        }
    }
}