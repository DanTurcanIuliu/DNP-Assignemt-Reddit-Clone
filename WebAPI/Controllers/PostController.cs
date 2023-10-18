using Application.Logic_Interfaces;
using Application.Provider_Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostLogic postLogic;
    private readonly IPostProvider postProvider;

    public PostController(IPostLogic postLogic, IPostProvider postProvider)
    {
        this.postLogic = postLogic;
        this.postProvider = postProvider;
    }
    
    [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync([FromBody]PostCreateDto dto)
    {
        try
        {
            Post created = await postLogic.CreateAsync(dto);
            return Created($"/post/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] string? userName, [FromQuery] int? userId,
        [FromQuery] string? titleContains,  [FromQuery] string? bodyContains)
    {
        try
        {
            SearchPostParametersDto parameters = new(userName,  titleContains, bodyContains, userId);
            var posts = await postProvider.GetAsync(parameters);
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}