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

        public async Task<bool> DeleteAsync(string id)
        {
            var todo = await dbContext.Todos.FindAsync(id);

            if (todo == null)
            {
                return false;
            }

            dbContext.Todos.Remove(todo);

            return true;
        }

    }
}
