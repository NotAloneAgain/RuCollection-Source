using Exiled.API.Features;
using Control.Handlers.Events.API.Serialization;

namespace LevelSystem.API;

public static class Extensions
{
    public static PlayerLog GetLog(this Player ply)
    {
        PlayerLog toInsert = null;
        if (!API.TryGetLog(ply.UserId, out var log))
        {
            toInsert = new PlayerLog()
            {
                ID = ply.UserId,
                LVL = 0,
                XP = 0,
                Nickname = ply.Nickname,
                DNT = ply.DoNotTrack,
            };
            Plugin.Singleton.db.GetCollection<PlayerLog>("Players").Insert(toInsert);
        }

        if (log is null)
            return toInsert;

        PlayerLog updateData = new PlayerLog()
        {
            ID = ply.UserId,
            LVL = log.LVL,
            XP = log.XP,
            Nickname = ply.Nickname,
            DNT = ply.DoNotTrack,
        };
        Plugin.Singleton.db.GetCollection<PlayerLog>("Players").Update(updateData);

        return log;
    }

    public static void UpdateLog(this PlayerLog log)
    {
        Plugin.Singleton.db.GetCollection<PlayerLog>("Players").Update(log);
    }

    public static void AddXP(this PlayerLog log, int amount)
    {
        log.XP += amount;
        Player ply = Player.Get(log.ID);

        int XPPerLevel = Plugin.Singleton.Config.XPPerLevel + (Plugin.Singleton.Config.XPPerNewLevel * log.LVL);

        int lvlsGained = log.XP / XPPerLevel;
        if (lvlsGained > 0)
        {
            log.LVL += lvlsGained;
            log.XP -= lvlsGained * XPPerLevel;
            if (Plugin.Singleton.Config.ShowAddedLVL && ply != null)
            {
                //Control.Extensions.HintExtensions.XPHintQueue.Add((ply, Plugin.Singleton.Config.AddedLVLHint
                    //.Replace("%level%", log.LVL.ToString()), 12f));
            }

            ply.RankName = "";
        }

        else if (Plugin.Singleton.Config.ShowAddedXP && ply != null)
        {
            //Control.Extensions.HintExtensions.XPHintQueue.Add((ply, $"+ <color=green>{amount}</color> XP", 12f));
        }
        log.UpdateLog();
    }
}
