using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantAttackState : EnemyState
{
    private Enemy_Mutant mutant;
    private int comboCounter = 0;

    public MutantAttackState(EnemyInfor _enemyBase, string _animName, Enemy_Mutant _mutant) : base(_enemyBase, _animName)
    {
        this.mutant = _mutant;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        if (comboCounter > 1)
            comboCounter = 0;

        enemyBase.anim.SetInteger("ComboCounter", comboCounter);
    }

    public override void OnExecute()
    {
        if (!GameManager.Instance.IsPlaying)
            return;
        base.OnExecute();

        if (triggerCalled && !mutant.isAttack)
            enemyBase.ChangeState(mutant.mutantIdleState);

        if (!enemyBase.InAttackRange() && !mutant.isAttack)
            enemyBase.ChangeState(mutant.mutantMoveState);
    }

    public override void OnExit()
    {
        base.OnExit();
        comboCounter ++;
    }
}
