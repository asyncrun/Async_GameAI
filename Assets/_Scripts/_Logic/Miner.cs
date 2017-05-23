//------------------------------------------------------------------------
//
//  Name:		Miner.cs
//
//  Author:		LIWEI\Administrator
//
//  Date:		05/23/2017 19:27:03
//
//  Project:	Async_GameAI
//
//  Desc:   
//
//------------------------------------------------------------------------
using AsyncRun.Core;
using UnityEngine;

namespace AsyncRun.Logic
{
	public class Miner : Entity
	{
        private StateMachine<Miner> _minerStateMachine;

	    public Miner(int id) : base(id)
	    {
            _minerStateMachine = new StateMachine<Miner>(this);
	    }

	    public override void Update()
	    {
	        Debug.Log(1);
	    }

        public override bool HandleMessage(Telegram msg)
        {
            return true;
        }
	}
}