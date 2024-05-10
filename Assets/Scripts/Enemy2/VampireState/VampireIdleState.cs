using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireIdleState : EnemyState
{
    Enemy_Vampire vampire;
    public VampireIdleState(EnemyInfor _enemyBase, string _animName, Enemy_Vampire vampire) : base(_enemyBase, _animName)
    {
        this.vampire = vampire;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        stateTimer = 4f;
    }

    public override void OnExecute()
    {
        if (!GameManager.Instance.IsPlaying)
            return;
        base.OnExecute();

        if(enemyBase.InAttackRange())
            enemyBase.ChangeState(vampire.vampireAttackState);

        if (!enemyBase.InAttackRange())
            enemyBase.ChangeState(vampire.vampireMoveState);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
