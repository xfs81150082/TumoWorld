﻿using ETModel;

namespace ETHotfix
{
	public static class MessageHelper
	{
        /// <summary>
        /// 广播 ， 发给所有的客户端
        /// </summary>
        /// <param name="message"></param>
		public static void Broadcast(IActorMessage message)
		{
			Unit[] units = Game.Scene.GetComponent<UnitComponent>().GetAll();
			ActorMessageSenderComponent actorLocationSenderComponent = Game.Scene.GetComponent<ActorMessageSenderComponent>();
			foreach (Unit unit in units)
			{
				UnitGateComponent unitGateComponent = unit.GetComponent<UnitGateComponent>();
				if (unitGateComponent.IsDisconnect)
				{
					continue;
				}

				ActorMessageSender actorMessageSender = actorLocationSenderComponent.Get(unitGateComponent.GateSessionActorId);
				actorMessageSender.Send(message);
			}
		}
       
        /// <summary>
        /// 后来增加群发 , 发给指定的客户端群
        /// </summary>
        /// <param name="message"></param>
        /// <param name="playerUnitId"></param>
        public static void Broadcast(IActorMessage message, long[] playerUnitIds)
        {
            foreach(long pi in playerUnitIds)
            {
                Broadcast(message, pi);
            }
        }

        /// <summary>
        /// 后来增加单发 , 发给指定的客户端
        /// </summary>
        /// <param name="message"></param>
        /// <param name="playerUnitId"></param>
        public static void Broadcast(IActorMessage message, long playerUnitId)
        {
            Unit unit = Game.Scene.GetComponent<UnitComponent>().Get(playerUnitId);
            if (unit.GetComponent<UnitGateComponent>().IsDisconnect || unit.GetComponent<UnitGateComponent>() == null)
            {
                return;
            }

            UnitGateComponent unitGateComponent = unit.GetComponent<UnitGateComponent>();
            ActorMessageSender actorMessageSender = Game.Scene.GetComponent<ActorMessageSenderComponent>().Get(unitGateComponent.GateSessionActorId);
            actorMessageSender.Send(message);
        }


    }
}