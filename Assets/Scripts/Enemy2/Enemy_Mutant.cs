using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mutant : EnemyInfor
{
    public GameObject[] attackVFX;
    public MutantIdleState mutantIdleState {  get; private set; }
    public MutantMoveState mutantMoveState {  get; private set; }
    public MutantAttackState mutantAttackState { get; private set; }

    public MutantDeathState mutantDeathState { get; private set; }
    public bool isAttack { get; private set; }

    private void Awake()
    {
        mutantIdleState = new MutantIdleState(this, "Idle", this);
        mutantMoveState = new MutantMoveState(this, "Move", this);
        mutantAttackState = new MutantAttackState(this, "Attack", this);
        mutantDeathState = new MutantDeathState(this, "Death", this);
        isAttack = false;
    }

    protected override void Start()
    {
        base.Start();
        StartState(mutantMoveState);
    }

    protected override void Update()
    {
        if (isDie)
            return;
        base.Update();
        currentState.OnExecute();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        if (curHp <= 0)
        {
            curHp = 0;
            ChangeState(mutantDeathState);
            Die();
        }
    }

    public void PunchSFX() => PlaySFX(1);

    public void SwipingSFX() => PlaySFX(2);

    public void OnVFX(int vfxToOn) => attackVFX[vfxToOn].SetActive(true);

    public void OffVFX(int vfxToOff) => attackVFX[vfxToOff].SetActive(false);

    public void StartAttack() {
        isAttack = true;
    }

    public void StopAttack() {
        isAttack = false;
    }
}
