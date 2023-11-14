// using Application.Dao_Interfaces;
// using Application.Provider_Interfaces;
// using Shared.DTOs;
// using Shared.Models;
//
// namespace FileData.DAOs;
//
// public class UserFileDao : IUserDao, IUserProvider
// {
//     private readonly FileContext context;
//
//     public UserFileDao(FileContext context)
//     {
//         this.context = context;
//     }
//
//     public Task<User> CreateAsync(User user)
//     {
//         int userId = 1;
//         if (context.Users.Any())
//         {
//             userId = context.Users.Max(u => u.Id);
//             userId++;
//         }
//
//         user.Id = userId;
//
//         context.Users.Add(user);
//         context.SaveChanges();
//
//         return Task.FromResult(user);
//     }
//
//     public Task<User?> GetByUsernameAsync(string userName)
//     {
//         User? existing = context.Users.FirstOrDefault(u =>
//             u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
//         );
//         return Task.FromResult(existing);
//     }
//     
//     public Task<User?> GetByIdAsync(int id)
//     {
//         User? existing = context.Users.FirstOrDefault(u =>
//             u.Id == id
//         );
//         return Task.FromResult(existing);
//     }
//
//     public Task<User> ValidateUser(string username, string password)
//     {
//         User existingUser = context.Users.First(u => u.UserName.Equals(username));
//         
//         if (existingUser == null)
//         {
//             throw new Exception("User not found");
//         }
//
//         if (!existingUser.Password.Equals(password))
//         {
//             throw new Exception("Password mismatch");
//         }
//
//         return Task.FromResult(existingUser);
//     }
//
//     public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
//     {
//         IEnumerable<User> users = context.Users.AsEnumerable();
//         if (searchParameters.UsernameContains != null)
//         {
//             users = context.Users.Where(u => u.UserName.Contains(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
//         }
//
//         return Task.FromResult(users);
//     }
// }