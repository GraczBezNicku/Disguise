using Exiled.API.Extensions;
using Exiled.API.Features;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disguise.Patches
{
    [HarmonyPatch(typeof(Radio), nameof(Radio.UserCode_CmdSyncVoiceChatStatus))]
    public static class SpeechPatch
    {
        public static bool Prefix(Radio __instance, bool b)
        {
            __instance.Network_syncPrimaryVoicechatButton = b;
            __instance._dissonanceSetup.SpectatorChat = (b && __instance._hub.characterClassManager.CurClass == RoleType.Spectator);

            if(Plugin.instance.playerDisguised.ContainsKey(Player.Get(__instance._hub)))
                __instance._dissonanceSetup.SCPChat = (b && Plugin.instance.playerDisguised[Player.Get(__instance._hub)].GetTeam() == Team.SCP);
            else
                __instance._dissonanceSetup.SCPChat = (b && __instance._hub.characterClassManager.CurRole.team == Team.SCP);

            return false;
        }
    }
}
