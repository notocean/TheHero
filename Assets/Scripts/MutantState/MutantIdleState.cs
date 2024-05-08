using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantIdleState : EnemyState
{
    private Enemy_Mutant mutant;

    public MutantIdleState(EnemyInfo _enemyBase, string _animName, Enemy_Mutant _mutant) : base(_enemyBase, _animName)
    {
        mutant = _mutant;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        enemyBase.Stop();

        stateTimer = enemyBase.attackDuration;

        Debug.Log(stateTimer);
    }

    public override void OnExecute()
    {
        base.OnExecute();

        if (enemyBase.InAttackRange() && stateTimer < 0)
            enemyBase.ChangeState(mutant.mutantAttackState);
        
        if(!enemyBase.InAttackRange())
            enemyBase.ChangeState(mutant.mutantMoveState);

        
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
