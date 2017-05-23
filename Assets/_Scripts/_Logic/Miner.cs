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
        private const int ComfortLevel = 5;
        private const int MaxNuggets = 3;
        private const int ThirstLevel = 5;
        private const int TirednessThreshold = 5;

        private StateMachine<Miner> _minerStateMachine;
        private EnterMineAndDigForNugget _enterMineAndDigForNugget;
        public EnterMineAndDigForNugget EnterMineAndDigForNugget
        {
            get
            {
                if(_enterMineAndDigForNugget == null)
                {
                    _enterMineAndDigForNugget = new EnterMineAndDigForNugget();
                }
                return _enterMineAndDigForNugget;
            }
        }
        
        private VisitBankAndDepositGold _visitBankAndDepositGold;
        public VisitBankAndDepositGold VisitBankAndDepositGold
        {
            get
            {
                if (_visitBankAndDepositGold == null)
                {
                    _visitBankAndDepositGold = new VisitBankAndDepositGold();
                }
                return _visitBankAndDepositGold;
            }
        }

        private QuenchThirst _quenchThirst;
        public QuenchThirst QuenchThirst
        {
            get
            {
                if (_quenchThirst == null)
                {
                    _quenchThirst = new QuenchThirst();
                }
                return _quenchThirst;
            }
        }


        private LocationType _locationType = LocationType.SHACK;
        public LocationType LocationType
        {
            get { return _locationType; }
            set { _locationType = value; }
        }

        //挖了金矿
        private int _goldCarried;
        public int GoldCarried
        {
            get { return _goldCarried; }
        }

        //存款
        private int _moneyInBank;

        //口渴
        private int _thirst;
        public bool IsThirsty
        {
            get
            {
                if (_thirst > ThirstLevel)
                {
                    return true;
                }

                return false;
            }
        }

        //疲劳度
        private int _fatigue;
        public bool IsFatigue
        {
            get
            {
                if (_fatigue > TirednessThreshold)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsPocketsFull
        {
            get
            {
                return _goldCarried >= MaxNuggets;
            }
        }

        public StateMachine<Miner> GetFSM
        {
            get { return _minerStateMachine; }
        }

        public Miner(int id) : base(id)
	    {
            _minerStateMachine = new StateMachine<Miner>(this);

            _minerStateMachine.CurrentState = EnterMineAndDigForNugget;
        }

        public void AddToGoldCarried(int val)
        {
            _goldCarried += val;
            if(_goldCarried <0)
            {
                _goldCarried = 0;
            }
        }

        public void AddToWealth(int val)
        {
            _moneyInBank += val;
            if(_moneyInBank <0)
            {
                _moneyInBank = 0;
            }
        }

        public void BuyAndDrinkAWhiskey()
        {
            _thirst = 0;
            _moneyInBank -= 2;
        }
        
	    public override void Update()
	    {
            _thirst += 1;
            _minerStateMachine.Update();
        }

        public override bool HandleMessage(Telegram msg)
        {
            return _minerStateMachine.HandleMessage(msg);
        }

        public override string GetName()
        {
            return ((EntityType)Id).ToString();
        }

        public void DecreaseFatigue()
        {
            _fatigue -= 1;
        }

        public void IncreaseFatigue()
        {
            _fatigue += 1;
        }
    }
}