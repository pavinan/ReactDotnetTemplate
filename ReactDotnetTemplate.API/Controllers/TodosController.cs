using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReactDotnetTemplate.Application.Todos.Commands;
using ReactDotnetTemplate.Application.Todos.Queries;

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

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            var todo = await mediator.Send(new DeleteTodoCommand { Id = id });

            return NoContent();
        }
    }
}
