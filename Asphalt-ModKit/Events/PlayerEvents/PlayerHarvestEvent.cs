﻿using Asphalt.Events;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Simulation.Agents;
using System;

namespace Asphalt.Api.Event.PlayerEvents
{
    public class PlayerHarvestEvent : CancellableEvent
    {
        public Player Player { get; set; }

        public Organism Target { get; set; }

        public PlayerHarvestEvent(Player pPlayer, Organism pTarget) : base()
        {
            this.Player = pPlayer;
            this.Target = pTarget;
        }
    }

    internal class PlayerHarvestEventHelper
    {
        public static bool Prefix(Player player, Organism target, ref IAtomicAction __result)
        {
            PlayerHarvestEvent cEvent = new PlayerHarvestEvent(player, target);
            IEvent iEvent = cEvent;

            EventManager.CallEvent(ref iEvent);

            if (cEvent.IsCancelled())
            {
                __result = new FailedAtomicAction(new LocString());
                return false;
            }

            return true;
        }
    }
}
