using ReactDotnetTemplate.Application.Services.AppEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactDotnetTemplate.Application.AppEvents
{
    public class TodoAddedAppEvent : AppEvent
    {
        public string TodoId { get; set; }

        public TodoAddedAppEvent(string todoId)
        {
            TodoId = todoId;
        }
    }
}
