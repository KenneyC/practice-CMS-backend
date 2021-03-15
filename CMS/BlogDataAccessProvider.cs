using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace practice_CMS_backend.CMS
{
    public class BlogDataAccessProvider : IBlogDataAccessProvider
    {

        private readonly CMSDbContext _context;
        public BlogDataAccessProvider(CMSDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddBlog(PostModel post)
        {
            EntityEntry entity = await _context.posts.AddAsync(post);
            await _context.SaveChangesAsync();

            if (entity == null) return false;
            return true;
        }

        public async Task<bool> DeleteBlog(int id)
        {
            PostModel post = _context.posts.FirstOrDefault(post => post.Id == id);
            _context.posts.Remove(post);
            _context.SaveChanges();

            PostModel deleteCheck = _context.posts.FirstOrDefault(post => post.Id == id);

            if (deleteCheck != null) return false;
            return true;
        }

        public List<PostModel> GetAllBlogs()
        {
            return _context.posts.ToList<PostModel>();
        }

        public PostModel GetBlog(int id)
        {
            return _context.posts.FirstOrDefault<PostModel>(post => post.Id == id);
        }
    }

}