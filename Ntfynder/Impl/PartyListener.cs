using System.Linq;
using Dalamud.Game.ClientState.Party;
using Dalamud.Logging;
using Lumina.Excel.GeneratedSheets;
using Ntfy.Delivery;
using Ntfy.Util;

namespace Ntfy.Impl;

public static class PartyListener
{
    public static void On()
    {
        Service.PluginLog.Debug("PartyListener On");
        CrossWorldPartyListSystem.OnJoin += OnJoin;
        CrossWorldPartyListSystem.OnLeave += OnLeave;
    }
    
    public static void Off()
    {
        Service.PluginLog.Debug("PartyListener Off");
        CrossWorldPartyListSystem.OnJoin -= OnJoin;
        CrossWorldPartyListSystem.OnLeave -= OnLeave;
    }

    private static void OnJoin(CrossWorldPartyListSystem.CrossWorldMember m)
    {
        if (!CharacterUtil.IsClientAfk()) return;

        var jobAbbr = LuminaDataUtil.GetJobAbbreviation(m.JobId);

        if (m.PartyCount == 8)
        {
            NtfyDelivery.Deliver("Party full",
                                     $"{m.Name} (Lv{m.Level} {jobAbbr}) joins the party.\nParty recruitment ended. All spots have been filled.");
        }
        else
        {
            NtfyDelivery.Deliver($"{m.PartyCount}/8: Party join",
                                     $"{m.Name} (Lv{m.Level} {jobAbbr}) joins the party.");
        }
    }
    
    private static void OnLeave(CrossWorldPartyListSystem.CrossWorldMember m)
    {
        if (!CharacterUtil.IsClientAfk()) return;
        
        var jobAbbr = LuminaDataUtil.GetJobAbbreviation(m.JobId);

        NtfyDelivery.Deliver($"{m.PartyCount-1}/8: Party leave",
                                 $"{m.Name} (Lv{m.Level} {jobAbbr}) has left the party.");
    }
}
