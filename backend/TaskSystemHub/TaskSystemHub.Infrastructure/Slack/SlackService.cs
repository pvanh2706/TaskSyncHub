using System.Text;
using System.Text.Json;

public class SlackService : ISlackService
{
    private readonly HttpClient _httpClient;

    public SlackService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SendMessageAsync(string message)
    {
        var payload = new
        {
            text = message
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var webhookUrl = "https://hooks.slack.com/services/xxx/yyy/zzz"; // Replace bằng webhook của bạn

        await _httpClient.PostAsync(webhookUrl, content);
    }
}
