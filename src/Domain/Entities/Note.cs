namespace Domain.Entities;

public class Note
{
    public Note(string content, Guid exerciseProgressId)
    {
        Id = Guid.NewGuid();
        Content = content;
        ExerciseProgressId = exerciseProgressId;
    }

    public Guid Id { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public Guid ExerciseProgressId { get; private set; }
    public ExerciseProgress? ExerciseProgress { get; private set; }

    public void Update(string newContent)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newContent);
        Content = newContent;
    }
}
