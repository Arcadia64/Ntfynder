using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using Ntfy.Delivery;
using Ntfy.Util;

namespace Ntfy.Windows;

public class ConfigWindow : Window, IDisposable
{
    private Configuration Configuration;
    
    public ConfigWindow(Plugin plugin) : base(
        "Ntfy Configuration",
        ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar |
        ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.AlwaysAutoResize)
    {
        Configuration = Plugin.Configuration;
    }

    public void Dispose() { }

    private TimedBool notifSentMessageTimer = new(3.0f);

    public override void Draw()
    {
        {
            var cfg = Configuration.NtfyServer;
            if (ImGui.InputText("Ntfy Server", ref cfg, 2048u))
            {
                Configuration.NtfyServer = cfg;
            }
        }
        {
            var cfg = Configuration.NtfyTopic;
            if (ImGui.InputText("Ntfy Topic", ref cfg, 2048u))
            {
                Configuration.NtfyTopic = cfg;
            }
        }
        {
            var cfg = Configuration.NtfyPriority;
            string[] ntfyPriorityOptions = [
                "5 (Max)",
                "4 (High)",
                "3 (Default)",
                "2 (Low)",
                "1 (Min)",
            ];
            var listBoxHeight = ImGui.GetTextLineHeightWithSpacing() * ntfyPriorityOptions.Length;
            if (ImGui.BeginListBox("Ntfy Notification Priority", new Vector2(0, listBoxHeight)))
            {
                for (var i = 0; i < ntfyPriorityOptions.Length; i++)
                {
                    // Calculating the selected priority from ntfyPriorityOptions index. It's not
                    // very elegant but ntfy's priority options seem stable enough that it should
                    // not cause problems in the future.
                    var currentPriority = ntfyPriorityOptions.Length - i;
                    if (ImGui.Selectable(ntfyPriorityOptions[i], cfg == currentPriority))
                    {
                        Configuration.NtfyPriority = currentPriority;
                    }
                }
                ImGui.EndListBox();
            }
        }
        {
            var cfg = Configuration.EnableForDutyPops;
            if (ImGui.Checkbox("Send message for duty pop?", ref cfg))
            {
                Configuration.EnableForDutyPops = cfg;
            }
        }

        if (ImGui.Button("Send test notification"))
        {
            notifSentMessageTimer.Start();
            NtfyDelivery.Deliver("Test notification", 
                                     "If you received this, Ntfy is configured correctly.");
        }

        if (notifSentMessageTimer.Value)
        {
            ImGui.SameLine();
            ImGui.Text("Notification sent!");
        }

        {
            var cfg = Configuration.IgnoreAfkStatus;
            if (ImGui.Checkbox("Ignore AFK status and always notify", ref cfg))
            {
                Configuration.IgnoreAfkStatus = cfg;
            }
        }

        if (!Configuration.IgnoreAfkStatus)
        {
            if (!CharacterUtil.IsClientAfk())
            {
                var red = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
                ImGui.TextColored(red, "This plugin will only function while your client is AFK (/afk, red icon)!");

                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.Text("The reasoning for this is that if you are not AFK, you are assumed to");
                    ImGui.Text("be at your computer, and ready to respond to a join or a duty pop.");
                    ImGui.Text("Notifications would be bothersome, so they are disabled.");
                    ImGui.EndTooltip();
                }
            }
            else
            {
                var green = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
                ImGui.TextColored(green, "You are AFK. The plugin is active and notifications will be served.");
            }
        }

        if (ImGui.Button("Save and close"))
        {
            Configuration.Save();
            IsOpen = false;
        }
    }
}
