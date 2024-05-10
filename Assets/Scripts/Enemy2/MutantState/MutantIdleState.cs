using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantIdleState : EnemyState
{
    private Enemy_Mutant mutant;

    public MutantIdleState(EnemyInfor _enemyBase, string _animName, Enemy_Mutant _mutant) : base(_enemyBase, _animName)
    {
        mutant = _mutant;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        stateTimer = enemyBase.attackDuration;
    }

    public override void OnExecute()
    {
        if (!GameManager.Instance.IsPlaying)
            return;
        base.OnExecute();

        if (enemyBase.InAttackRange() && stateTimer < 0 && !mutant.isAttack) 
            enemyBase.ChangeState(mutant.mutantAttackState);
        
        if(!enemyBase.InAttackRange() && !mutant.isAttack)
            enemyBase.ChangeState(mutant.mutantMoveState);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
