//------------------------------------------------------------------------
//
//  Name:		Telegram.cs
//
//  Author:		LIWEI\Administrator
//
//  Date:		05/23/2017 19:11:11
//
//  Project:	Async_GameAI
//
//  Desc:   
//
//------------------------------------------------------------------------

using System;
using System.Text;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace AsyncRun.Core
{
    public class Telegram : IComparable
    {
        public int SenderId = -1;
        public int ReceiverId = -1;
        public float DispatchTime = -1;
        public MessageTypes MessageType = MessageTypes.MsgNull;
        public object ExtraInfo;
		 
        public const float SmallestDelay = 0.25f;


        public Telegram() { }

        public Telegram(int senderId, int receiverId, float delayTime, MessageTypes message, object extraInfo)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            DispatchTime = delayTime;
            MessageType = message;
            ExtraInfo = extraInfo;
        }

        public static bool operator ==(Telegram t1, Telegram t2)
        {
            return (Mathf.Abs(t1.DispatchTime - t2.DispatchTime) < SmallestDelay
                    && t1.SenderId == t2.SenderId
                    && t1.ReceiverId == t2.ReceiverId
                    && t1.MessageType == t2.MessageType);
        }

        public static bool operator !=(Telegram t1, Telegram t2)
        {
            return !(t1 == t2);
        }

        public static bool operator <(Telegram t1, Telegram t2)
        {
            if (t1 == t2)
            {
                return false;
            }
            return (t1.DispatchTime < t2.DispatchTime);
        }

        public static bool operator >(Telegram t1, Telegram t2)
        {
            if (t1 == t2)
            {
                return false;
            }
            return (t1.DispatchTime > t2.DispatchTime);
        }

        public override bool Equals(object o)
        { 
            if(o == null || !(o is Telegram))
            {
                return false;
            }
            else
            {
                return this == (Telegram)o;
            }
        }

        public override int GetHashCode()
        {
            return GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("time : {0}  sender: {1}  receiver: {2}  msg: {3}", DispatchTime, SenderId, ReceiverId, MessageType);
            return strBuilder.ToString();
        }

        public int CompareTo(object obj)
        {
            int result = 0;
            Telegram telegram = obj as Telegram;
            if (this > telegram)
            {
                result = 1;
            }
            else if (this < telegram)
            {
                result = -1;
            }
            else
            {
                result = 0;
            }
            return result;
        }
    }
}


