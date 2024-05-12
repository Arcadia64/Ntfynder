using System.Threading.Tasks;
using Dalamud.Logging;
using Flurl;
using Flurl.Http;

namespace Ntfy.Delivery;

public static class NtfyDelivery
{
    public static void Deliver(string title, string text = "")
    {
        Task.Run(() => DeliverAsync(title, text));
    }

    private static async Task DeliverAsync(string title, string text)
    {
        // Construct the URL dynamically using the NtfyTopic from the settings
        var ntfyUrl = Plugin.Configuration.NtfyServer.AppendPathSegment(Plugin.Configuration.NtfyTopic);

        // Assuming `title` is used as the main message
        // Since ntfy doesn't have a separate title field, you can prepend it to the message or just send the message
        var message = string.IsNullOrEmpty(title) ? text : $"{title}: {text}";

        try
        {
           // Send a simple text message to the specified ntfy topic with additional headers
            await ntfyUrl
                .WithHeaders(new
                {
                    Priority = Plugin.Configuration.NtfyPriority,
                })
                .PostStringAsync(message);
        }
        catch (FlurlHttpException e)
        {
            PluginLog.Error($"Failed to send ntfy notification: '{e.Message}'");
            PluginLog.Error($"{e.StackTrace}");
        }
    }
}
