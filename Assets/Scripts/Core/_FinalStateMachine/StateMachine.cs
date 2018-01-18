//------------------------------------------------------------------------
//
//  Name:		StateMachine.cs
//
//  Author:		LIWEI\Administrator
//
//  Date:		05/23/2017 19:50:10
//
//  Project:	Async_GameAI
//
//  Desc:   
//
//------------------------------------------------------------------------

using System.Diagnostics;
using UnityEngine;

namespace AsyncRun.Core
{
	public class StateMachine<T> where T:Entity
	{
	    private readonly T _owner;

        private IState<T> _previousState;
        public IState<T> PreviousState
        {
            get { return _previousState; }
        }

        private IState<T> _currentState;
        public IState<T> CurrentState
	    {
	        get { return _currentState; }
            set { _currentState = value; }
	    }

        private IState<T> _globalState;
        public IState<T> GlobalState
        {
            get { return _globalState; }
        }


        public string GetCurrentStateName
        {
            get { return _currentState.GetType().Name; }
        }


        public StateMachine(T owner)
        {
            _owner = owner;
        }

	    public void ChangeState(IState<T> newState)
	    {
            UnityEngine.Debug.Assert(newState != null, "<StateMachine.ChangeState>:: trying to assign null state to current.");

	        _previousState = _currentState;

            _currentState.Exit(_owner);
	        _currentState = newState;
            _currentState.Enter(_owner);
	    }

        public void Excute()
        {
            if(_globalState != null)
            {
                _globalState.Excute(_owner);
            }

            if(_currentState != null)
            {
                _currentState.Excute(_owner);
            }
        }

        public bool HandleMessage(Telegram msg)
        {
            if(_currentState != null && _currentState.OnMessage(_owner, msg))
            {
                return true;
            }

            if(_globalState != null && _globalState.OnMessage(_owner, msg))
            {
                return true;
            }
            return false;
        }

        public void RevertToPreviousState()
        {
            ChangeState(_previousState);
        }

        public bool IsInState(IState<T> state)
        {
            if (_currentState.GetType() == state.GetType())
            {
                return true;
            }
            return false;
        }
	}
}