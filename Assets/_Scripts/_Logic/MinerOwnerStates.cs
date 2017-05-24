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
    //-----------------------------------------------class for EnterMineAndDigForNugget
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

        public void Exit(Miner miner)
        {
            Debug.Log(miner.GetName() 
                + " : "
                + "Ah'm leavin' the goldmine with mah pockets full o' sweet gold");
        }

        public bool OnMessage(Miner entity, Telegram telegram)
        {
            return false;
        }
    }


    //-----------------------------------------------class for VisitBankAndDepositGold
    public class VisitBankAndDepositGold : IState<Miner>
    {
        public void Enter(Miner miner)
        {
            if(miner.LocationType != LocationType.BANK)
            {
                Debug.Log(miner.GetName() + " : " + "Goin' to the bank. Yes siree");
                miner.LocationType = LocationType.BANK;
            }
        }

        public void Excute(Miner miner)
        {
            miner.AddToWealth(miner.GoldCarried);
            miner.SetGoldCarried(0);

            Debug.Log(miner.GetName()
                 + " : "
                 + "Depositing gold. Total savings now: "
                 + miner.Wealth);

            if(miner.IsComfort)
            {
                Debug.Log(miner.GetName()
                + " : "
                + "WooHoo! Rich enough for now. Back home to mah li'lle lady");

                miner.GetFSM.ChangeState(miner.GoHomeAndSleepTilRested);
            }
            else
            {
                miner.GetFSM.ChangeState(miner.EnterMineAndDigForNugget);
            }
        }

        public void Exit(Miner miner)
        {
            Debug.Log(miner.GetName()
                             + " : "
                             + "Leavin' the bank");
        }

        public bool OnMessage(Miner miner, Telegram telegram)
        {
            return false;
        }
    }

    //-----------------------------------------------class for GoHomeAndSleepTilRested
    public class GoHomeAndSleepTilRested : IState<Miner>
    {
        public void Enter(Miner miner)
        {
            if (miner.LocationType != LocationType.SHACK)
            {
                Debug.Log(miner.GetName() + " : " + "Walkin' home");
                miner.LocationType = LocationType.SHACK;

                //let the wife know I'm home
                DispatchMessage.Self.DispatchMsg((int)EntityType.MINER,
                    (int)EntityType.ELSA,
                    DispatchMessage.SendMessageImmediately,
                    MessageTypes.Msg_HiHoneyImHome,
                    DispatchMessage.NoAdditionInfo);
            }
        }

        public void Excute(Miner miner)
        {
            if(!miner.IsFatigue)
            {
                Debug.Log(miner.GetName() 
                    + " : " 
                    + "All mah fatigue has drained away. Time to find more gold!");

                miner.GetFSM.ChangeState(miner.EnterMineAndDigForNugget);
            }
            else
            {
                miner.DecreaseFatigue();
                Debug.Log(miner.GetName()
                    + " : "
                    + "ZZZZZ...");
            }
        }

        public void Exit(Miner miner)
        {

        }

        public bool OnMessage(Miner miner, Telegram msg)
        {
            switch(msg.MessageType)
            {
                case MessageTypes.Msg_StewReady:
                    Debug.Log("Message handled by"
                        + miner.GetName()
                        + " at time : "
                        + System.DateTime.Now);

                    Debug.Log(miner.GetName()
                        + ": Okay Hun, ahm a comin'!");

                    miner.GetFSM.ChangeState(miner.EatStew);
                    return true;
            }
            return true;
        }
    }

    //-----------------------------------------------class for VisitBankAndDepositGold
    public class QuenchThirst : IState<Miner>
    {
        public void Enter(Miner miner)
        {
            if (miner.LocationType != LocationType.SALOON)
            {
                Debug.Log(miner.GetName() 
                    + " : " 
                    + "Boy, ah sure is thusty! Walking to the saloon");

                miner.LocationType = LocationType.SALOON;
            }
        }

        public void Excute(Miner miner)
        {
            miner.BuyAndDrinkAWhiskey();

            Debug.Log(miner.GetName()
                    + " : "
                    + "That's mighty fine sippin' liquer");

            miner.GetFSM.ChangeState(miner.EnterMineAndDigForNugget);
        }

        public void Exit(Miner miner)
        {
            Debug.Log(miner.GetName()
                   + " : "
                   + "Leaving the saloon, feelin' good");

            miner.LocationType = LocationType.SALOON;
        }

        public bool OnMessage(Miner miner, Telegram msg)
        {
            return false;
        }
    }

    //-----------------------------------------------class for EatStew
    public class EatStew : IState<Miner>
    {
        public void Enter(Miner miner)
        {
            Debug.Log(miner.GetName()
                   + " : "
                   + "mells Reaaal goood Elsa!");
        }

        public void Excute(Miner miner)
        {
            Debug.Log(miner.GetName()
                   + " : "
                   + "Tastes real good too!");

            miner.GetFSM.RevertToPreviousState();
        }

        public void Exit(Miner miner)
        {
            Debug.Log(miner.GetName()
                   + " : "
                   + "Thankya li'lle lady. Ah better get back to whatever ah wuz doin'");
        }

        public bool OnMessage(Miner miner, Telegram msg)
        {
            return false;
        }
    }
}