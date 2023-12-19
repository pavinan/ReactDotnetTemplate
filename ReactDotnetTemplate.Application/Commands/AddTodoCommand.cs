﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactDotnetTemplate.Application.Commands
{
    public class AddTodoCommand : IRequest<bool>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}