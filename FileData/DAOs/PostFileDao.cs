// using Application.Dao_Interfaces;
// using Application.Provider_Interfaces;
// using Shared.DTOs;
// using Shared.Models;
//
// namespace FileData.DAOs;
//
// public class PostFileDao:IPostDao, IPostProvider
// {
//     private readonly FileContext context;
//
//     public PostFileDao(FileContext context)
//     {
//         this.context = context;
//     }
//
//     public Task<Post> CreateAsync(Post post)
//     {
//         int id = 1;
//         if (context.Posts.Any())
//         {
//             id = context.Posts.Max(t => t.Id);
//             id++;
//         }
//
//         post.Id = id;
//         
//         context.Posts.Add(post);
//         context.SaveChanges();
//
//         return Task.FromResult(post);
//     }
//
//     public Task<Post?> GetByIdAsync(int dtoPostId)
//     {
//         Post? existing = context.Posts.FirstOrDefault(p =>
//             p.Id == dtoPostId
//         );
//         return Task.FromResult(existing);
//     }
//     
//     public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParams)
//     {
//         IEnumerable<Post> result = context.Posts.AsEnumerable();
//
//         if (!string.IsNullOrEmpty(searchParams.Username))
//         {
//             result = context.Posts.Where(post =>
//                 post.User.UserName.Equals(searchParams.Username, StringComparison.OrdinalIgnoreCase));
//         }
//
//         if (searchParams.UserId != null)
//         {
//             result = result.Where(t => t.User.Id == searchParams.UserId);
//         }
//
//         if (!string.IsNullOrEmpty(searchParams.TitleContains))
//         {
//             result = result.Where(t =>
//                 t.Title.Contains(searchParams.TitleContains, StringComparison.OrdinalIgnoreCase));
//         }
//         
//         if (!string.IsNullOrEmpty(searchParams.BodyContains))
//         {
//             result = result.Where(t =>
//                 t.Body.Contains(searchParams.BodyContains, StringComparison.OrdinalIgnoreCase));
//         }
//
//         return Task.FromResult(result);
//     }
// }