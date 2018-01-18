//------------------------------------------------------------------------
//
//  Name:		Entity.cs
//
//  Author:		LIWEI\Administrator
//
//  Date:		05/23/2017 19:11:56
//
//  Project:	Async_GameAI
//
//  Desc:   
//
//------------------------------------------------------------------------

using UnityEngine;
using System;

namespace AsyncRun.Core
{
    public class Entity : MonoBehaviour
    {
        private static int _nextValidId;
        private int _id;

        public int Id
        {
            get { return _id; }
        }
        
        private void SetId(int id)
        {
            Debug.Assert(id >= _nextValidId, "<Entity::SetId> : invalid ID");
            _id = id;
            _nextValidId = _id + 1;
        }

        public virtual void Excute() { }

        public virtual bool HandleMessage(Telegram msg)
        {
            return false;
        }
    }
}