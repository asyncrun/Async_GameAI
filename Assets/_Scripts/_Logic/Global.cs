//------------------------------------------------------------------------
//
//  Name:		Global.cs
//
//  Author:		LIWEI\Administrator
//
//  Date:		05/23/2017 19:16:32
//
//  Project:	Async_GameAI
//
//  Desc:   
//
//------------------------------------------------------------------------

using System.Collections;
using AsyncRun.Core;
using UnityEngine;
using UnityEngine.Events;

namespace AsyncRun.Logic
{
    public class Global : MonoBehaviour
    {
        void Start()
        {
            Miner miner = new Miner((int)EntityType.MINER);

            EntityManager.Self.Regist(miner);

            UnityAction updateAction = () =>
            {
                miner.Update();
            };

            StartCoroutine(GlobalUpdate(2, updateAction));
        }


        void Update()
        {
            DispatchMessage.Self.DispatchDelayedMessage();
        }

        public IEnumerator GlobalUpdate(float waitTime, UnityAction updateAction)
        {
            while (true)
            {
                if (updateAction != null)
                {
                    updateAction.Invoke();
                }
                yield return new WaitForSeconds(waitTime);
            }
        }
    }

}