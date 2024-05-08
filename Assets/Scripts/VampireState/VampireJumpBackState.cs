using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireJumpBackState : EnemyState
{
    private Enemy_Vampire vampire;

    public VampireJumpBackState(EnemyInfo _enemyBase, string _animName, Enemy_Vampire vampire) : base(_enemyBase, _animName)
    {
        this.vampire = vampire;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExecute()
    {
        base.OnExecute();

        vampire.JumpBack();

        if (triggerCalled)
            enemyBase.ChangeState(vampire.vampireIdleState);


    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
