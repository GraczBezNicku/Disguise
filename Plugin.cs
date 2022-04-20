using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disguise
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin instance;
        public EventsHandler eventsHandler;

        public Dictionary<Player, RoleType> playerDisguised = new Dictionary<Player, RoleType>();

        public override void OnEnabled()
        {
            instance = this;
            eventsHandler = new EventsHandler();

            Exiled.Events.Handlers.Player.Verified += eventsHandler.OnVerified;
            Exiled.Events.Handlers.Player.Dying += eventsHandler.OnDeath;
            Exiled.Events.Handlers.Player.Left += eventsHandler.OnLeft;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Verified -= eventsHandler.OnVerified;
            Exiled.Events.Handlers.Player.Dying -= eventsHandler.OnDeath;
            Exiled.Events.Handlers.Player.Left -= eventsHandler.OnLeft;

            instance = null;
            eventsHandler = null;

            base.OnDisabled();
        }
    }
}
