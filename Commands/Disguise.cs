using CommandSystem;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disguise.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Disguise : ICommand
    {
        public string Command { get; } = "disguise";

        public string[] Aliases { get; } = { "d" };

        public string Description { get; } = "disguise as another role";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if(sender.CheckPermission("disguise"))
            {
                string[] argumentsArray = arguments.ToArray();
                RoleType roleToChangeTo;
                try
                {
                    roleToChangeTo = (RoleType)Enum.Parse(typeof(RoleType), argumentsArray[0]);
                }
                catch
                {
                    response = "Parse error! Make sure you've typed the role correctly. (List of all avalible roles on github)";
                    return true;
                }

                Player player = Player.Get((sender as PlayerCommandSender).ReferenceHub);

                foreach(Player p in Player.List.Where((Player x) => x != player))
                    p.SendFakeSyncVar(player.ReferenceHub.networkIdentity, typeof(CharacterClassManager), "NetworkCurClass", (sbyte)roleToChangeTo);

                if (!Plugin.instance.playerDisguised.ContainsKey(player))
                    Plugin.instance.playerDisguised.Add(player, roleToChangeTo);
                else
                    Plugin.instance.playerDisguised[player] = roleToChangeTo;

                response = "Success!";
                return false;
            }
            else
            {
                response = "Insufficient permissions.";
                return true;
            }
        }
    }
}
