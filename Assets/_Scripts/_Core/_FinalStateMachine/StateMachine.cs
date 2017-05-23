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

namespace AsyncRun.Core
{
	public class StateMachine<T> where T:Entity
	{
	    private readonly T _owner;
        private State<T> _previourState;
        private State<T> _currentState;
	    private State<T> _globalState;

	    public State<T> CurrentState
	    {
	        get { return _currentState; }
            set { _currentState = value; }
	    }

        public StateMachine(T owner)
        {
            _owner = owner;
        }

	    public void ChangeState(State<T> newState)
	    {
            Debug.Assert(newState != null, "<StateMachine.ChangeState>:: trying to assign null state to current.");

	        _previourState = _currentState;

            _currentState.Exit(_owner);
	        _currentState = newState;
            _currentState.Enter(_owner);
	    }
	}
}