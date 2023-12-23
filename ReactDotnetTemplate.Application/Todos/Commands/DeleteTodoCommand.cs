using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactDotnetTemplate.Application.Todos.Commands
{
    public class DeleteTodoCommand : IRequest<bool>
    {
        public string? Id { get; set; }
    }
}
