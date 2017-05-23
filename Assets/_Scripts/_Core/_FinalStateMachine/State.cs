//------------------------------------------------------------------------
//
//  Name:		State.cs
//
//  Author:		LIWEI\Administrator
//
//  Date:		05/23/2017 19:50:25
//
//  Project:	Async_GameAI
//
//  Desc:   
//
//------------------------------------------------------------------------

namespace AsyncRun.Core
{
    public class State<T> where T : Entity
	{
	    public virtual void Enter(T entity){}
        public virtual void Excute(T entity) { }
        public virtual void Exit(T entity) { }
	}
}