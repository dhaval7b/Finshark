using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repository.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAll();
        public Task<Comment?> GetById(int id);

        public Task<Comment> Create(Comment comment);

        public Task<Comment?> Update(int id, Comment comment);

        public Task<Comment?> Delete(int id);

    }
}