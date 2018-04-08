﻿/** 
* ------------------------------------
* Copyright (c) 2018 [Kronox]
* See LICENSE file in the project root for full license information.
* ------------------------------------
* Created by Kronox on March 29, 2018
* ------------------------------------
**/

using Asphalt.Api.Event;
using Asphalt.Api.Event.PlayerEvents;
using Asphalt.Api.Util;
using Eco.Core.Plugins.Interfaces;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Utils;
using System;
using System.Reflection;
using System.Security.Principal;
using System.Threading;

namespace Asphalt.Api
{
    public class Asphalt : IModKitPlugin, IServerPlugin
    {
        public static bool IsInitialized { get; protected set; }

        public Asphalt()
        {
            /*
            Injection.Install(
                typeof(ChatManager).GetMethod("SendChat", BindingFlags.Instance | BindingFlags.NonPublic),
                typeof(PlayerSendMessageEventHelper).GetMethod("SendChat", BindingFlags.Instance | BindingFlags.NonPublic),
                typeof(PlayerSendMessageEventHelper).GetMethod("SendChat_original", BindingFlags.Instance | BindingFlags.NonPublic)
                );
                */

            if (!IsAdministrator)
                Log.WriteError("If Asphalt is not working, try running Eco as Administrator!");


            Injection.InstallCreateAtomicAction(typeof(CraftPlayerActionManager), typeof(PlayerCraftEventHelper));
            Injection.InstallCreateAtomicAction(typeof(GainSkillPlayerActionManager), typeof(PlayerGainSkillEventHelper));

            Injection.InstallCreateAtomicAction(typeof(SellPlayerActionManager), typeof(PlayerSellEventHelper));
            Injection.InstallCreateAtomicAction(typeof(MessagePlayerActionManager), typeof(PlayerSendMessageEventHelper));
    
            Injection.InstallCreateAtomicAction(typeof(PolluteAirPlayerActionManager), typeof(WorldPolluteEventHelper));
       

            Injection.Install(
                    typeof(InteractionExtensions).GetMethod("MakeContext", BindingFlags.Static | BindingFlags.Public),
                    typeof(PlayerInteractEventHelper).GetMethod("MakeContext", BindingFlags.Static | BindingFlags.Public),
                    typeof(PlayerInteractEventHelper).GetMethod("MakeContext_original", BindingFlags.Static | BindingFlags.Public)
                 );

            Injection.Install(
                    typeof(User).GetMethod("Login", BindingFlags.Instance | BindingFlags.Public),
                    typeof(PlayerLoginEventHelper).GetMethod("Login", BindingFlags.Instance | BindingFlags.Public),
                    typeof(PlayerLoginEventHelper).GetMethod("Login_original", BindingFlags.Instance | BindingFlags.Public)
                  );

            IsInitialized = true;
        }


        public static bool IsAdministrator => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        public string GetStatus()
        {
            return IsInitialized ? "Complete!" : "Initializing...";
        }

        public override string ToString()
        {
            return "Asphalt ModKit";
        }
    }
}
