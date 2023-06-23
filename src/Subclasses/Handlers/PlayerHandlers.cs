using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using RuCollection.API.Subclasses;
using RuCollection.API.Subclasses.Group;
using RuCollection.API.Subclasses.Single;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Subclasses.Handlers
{
    internal sealed class PlayerHandlers
    {
        private static IReadOnlyDictionary<SubclassBase, int> _subclasses;
        private static List<Player> _hasSubclass;

        public PlayerHandlers()
        {
            if (_subclasses != null)
            {
                return;
            }

            _hasSubclass = new (100);

            _subclasses = new Dictionary<SubclassBase, int>(8)
            {
                // D-Boys

                { Pickpocket.Singleton, 30 },
                { Veteran.Singleton, 24 },
                { Blatnoy.Singleton, 12 },
                { LittleD.Singleton, 11 },
                { BigD.Singleton, 11 },
                { Godfather.Singleton, 10 },
                { Thief.Singleton, 8 },
                { Killer.Singleton, 6 },
                { Scp181.Singleton, 4 },
                { Scp343.Singleton, 1 },

                // Scientists

                { Friendly.Singleton, 25 },
                { Medic.Singleton, 15 },
                { Lead.Singleton, 10 },
                { Engineer.Singleton, 9 },
                { Manager.Singleton, 4 },
            };
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (!ev.IsAllowed || ev.NewRole is RoleTypeId.None or RoleTypeId.Spectator or RoleTypeId.Overwatch or RoleTypeId.Filmmaker || ev.Reason is not SpawnReason.Respawn and not SpawnReason.RoundStart and not SpawnReason.LateJoin and not SpawnReason.Revived || _hasSubclass.Contains(ev.Player))
            {
                return;
            }

            foreach (var subclass in _subclasses.Where(x => x.Key.Role == ev.NewRole))
            {
                if (Random.Range(0, 101) <= subclass.Value)
                {
                    subclass.Key.Init(ev.Player);

                    _hasSubclass.Add(ev.Player);

                    var items = subclass.Key.Inventory.Items;

                    if (items == null || items.Count == 0 || ev.Items == null)
                    {
                        break;
                    }

                    ev.Items.Clear();

                    ev.Items.AddRange(items);

                    break;
                }
            }
        }

        public void OnDied(DiedEventArgs ev)
        {
            if (!_hasSubclass.Contains(ev.Player))
            {
                return;
            }

            foreach (var subclass in _subclasses.Where(x => x.Key.Role == ev.TargetOldRole))
            {
                var single = subclass.Key as SingleSubclass;
                var group = subclass.Key as GroupSubclass;

                if (single == null && group == null)
                {
                    continue;
                }

                if (group == null && single.Player == ev.Player)
                {
                    subclass.Key.Destroy(ev.Player);

                    continue;
                }

                if (single == null && group.Players.Contains(ev.Player))
                {
                    if (group.Players.Count == 1)
                    {
                        subclass.Key.Destroy(ev.Player);

                        continue;
                    }

                    subclass.Key.Deassign(ev.Player);

                    continue;
                }
           }
        }
    }
}
