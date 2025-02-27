using Dalamud.Utility;
using Lumina.Excel.GeneratedSheets;
using Ntfy.Delivery;
using Ntfy.Util;

namespace Ntfy.Impl;

public class DutyListener
{
    public static void On()
    {
        Service.PluginLog.Debug("DutyListener On");
        Service.ClientState.CfPop += OnDutyPop;
    }

    public static void Off()
    {
        Service.PluginLog.Debug("DutyListener Off");
        Service.ClientState.CfPop -= OnDutyPop;
    }

    private static void OnDutyPop(ContentFinderCondition e)
    {
        if (!Plugin.Configuration.EnableForDutyPops)
            return;

        if (!CharacterUtil.IsClientAfk())
            return;
        
        var dutyName = e.RowId == 0 ? "Duty Roulette" : e.Name.ToDalamudString().TextValue;
        NtfyDelivery.Deliver($"Duty pop", $"Duty registered: '{dutyName}'.");
    }
}
