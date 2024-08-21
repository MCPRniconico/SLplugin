using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Exiled.API.Enums;
using Exiled.API.Interfaces;
using Exiled.Events.Handlers;
using Exiled.Events.EventArgs.Player;

namespace PainkillerPlugin
{
    public class Plugin : Exiled.API.Features.Plugin<Config>
    {
        public override string Author => "KUYU1013";
        public override string Name => "PainkillerPlugin";
        public override string Prefix => "painkillerplugin";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(5, 0, 0);

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.UsedItem += OnUsedItem;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.UsedItem -= OnUsedItem;
            base.OnDisabled();
        }

        private void OnUsedItem(UsedItemEventArgs ev)
        {
            // Use correct type comparison
            if (ev.Item.Type == ItemType.Painkillers)
            {
                Log.Info($"{ev.Player.Nickname} used Painkillers!");

                float deathChance = (float)new Random().NextDouble();
                if (deathChance <= Config.DeathChance)
                {
                    ev.Player.Kill("鎮痛剤の副作用");
                    return;
                }

                float boostChance = (float)new Random().NextDouble();
                if (boostChance <= Config.BoostChance)
                {
                    ApplyMovementBoost(ev.Player, Config.BoostAmount);
                    Log.Info($"{ev.Player.Nickname} received a movement boost of {Config.BoostAmount * 100}%.");
                }
            }
        }

        private void ApplyMovementBoost(Exiled.API.Features.Player player, float boostAmount)
        {
            // Implement speed boost logic according to your API's capabilities
            // Example: Adjust player's speed with a hypothetical method
            // player.MovementSpeed *= (1 + boostAmount); // Placeholder; replace with actual method
        }
    }

    public class Config : IConfig
    {
        public float BoostAmount { get; set; } = 0.05f;
        public float DeathChance { get; set; } = 0.3f;
        public float BoostChance { get; set; } = 0.25f;

        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}
