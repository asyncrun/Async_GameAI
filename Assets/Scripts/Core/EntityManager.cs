//------------------------------------------------------------------------
//
//  Name:		EntityManager.cs
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

using System.Collections.Generic;
using UnityEngine;

namespace AsyncRun.Core
{
    internal class EntityManager : Singleton<EntityManager>
    {
        private readonly Dictionary<int, Entity> _entityDic = new Dictionary<int, Entity>();

        public void Regist(Entity entity)
        {
            Debug.Assert(entity != null, "EntityManager Regist(entity), entity is null.");

            int id = entity.Id;
            if (!_entityDic.ContainsKey(id))
            {
                _entityDic.Add(id, entity);
            }
        }

        public void Remove(int id)
        {
            if (_entityDic.ContainsKey(id))
            {
                Object.DestroyObject(_entityDic[id].gameObject);
                _entityDic.Remove(id);
            }
        }

        public Entity GetEntity(int id)
        {
            Entity entity = null;
            if (_entityDic.ContainsKey(id))
            {
                entity = _entityDic[id];
            }
            return entity;
        }
    }
}
