namespace LevelSystem.API;

using System.Linq;
using Control.Handlers.Events.API.Serialization;
using Exiled.API.Features;

public static class API
{
    public static bool TryGetLog(string id, out PlayerLog log)
    {
        log = Plugin.Singleton.db.GetCollection<PlayerLog>("Players")?.FindById(id);
        return log != null;
    }

    public static void UpdateBadge(Player ply, string i = null, bool hidden = false)
    {
        try
        {
            if (i != null && i.Contains("\n"))
            {
                return;
            }
            if (ply == null || ply.UserId == null)
            {
                return;
            }
            var log = ply.GetLog();

            LevelSystem.API.Features.Badge badge = new LevelSystem.API.Features.Badge()
            {
                Color = "grey",
            };

            foreach (var kvp in Plugin.Singleton.Config.LevelsBadge.OrderBy(kvp => kvp.Key))
            {
                if (log.LVL > kvp.Key && Plugin.Singleton.Config.LevelsBadge.OrderByDescending(kvp2 => kvp2.Key).ElementAt(0).Key != kvp.Key)
                    continue;
                badge = new LevelSystem.API.Features.Badge()
                {
                    Color = $"{kvp.Value.Color}",
                };
                break;
            }

            Log.Info(hidden);

            string text = ply.Group == null || hidden
                ? $"{log.LVL} уровень"
                : $"{log.LVL} уровень | {ply.Group.BadgeText}";

            text += "\n";

            Log.Info(text);
            ply.RankName = text;
            ply.RankColor = badge.Color;
        }
        catch (System.Exception er)
        {
            Log.Error(er);
        }
    }
}