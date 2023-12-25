using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactDotnetTemplate.Application.Services.AppEvents
{
    public class AppEvent
    {
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public AppEvent() 
        {
            Id = Ulid.NewUlid().ToString();
            CreatedAt = DateTime.UtcNow;
        }

    }
}
