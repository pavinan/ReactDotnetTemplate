﻿using MediatR;
using ReactDotnetTemplate.Application.AppEvents;
using ReactDotnetTemplate.Application.Data;
using ReactDotnetTemplate.Application.Services.AppEvents;
using ReactDotnetTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactDotnetTemplate.Application.Todos.Commands
{
    public class AddTodoCommandHandler(
        ITodoRepository todoRepository,
        IAppEventLogService appEventLogService
        ) : IRequestHandler<AddTodoCommand, bool>
    {
        public async Task<bool> Handle(AddTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = new Todo
            {
                Id = Guid.NewGuid().ToString(),
                Title = request.Title,
                Description = request.Description,
                CreatedAt = DateTimeOffset.Now
            };

            await appEventLogService.SaveEventAsync(new TodoAddedAppEvent(todo.Id));

            await todoRepository.AddAsync(todo);

            return true;
        }
    }
}
