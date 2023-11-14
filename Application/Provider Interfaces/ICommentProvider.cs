using Shared.DTOs;
using Shared.Models;

namespace Application.Provider_Interfaces;

public interface ICommentProvider
{
    Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto searchParameters);
}