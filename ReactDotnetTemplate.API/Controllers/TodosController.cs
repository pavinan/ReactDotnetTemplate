using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReactDotnetTemplate.Application.Commands;
using ReactDotnetTemplate.Application.Queries;

namespace ReactDotnetTemplate.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodosController (IMediator mediator) : ControllerBase
    {   

        [HttpGet]
        public async Task<ActionResult<List<TodoDTO>>> Get()
        {
            var todos = await mediator.Send(new GetTodosQuery());

            return Ok(todos);
        }

        [HttpPost]
        public async Task<ActionResult<TodoDTO>> Post(AddTodoCommand command)
        {
            var todo = await mediator.Send(command);

            return NoContent();
        }
    }
}
