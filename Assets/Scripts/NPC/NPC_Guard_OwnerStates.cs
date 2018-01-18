using UnityEngine;
using System.Collections;
using AsyncRun.Core;

/// <summary>
/// 巡逻
/// </summary>
public class Patrol : IState<NPC_Guard>
{
    public void Enter(NPC_Guard entity)
    {
        Debug.Log("Patrol : Enter");
    }
    public void Excute(NPC_Guard entity)
    {
        Debug.Log("Patrol : Excute");
        //看到敌人
        if (entity.CanSeeEnemy())
        {
            entity.OwnerStateMachine.ChangeState(entity.State_Attack);
        }
        else if(entity.CanHearEnemyNoise())
        {
            entity.OwnerStateMachine.ChangeState(entity.State_Investigate);
        }
    }

    public void Exit(NPC_Guard entity)
    {
        Debug.Log("Patrol : Exit");
    }
    public bool OnMessage(NPC_Guard entity, Telegram telegram)
    {
        return true;
    }
}

/// <summary>
/// 攻击
/// </summary>
public class Attack : IState<NPC_Guard>
{
    public void Enter(NPC_Guard entity)
    {
        Debug.Log("Attack : Enter");
    }
    public void Excute(NPC_Guard entity)
    {
        Debug.Log("Attack : Excute");
    }
    public void Exit(NPC_Guard entity)
    {
        Debug.Log("Attack : Exit");
    }
    public bool OnMessage(NPC_Guard entity, Telegram telegram)
    {
        return true;
    }
}

/// <summary>
/// 调查
/// </summary>
public class Investigate : IState<NPC_Guard>
{
    public void Enter(NPC_Guard entity)
    {
        Debug.Log("Investigate : Enter");
    }
    public void Excute(NPC_Guard entity)
    {
        Debug.Log("Investigate : Excute");
        //看到敌人
        if (entity.CanSeeEnemy())
        {
            entity.OwnerStateMachine.ChangeState(entity.State_Attack);
        }
    }
    public void Exit(NPC_Guard entity)
    {
        Debug.Log("Investigate : Exit");
    }
    public bool OnMessage(NPC_Guard entity, Telegram telegram)
    {
        return true;
    }
}

/// <summary>
/// 逃跑
/// </summary>
public class Flee : IState<NPC_Guard>
{
    public void Enter(NPC_Guard entity)
    {

    }
    public void Excute(NPC_Guard entity)
    {

    }
    public void Exit(NPC_Guard entity)
    {

    }
    public bool OnMessage(NPC_Guard entity, Telegram telegram)
    {
        return true;
    }
}
