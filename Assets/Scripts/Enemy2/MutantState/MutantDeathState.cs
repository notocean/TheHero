using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantDeathState : EnemyState
{
    private Enemy_Mutant mutant;

    public MutantDeathState(EnemyInfor _enemyBase, string _animName, Enemy_Mutant _mutant) : base(_enemyBase, _animName)
    {
        this.mutant = _mutant;
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
