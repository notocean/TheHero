using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mutant : EnemyInfo
{
    public GameObject me;

    private EnemyInfo enemy;

    public GameObject[] attackVFX;
    public MutantIdleState mutantIdleState {  get; private set; }
    public MutantMoveState mutantMoveState {  get; private set; }
    public MutantAttackState mutantAttackState { get; private set; }

    public MutantDeathState mutantDeathState { get; private set; }

    private void Awake()
    {
        mutantIdleState = new MutantIdleState(this, "Idle", this);
        mutantMoveState = new MutantMoveState(this, "Move", this);
        mutantAttackState = new MutantAttackState(this, "Attack", this);
        mutantDeathState = new MutantDeathState(this, "Death", this);
    }

    protected override void Start()
    {
        base.Start();
        StartState(mutantMoveState);

        UIManager.instance.mutantHealthSlider.maxValue = maxHp;
        UIManager.instance.mutantHealthSlider.value = curHp;
    }

    protected override void Update()
    {
        base.Update();
        //UpdateState(currentState);
        currentState.OnExecute();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);


        if (curHp <= 0)
        {
            curHp = 0;
            ChangeState(mutantDeathState);

            Destroy(gameObject, timeToDestroy);
        }

        UIManager.instance.mutantHealthSlider.value = curHp;
    }

    public void PunchSFX() => PlaySFX(1);

    public void SwipingSFX() => PlaySFX(2);

    public void OnVFX(int vfxToOn) => attackVFX[vfxToOn].SetActive(true);

    public void OffVFX(int vfxToOff) => attackVFX[vfxToOff].SetActive(false);

    //public override void UpdateState(EnemyState _currentState)
    //{
    //    switch ((int)_currentState)
    //    {
    //        case 0:
    //            EnemyMove();
    //            if (InAttackRange())
    //            {
    //                anim.SetBool(currentState.ToString(), false);
    //                ChangeState(EnemyState.Attack);
    //            }
    //            break;

    //        case 1:
    //            EnemyStop();
    //            EnemyAttack();
    //        break;
    //    }
    //}
}
