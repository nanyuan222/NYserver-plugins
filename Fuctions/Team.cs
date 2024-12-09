using Exiled.API.Features;
using System.Collections.Generic;
using System.Linq;

namespace NYPlugin.Fuctions
{
    public class Team
    {
        public static void RegisterEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStartedTeammates;
        }
        public static void UnregisterEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStartedTeammates;
        }
        private static void OnRoundStartedTeammates()
        {
            DisplayTeammates();
        }
        private static void DisplayTeammates()
        {
            foreach (var player in Player.List)
            {
                List<string> teammates = Player.List
                    .Where(p => p.Role.Team == player.Role.Team && p != player)
                    .Select(p => p.Nickname)
                    .ToList();

                if (teammates.Count > 0 && player.IsHuman)
                {
                    player.ShowHint(string.Format(Plugin.Instance.Config.Team, string.Join("\n", teammates)), 9999f);
                }
                else
                {
                }
            }
        }
    }
}

