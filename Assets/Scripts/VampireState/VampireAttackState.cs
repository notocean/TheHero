using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireAttackState : EnemyState
{
    Enemy_Vampire vampire;

    private int attackCounter;

    public VampireAttackState(EnemyInfo _enemyBase, string _animName, Enemy_Vampire _vampire) : base(_enemyBase, _animName)
    {
        vampire = _vampire;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        enemyBase.Stop();

        if (attackCounter > 1)
            attackCounter = 0;

        enemyBase.anim.SetInteger("AttackCounter", attackCounter);
    }

    public override void OnExecute()
    {
        base.OnExecute();

        if(triggerCalled)
            enemyBase.ChangeState(vampire.vampireIdleState);
    }

    public override void OnExit()
    {
        base.OnExit();

        attackCounter++;
    }

    
}
