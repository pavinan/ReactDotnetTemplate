using ReactDotnetTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactDotnetTemplate.Application.Services.AppEvents
{
    public interface IAppEventLogService
    {
        Task MarkEventAsFailedAsync(string id);
        Task MarkEventAsInProgressAsync(string id);
        Task MarkEventAsPublishedAsync(string id);
        Task<IEnumerable<AppEventLog>> RetrieveEventLogsPendingToPublishAsync(string transactionId);
        Task SaveEventAsync(AppEvent @event);
    }
}
