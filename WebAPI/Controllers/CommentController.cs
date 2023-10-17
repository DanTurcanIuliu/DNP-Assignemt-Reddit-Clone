using Application.Logic_Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentLogic commentLogic;

    public CommentsController(ICommentLogic commentLogic)
    {
        this.commentLogic = commentLogic;
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
}