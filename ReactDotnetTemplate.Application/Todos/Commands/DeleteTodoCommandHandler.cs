using MediatR;
using ReactDotnetTemplate.Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactDotnetTemplate.Application.Todos.Commands
{
    public class DeleteTodoCommandHandler(ITodoRepository todoRepository) : IRequestHandler<DeleteTodoCommand, bool>
    {
        public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            return await todoRepository.DeleteAsync(request.Id!);
        }
    }
}
