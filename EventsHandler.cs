using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Interactables.Interobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disguise
{
    public class EventsHandler
    {
        public void OnVerified(VerifiedEventArgs ev)
        {
            if(Plugin.instance.playerDisguised.Count != 0)
            {
                foreach(Player playerDisguised in Plugin.instance.playerDisguised.Keys)
                    ev.Player.SendFakeSyncVar(playerDisguised.ReferenceHub.networkIdentity, typeof(CharacterClassManager), "NetworkCurClass", (sbyte)Plugin.instance.playerDisguised[playerDisguised]);
            }
        }

        public void OnDeath(DyingEventArgs ev)
        {
            if(Plugin.instance.playerDisguised.ContainsKey(ev.Target))
                Plugin.instance.playerDisguised.Remove(ev.Target);
        }

        public void OnLeft(LeftEventArgs ev)
        {
            if(Plugin.instance.playerDisguised.ContainsKey(ev.Player))
                Plugin.instance.playerDisguised.Remove(ev.Player);
        }
    }
}
