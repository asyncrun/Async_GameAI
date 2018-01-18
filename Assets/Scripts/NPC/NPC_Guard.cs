using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsyncRun.Core;

public class NPC_Guard : Entity 
{
    private StateMachine<NPC_Guard> _ownerStateMachine;
    public StateMachine<NPC_Guard> OwnerStateMachine
    {
        get { return _ownerStateMachine; }
    }

    private Patrol _patrol;
    private Attack _attack;
    private Investigate _investigate;
    private Flee _flee;

    public Patrol State_Patrol
    {
        get { return _patrol; }
    }
    public Attack State_Attack
    {
        get { return _attack; }
    }
    public Investigate State_Investigate
    {
        get { return _investigate; }
    }
    public Flee State_Flee
    {
        get { return _flee; }
    }


    public GameObject TargetA;


    public NPC_Guard()
    {
        _ownerStateMachine = new StateMachine<NPC_Guard>(this);
        _patrol = new Patrol();
        _attack = new Attack();
        _investigate = new Investigate();
        _flee = new Flee();

        _ownerStateMachine.CurrentState = _patrol;
    }

    public override void Excute()
    {
        _ownerStateMachine.Excute();
    }

    public override bool HandleMessage(Telegram msg)
    {
        return false;
    }

    public bool CanSeeEnemy()
    {
        if (TargetA == null) return false;

        return Vector3.Distance(transform.position, TargetA.transform.position) < 5;
    }

    
    public bool CanHearEnemyNoise()
    {
        if (TargetA == null) return false;

        float distance = Vector3.Distance(transform.position, TargetA.transform.position);
        return distance < 10 && distance >= 5;
    }
}
