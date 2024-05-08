using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantAttackState : EnemyState
{
    private Enemy_Mutant mutant;
    private int comboCounter = 0;

    public MutantAttackState(EnemyInfo _enemyBase, string _animName, Enemy_Mutant _mutant) : base(_enemyBase, _animName)
    {
        this.mutant = _mutant;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        enemyBase.Stop();

        Debug.Log("call");

        if (comboCounter > 1)
            comboCounter = 0;

        enemyBase.anim.SetInteger("ComboCounter", comboCounter);

    }

    public override void OnExecute()
    {
        base.OnExecute();

        if (triggerCalled)
            enemyBase.ChangeState(mutant.mutantIdleState);

        if (!enemyBase.InAttackRange())
            enemyBase.ChangeState(mutant.mutantMoveState);
    }

    public override void OnExit()
    {
        base.OnExit();
        comboCounter ++;
    }
}
