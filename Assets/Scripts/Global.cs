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
            NPC_Guard npcGuard = new GameObject().AddComponent<NPC_Guard>();
            npcGuard.InitData(EntityID.NPC_Guard);

            EntityManager.Self.Regist(npcGuard);

            StartCoroutine(GlobalUpdate(1, () => { npcGuard.Excute(); }));
        }


        void Update()
        {
            //DispatchMessage.Self.DispatchDelayedMessage();
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