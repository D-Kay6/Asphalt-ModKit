﻿/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on April 6, 2018
 * ------------------------------------
 **/

using Asphalt.Api.Event.PlayerEvents;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Players;
using Eco.Gameplay.Stats.ConcretePlayerActions;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Localization;
using Eco.Shared.Services;
using System;

namespace Asphalt.Api.Event.Player
{
    /**
     * Called when a player sends a chat message;
     * */
    public class PlayerSendMessageEvent : MessageAction, ICancellable, IEvent
    {
        private bool cancel = false;

        public User User { get; protected set; }

        public ChatMessage Message { get; protected set; }

        public PlayerSendMessageEvent(User user, ChatMessage message) : base()
        {
            this.User = user;
            this.Message = message;
        }

        public bool IsCancelled()
        {
            return this.cancel;
        }

        public void SetCancelled(bool cancel)
        {
            this.cancel = cancel;
        }
    }

    public class PlayerSendMessageEventHelper
    {
        public IAtomicAction CreateAtomicAction(User user, ChatMessage message)
        {
            PlayerSendMessageEvent psme = new PlayerSendMessageEvent(user, message);
            IEvent psmeEvent = psme;

            EventManager.CallEvent(ref psmeEvent);

            if (!psme.IsCancelled())
                return CreateAtomicAction_original(user, message);

            return new FailedAtomicAction(new LocString());
        }

        public IAtomicAction CreateAtomicAction_original(User user, ChatMessage message)
        {
            throw new InvalidOperationException();
        }
    }
}