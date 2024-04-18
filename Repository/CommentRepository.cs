using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAll(){
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetById(int id){
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment> Create(Comment comment){
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> Update(int id, Comment comment){
            Comment? existingComment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if(existingComment == null) return null;
            existingComment.Title = comment.Title;
            existingComment.Content = comment.Content;
            await _context.SaveChangesAsync();
            return existingComment;
        }

        public async Task<Comment?> Delete(int id){
            Comment? comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if(comment == null) return null;
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

    }
}