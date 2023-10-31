using Shared.DTOs;
using Shared.Models;

namespace Application.Provider_Interfaces;

public interface IUserProvider
{
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
}