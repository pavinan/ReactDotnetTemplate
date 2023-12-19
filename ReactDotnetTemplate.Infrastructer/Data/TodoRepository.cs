using ReactDotnetTemplate.Application.Data;
using ReactDotnetTemplate.Models;
using ReactDotnetTemplate.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactDotnetTemplate.Infrastructure.Data
{
    public class TodoRepository(AppDbContext dbContext) : ITodoRepository
    {
        public async Task<Todo> AddAsync(Todo todo)
        {
            var result = await dbContext.Todos.AddAsync(todo);            

            return result.Entity;
        }

    }
}
