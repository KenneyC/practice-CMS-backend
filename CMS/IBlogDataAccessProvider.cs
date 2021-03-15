using System.Collections.Generic;
using System.Threading.Tasks;

namespace practice_CMS_backend.CMS
{
    public interface IBlogDataAccessProvider
    {
        Task<bool> AddBlog(PostModel post);
        Task<bool> DeleteBlog(int postId);

        List<PostModel> GetAllBlogs();

        PostModel GetBlog(int id);
    }
}