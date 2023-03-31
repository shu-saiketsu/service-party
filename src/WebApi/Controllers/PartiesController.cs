using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saiketsu.Service.Party.Application.Parties.Commands.CreateParty;
using Saiketsu.Service.Party.Application.Parties.Commands.DeleteParty;
using Saiketsu.Service.Party.Application.Parties.Queries.GetParties;
using Saiketsu.Service.Party.Application.Parties.Queries.GetParty;
using Saiketsu.Service.Party.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Saiketsu.Service.Party.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PartiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PartiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Retrieve all parties")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved parties successfully", typeof(List<PartyEntity>))]
    public async Task<IActionResult> Get()
    {
        var request = new GetPartiesQuery{  };
        var response = await _mediator.Send(request);

        return Ok(response);
    }


    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Retrieve a party")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved party successfully", typeof(PartyEntity))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Party does not exist")]
    public async Task<IActionResult> GetParty(int id)
    {
        var request = new GetPartyQuery { Id = id };
        var response = await _mediator.Send(request);

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new party")]
    [SwaggerResponse(StatusCodes.Status201Created, "Created party successfully", typeof(PartyEntity))]
    public async Task<IActionResult> CreateParty([FromBody] CreatePartyCommand command)
    {
        var response = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetParty), new { id = response.Id }, response);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Delete a party")]
    [SwaggerResponse(StatusCodes.Status200OK, "Deleted party successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Party does not exist")]
    public async Task<IActionResult> DeleteParty(int id)
    {
        var request = new DeletePartyCommand { Id = id };
        var response = await _mediator.Send(request);

        if (response)
            return Ok();

        return NotFound();
    }
}