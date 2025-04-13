public interface ISlackService
{
    Task SendMessageAsync(string message);
}
