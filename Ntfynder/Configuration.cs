using Dalamud.Configuration;
using Dalamud.Plugin;
using System;

namespace Ntfy
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 1;

        public bool EnableForDutyPops { get; set; } = true;
        public bool IgnoreAfkStatus { get; set; } = false;

        // Optional: Specify a ntfy server location
        public string NtfyServer { get; set; } = "https://ntfy.sh";
        // Optional: Specify the ntfy topic if needed
        public string NtfyTopic { get; set; } = "";
        // Optional: Specify ntfy notification priority. Defaults to 5 (Max).
        public int NtfyPriority { get; set; } = 5;

        [NonSerialized]
        private DalamudPluginInterface? PluginInterface;

        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            this.PluginInterface = pluginInterface;
        }

        public void Save()
        {
            this.PluginInterface!.SavePluginConfig(this);
        }
    }
}
