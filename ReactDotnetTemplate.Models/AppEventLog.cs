using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactDotnetTemplate.Models
{
    public class AppEventLog
    {
        public string? Id { get; set; }
        public string? EventType { get; set; }
        public string? Content { get; set; }
        public AppEventStateEnum State { get; set; }
        public int TimesSent { get; set; }
        public string? TransactionId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }

    public enum AppEventStateEnum
    {
        NotPublished = 0,
        InProgress = 1,
        Published = 2,
        PublishedFailed = 3
    }
}
