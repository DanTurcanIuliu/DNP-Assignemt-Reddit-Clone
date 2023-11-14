using Shared.DTOs;
using Shared.Models;

namespace Application.Provider_Interfaces;

public interface IPostProvider
{
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters);
}