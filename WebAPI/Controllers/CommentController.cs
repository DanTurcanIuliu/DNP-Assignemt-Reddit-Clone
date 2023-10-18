using Application.Logic_Interfaces;
using Application.Provider_Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentLogic commentLogic;
    private readonly ICommentProvider commentProvider;

    public CommentsController(ICommentLogic commentLogic, ICommentProvider commentProvider)
    {
        this.commentLogic = commentLogic;
        this.commentProvider = commentProvider;
    }
    
    [HttpPost]
    public async Task<ActionResult<Comment>> CreateAsync([FromBody]CommentCreateDto dto)
    {
        try
        {
            Comment created = await commentLogic.CreateAsync(dto);
            return Created($"/comments/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetAsync([FromQuery] string? userName, [FromQuery] int? userId,
        [FromQuery] string? postTitleContains, [FromQuery] string? bodyContains, [FromQuery] int? postId)
    {
        try
        {
            SearchCommentParametersDto parameters = new(userName, postTitleContains, bodyContains, userId, postId);
            var comments = await commentProvider.GetAsync(parameters);
            return Ok(comments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}