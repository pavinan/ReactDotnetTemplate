using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactDotnetTemplate.Application.Todos.Queries
{
    public class GetTodosQuery : IRequest<List<TodoDTO>>
    {
    }


    public class TodoDTO
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

}
