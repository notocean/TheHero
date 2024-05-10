using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireMoveState : EnemyState
{
    Enemy_Vampire vampire;

    public VampireMoveState(EnemyInfor _enemyBase, string _animName, Enemy_Vampire _vampire) : base(_enemyBase, _animName)
    {
        vampire = _vampire;
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

        if (enemyBase.InAttackRange())
            enemyBase.ChangeState(vampire.vampireAttackState);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
