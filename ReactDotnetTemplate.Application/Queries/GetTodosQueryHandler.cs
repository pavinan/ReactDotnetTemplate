using MediatR;
using Microsoft.EntityFrameworkCore;
using ReactDotnetTemplate.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactDotnetTemplate.Application.Queries
{
    internal class GetTodosQueryHandler(AppDbContext appDbContext, ILogger<GetTodosQueryHandler> logger)
        : IRequestHandler<GetTodosQuery, List<TodoDTO>>
    {
        public async Task<List<TodoDTO>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            var todos = await appDbContext.Todos.ToListAsync(cancellationToken);

            return todos.Select(todo => new TodoDTO
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description
            }).ToList();
        }
    }
}
