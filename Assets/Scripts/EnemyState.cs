using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyState
{
    public EnemyInfo enemyBase {  get; private set; }

    private string animName;

    protected bool triggerCalled;
    protected float stateTimer;

    public EnemyState(EnemyInfo _enemyBase, string _animName)
    {
        this.enemyBase = _enemyBase;
        this.animName = _animName;
    }

    public virtual void OnEnter()
    {
        enemyBase.anim.SetBool(animName, true);
        triggerCalled = false;
    }

    public virtual void OnExecute()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void OnExit()
    {
        enemyBase.anim.SetBool(animName, false);
    }


    public virtual void AnimatonFinish()
    {
        triggerCalled = true;
    }
}
