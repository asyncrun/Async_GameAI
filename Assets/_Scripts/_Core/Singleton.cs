//------------------------------------------------------------------------
//
//  Name:		Singleton.cs
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
namespace AsyncRun.Core
{
    public class Singleton<T> where T : new()
    {
        private static readonly T _self = new T();

        public static T Self
        {
            get { return _self; }
        }
    }
}
