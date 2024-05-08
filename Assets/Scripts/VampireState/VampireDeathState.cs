using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireDeathState : EnemyState
{
    private Enemy_Vampire vampire;

    public VampireDeathState(EnemyInfo _enemyBase, string _animName, Enemy_Vampire vampire) : base(_enemyBase, _animName)
    {
        this.vampire = vampire;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        enemyBase.Stop();

        AudioManager.instance.PlaySFX(6);
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
