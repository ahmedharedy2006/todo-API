public record TaskDto
{
    public string Title { get; init; }
    public string Description { get; init; }
    public bool IsCompleted { get; init; }

    public int ListId { get; init; }
}
