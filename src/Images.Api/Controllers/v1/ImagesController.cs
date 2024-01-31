using Images.Application.Features.Images.Commands.CreateImage;
using Images.Application.Features.Images.Commands.DeleteImage;
using Images.Application.Features.Images.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Images.Api.Controllers.v1;
[ApiController]
[ApiVersion("1.0")]
[Route("/api/v1/[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ImagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize("Administrator")]
    [HttpGet]
    public async Task<List<ImageDto>> Get([FromQuery] ImagesQueryParameters queryParameters)
    {
        return await _mediator.Send(new GetImagesQuery(queryParameters));
    }

    [Authorize("Administrator")]
    [HttpGet("{id:guid}")]
    public async Task<ImageDto?> Get(Guid id)
    {
        return await _mediator.Send(new GetImagesByIdQuery(id));
    }

    [Authorize("Administrator")]
    [HttpGet("Download/{id:guid}")]
    public async Task<IActionResult> GetImage(Guid id)
    {
       var result = await _mediator.Send(new GetImagesByIdQuery(id));
       if (result == null)
       {
           return NotFound();
       }
       
       var imageBytes = Convert.FromBase64String(result.Content);
       var file = File(imageBytes, result.ContentType);
       return file;
    }

    [Authorize("Administrator")]
    [HttpPost]
    public async Task<IActionResult> CreateImage(CreateImagesCommand command)
    {
        if (command.Content.Length == 0)
        {
            return BadRequest("Image is required.");
        }
        
        var result = await _mediator.Send(command);
        
        return Ok($"Image {command.Content.FileName} - {result} received.");

    }

    [Authorize("Administrator")]
    [HttpDelete("{id:guid}")]
    public async Task<IResult> DeleteImage(Guid id)
    {
        await _mediator.Send(new DeleteImagesCommand(id));
        return Results.NoContent();
    }
}

