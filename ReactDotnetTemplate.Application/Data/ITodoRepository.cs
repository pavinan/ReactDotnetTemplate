using ReactDotnetTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactDotnetTemplate.Application.Data
{
    public interface ITodoRepository
    {
        Task<Todo> AddAsync(Todo todo);
        Task<bool> DeleteAsync(string id);
    }
}
