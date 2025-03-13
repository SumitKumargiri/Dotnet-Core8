using App.EnglishBuddy.Application.Features.DeleteEmployee;
using App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;
using App.EnglishBuddy.Application.Features.UserFeatures.GetUser;
using App.EnglishBuddy.Application.Features.UserFeatures.SaveEmployee;
using App.EnglishBuddy.Application.Features.UserFeatures.UpdateEmployee;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.EnglishBuddy.API.Controllers
{
    [ApiController]
    [Route("employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<GetEmployeeResponse>> Get(Guid id, CancellationToken cancellationToken)
        {
            var request = new GetEmployeeRequest { Id = id };
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }


        [HttpGet("list")]
        public async Task<ActionResult<List<GetAllEmployeeResponse>>> GetAll(GetAllEmployeeRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }


        [HttpPost("create")]
        public async Task<ActionResult<SaveEmployeeResponse>> Create(SaveEmployeeRequest request,CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<UpdateEmployeeResponse>> Update(Guid id, [FromBody] UpdateEmployeeRequest request, CancellationToken cancellationToken)
        {
            request.Id = id; 
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<DeleteEmployeeResponse>> Delete(Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteEmployeeRequest { Id = id };
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }


    }

}