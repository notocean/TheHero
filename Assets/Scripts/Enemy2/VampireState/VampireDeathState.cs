using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireDeathState : EnemyState
{
    private Enemy_Vampire vampire;

    public VampireDeathState(EnemyInfor _enemyBase, string _animName, Enemy_Vampire vampire) : base(_enemyBase, _animName)
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
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
