//------------------------------------------------------------------------
//
//  Name:		DispatchMessage.cs
//
//  Author:		LIWEI\Administrator
//
//  Date:		05/23/2017 19:04:11
//
//  Project:	Async_GameAI
//
//  Desc:   
//
//------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace AsyncRun.Core
{
    public class DispatchMessage : Singleton<DispatchMessage>
    {
        private readonly List<Telegram> _telegramList = new List<Telegram>();

        public void Discharge(Entity receiver, Telegram telegram)
        {
            if (!receiver.HandleMessage(telegram))
            {
                Debug.LogError("Message not handled");
            }
        }

        public void DispatchMsg(int senderId, int receiverId, float delayTime, MessageTypes messageType,
            object extraInfo)
        {
            Entity sender = EntityManager.Self.GetEntity(senderId);
            Entity receiver = EntityManager.Self.GetEntity(receiverId);

            if (receiver == null)
            {
                Debug.LogWarning("No Receiver with Id of " + receiverId + " found");
                return;
            }

            Telegram telegram = new Telegram(senderId, receiverId, 0, messageType, extraInfo);
            if (delayTime <= 0.0f)
            {
                Discharge(receiver, telegram);
            }
            else
            {
                float currentTime = Time.time;
                telegram.DispatchTime = currentTime + delayTime;
                _telegramList.Add(telegram);
                //默认升序排列
                _telegramList.Sort();
            }
        }

        //需要每帧调用
        public void DispatchDelayedMessage()
        {
            float currentTime = Time.time;
            while (_telegramList.Count > 0
                   && _telegramList[0].DispatchTime > 0
                   && _telegramList[0].DispatchTime < currentTime)
            {
                Telegram telegram = _telegramList[0];
                Entity reveiver = EntityManager.Self.GetEntity(telegram.ReceiverId);
                Discharge(reveiver, telegram);
                _telegramList.Remove(telegram);
            }
        }
    }
}
