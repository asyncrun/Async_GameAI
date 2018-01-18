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
    public interface IState<T> where T : Entity
	{
        void Enter(T entity);
        void Excute(T entity);
        void Exit(T entity);
        bool OnMessage(T entity, Telegram telegram);
    }
}