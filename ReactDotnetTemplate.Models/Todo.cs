namespace ReactDotnetTemplate.Models
{
    public class Todo
    {
        public string? Id { get; set; }        
        public bool IsCompleted { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
