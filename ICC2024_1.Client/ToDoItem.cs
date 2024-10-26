namespace ICC2024_1.Client;

public class ToDoItem(string text, bool isCompleted): IIdentity
{
    public int Id { get; set; }
    public string Text { get; set; } = text;
    public bool IsCompleted { get; set; } = isCompleted;
}
