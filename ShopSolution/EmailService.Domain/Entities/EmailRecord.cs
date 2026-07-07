namespace EmailService.Domain.Entities;

public class EmailRecord
{
    public int Id { get; private set; }
    public string To { get; private set; } = string.Empty;
    public string Subject { get; private set; } = string.Empty;
    public string Body { get; private set; } = string.Empty;
    public DateTime SentAt { get; private set; } = DateTime.UtcNow;

    private EmailRecord() { }

    public EmailRecord(string to, string subject, string body)
    {
        To = to;
        Subject = subject;
        Body = body;
    }
}
