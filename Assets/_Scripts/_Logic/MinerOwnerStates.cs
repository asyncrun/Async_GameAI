//------------------------------------------------------------------------
//
//  Name:		MinerOwnedStates.cs
//
//  Author:		DESKTOP-F4NMG0C\iamle
//
//  Date:		05/23/2017 22:14:12
//
//  Project:	Async_GameAI
//
//  Desc:   
//
//------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsyncRun.Core;

namespace AsyncRun.Logic
{
	public class EnterMineAndDigForNugget : IState<Miner>
    {
        public void Enter(Miner miner)
        {
            if(miner.LocationType != LocationType.GOLDMINE)
            {
                Debug.Log(miner.GetName() + " : " + "Walkin' to the goldmine");
                miner.LocationType = LocationType.GOLDMINE;
            }
        }

        public void Excute(Miner miner)
        {
            miner.AddToGoldCarried(1);
            miner.IncreaseFatigue();
            Debug.Log(miner.GetName() + " : " + "Pickin' up a nugget");

            if(miner.IsPocketsFull)
            {
                miner.GetFSM.ChangeState(miner.VisitBankAndDepositGold);
            }

            if(miner.IsThirsty)
            {
                miner.GetFSM.ChangeState(miner.QuenchThirst);
            }
        }

        public void Exit(Miner entity)
        {

        }

        public bool OnMessage(Miner entity, Telegram telegram)
        {
            return true;
        }
    }

	public class VisitBankAndDepositGold : IState<Miner>
    {
        public void Enter(Miner miner)
        {
            
        }

        public void Excute(Miner miner)
        {
           
        }

        public void Exit(Miner entity)
        {

        }

        public bool OnMessage(Miner entity, Telegram telegram)
        {
            return true;
        }
    }



    public class QuenchThirst : IState<Miner>
    {
        public void Enter(Miner miner)
        {

        }

        public void Excute(Miner miner)
        {

        }

        public void Exit(Miner entity)
        {

        }

        public bool OnMessage(Miner entity, Telegram telegram)
        {
            return true;
        }
    }
}