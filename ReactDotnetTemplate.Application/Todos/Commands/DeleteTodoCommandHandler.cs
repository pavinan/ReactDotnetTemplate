using MediatR;
using ReactDotnetTemplate.Application.AppEvents;
using ReactDotnetTemplate.Application.Data;
using ReactDotnetTemplate.Application.Services.AppEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactDotnetTemplate.Application.Todos.Commands
{
    public class DeleteTodoCommandHandler(
        ITodoRepository todoRepository,
        IAppEventLogService appEventLogService
        ) : IRequestHandler<DeleteTodoCommand, bool>
    {
        public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            await appEventLogService.SaveEventAsync(new TodoDeletedAppEvent(request.Id!));

            return await todoRepository.DeleteAsync(request.Id!);
        }
    }
}
