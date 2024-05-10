using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantMoveState : EnemyState
{
    Enemy_Mutant mutant;

    public MutantMoveState(EnemyInfor _enemyBase, string _animName, Enemy_Mutant _mutant) : base(_enemyBase, _animName)
    {
        mutant = _mutant;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExecute()
    {
        if (!GameManager.Instance.IsPlaying)
            return;
        base.OnExecute();

        enemyBase.Move();

        if (enemyBase.InAttackRange() && !mutant.isAttack)
            enemyBase.ChangeState(mutant.mutantAttackState);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
