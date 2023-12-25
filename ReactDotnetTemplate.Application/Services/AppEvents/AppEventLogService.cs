using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<AppEventLog>> RetrieveEventLogsPendingToPublishAsync(string transactionId)
        {
            var result = await dbContext.Set<AppEventLog>()
                .Where(e => e.TransactionId == transactionId && e.State == AppEventStateEnum.NotPublished)
                .ToListAsync();

            return result;
        }

        public Task MarkEventAsPublishedAsync(string id)
        {
            return UpdateEventStatus(id, AppEventStateEnum.Published);
        }

        public Task MarkEventAsInProgressAsync(string id)
        {
            return UpdateEventStatus(id, AppEventStateEnum.InProgress);
        }

        public Task MarkEventAsFailedAsync(string id)
        {
            return UpdateEventStatus(id, AppEventStateEnum.PublishedFailed);
        }

        private Task UpdateEventStatus(string id, AppEventStateEnum status)
        {
            var eventLogEntry = dbContext.Set<AppEventLog>().Single(ie => ie.Id == id);
            eventLogEntry.State = status;

            if (status == AppEventStateEnum.InProgress)
                eventLogEntry.TimesSent++;

            return dbContext.SaveChangesAsync();
        }


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
