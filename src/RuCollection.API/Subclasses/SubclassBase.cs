using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using UnityEngine;

namespace RuCollection.API.Subclasses
{
    public abstract class SubclassBase
    {
        protected bool _loaded;

        public abstract string Name { get; }

        public abstract RoleTypeId Role { get; }

        public virtual string Message { get; } = string.Empty;

        public virtual string Color { get; } = string.Empty;

        public virtual float Health { get; } = 100;

        public virtual (float Amount, float Limit, float Decay, float Efficacy, float sus, bool Persistent) Shield { get; } = (0, 75, 1.2f, 0.7f, 0, false);

        public virtual Inventory Inventory { get; } = new Inventory();

        public virtual bool Show { get; } = false;

        public virtual Vector3 Size { get; } = Vector3.zero;

        public virtual void Init(Player player)
        {
            if (!_loaded)
            {
                Subscribe();
            }

            Assign(player);

            _loaded = true;
        }

        public virtual void Destroy(Player player)
        {
            if (!_loaded)
            {
                return;
            }

            Deassign(player);

            Unsubscribe();

            _loaded = false;
        }

        public virtual void Assign(Player player)
        {
            if (Show)
            {
                SetInfo(player, true);
            }

            if (Size != Vector3.zero)
            {
                player.Scale = Size;
            }

            if (Message != string.Empty)
            {
                player.ShowHint($"<line-height=95%><size=95%><voffset=-20em><color={(string.IsNullOrEmpty(Color) ? Role.GetColor().ToHex() : Color)}>{Message}</color></size></voffset>", 15);
            }

            Timing.CallDelayed(0.0005f, delegate ()
            {
                player.AddAhp(Shield.Amount, Shield.Limit, Shield.Decay, Shield.Efficacy, Shield.sus, Shield.Persistent);

                player.MaxHealth = Health;
                player.Health = Health;
            });
        }

        public virtual void Deassign(Player player)
        {
            if (Show)
            {
                SetInfo(player, false);
            }

            if (Size != Vector3.zero)
            {
                player.Scale = Vector3.one;
            }
        }

        public virtual void Subscribe()
        {
            Exiled.Events.Handlers.Player.Left += OnPlayerLeft;
            Exiled.Events.Handlers.Player.Died += OnDied;
        }

        public virtual void Unsubscribe()
        {
            Exiled.Events.Handlers.Player.Left -= OnPlayerLeft;
            Exiled.Events.Handlers.Player.Died -= OnDied;
        }

        protected virtual void OnPlayerLeft(LeftEventArgs ev) => Deassign(ev.Player);

        protected virtual void OnDied(DiedEventArgs ev) => Deassign(ev.Player);

        protected void SetInfo(Player ply, bool status)
        {
            if (!status)
            {
                ply.CustomInfo = string.Empty;
                ply.InfoArea |= PlayerInfoArea.Role | PlayerInfoArea.Nickname;

                return;
            }

            ply.CustomInfo = $"{ply.CustomName}\n{Name}";
            ply.InfoArea &= ~(PlayerInfoArea.Role | PlayerInfoArea.Nickname);
        }
}
}
