using ReactDotnetTemplate.Models;
using ReactDotnetTemplate.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReactDotnetTemplate.Application.Services.AppEvents
{
    public class AppEventLogService(AppDbContext dbContext) : IAppEventLogService
    {
        private static readonly JsonSerializerOptions s_indentedOptions = new() { WriteIndented = true };
        private static readonly JsonSerializerOptions s_caseInsensitiveOptions = new() { PropertyNameCaseInsensitive = true };

        public async Task SaveEventAsync(AppEvent @event)
        {
            var appEvent = new AppEventLog
            {
                Id = @event.Id,
                CreatedAt = @event.CreatedAt,
                EventType = @event.GetType().Name,
                Content = JsonSerializer.Serialize(@event, @event.GetType(), s_indentedOptions),
                State = AppEventStateEnum.NotPublished,
                TimesSent = 0,
                TransactionId = dbContext.GetCurrentTransaction()?.TransactionId.ToString()
            };

            await dbContext.Set<AppEventLog>().AddAsync(appEvent);

            await dbContext.SaveChangesAsync();
        }
    }
}
