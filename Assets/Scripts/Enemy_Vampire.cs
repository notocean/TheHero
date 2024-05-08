using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Vampire : EnemyInfo
{
    public GameObject me;

    // Start is called before the first frame update
    [Header("Attack Info")]
    public GameObject magicPref;
    public Transform attackPoint;
    [SerializeField] float magicSpeed;

    [Header("Special Attack")]
    [SerializeField] float magicNumber;
    [SerializeField] float nextMagicTime;

    [Header("JumpBack Info")]
    [SerializeField] float distance;
    [SerializeField] float jumpBackForce;
    public float jumpBackDuration;
    [SerializeField] float jumpBackCooldown; 
    private float jumpBackCounter;

    public VampireIdleState vampireIdleState {  get; private set; }
    public VampireMoveState vampireMoveState { get; private set; }
    public VampireAttackState vampireAttackState { get; private set; }

    public VampireJumpBackState vampireJumpBackState { get; private set; }

    public VampireDeathState vampireDeathState {  get; private set; }

    private void Awake()
    {
        vampireIdleState = new VampireIdleState(this, "Idle", this);
        vampireMoveState = new VampireMoveState(this, "Move", this);
        vampireAttackState = new VampireAttackState(this, "Attack", this);
        vampireJumpBackState = new VampireJumpBackState(this, "JumpBack", this);
        vampireDeathState = new VampireDeathState(this, "Death", this);
    }

    protected override void Start()
    {
        base.Start();
        StartState(vampireMoveState);

        UIManager.instance.vampireHealthSlider.maxValue = maxHp;
        UIManager.instance.vampireHealthSlider.value = curHp;
    }

    protected override void Update()
    {
        base.Update();
        currentState.OnExecute();

        jumpBackCounter -= Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) <= distance && jumpBackCounter < 0)
        {
            jumpBackCounter = jumpBackCooldown;
            ChangeState(vampireJumpBackState);
        }
    }


    public void JumpBack()
    {
        rb.velocity =  direction.normalized * -jumpBackForce;
    }

    public void NormalAttack()
    {
        GameObject newMagic = Instantiate(magicPref);
        newMagic.transform.position = attackPoint.position;
        newMagic.GetComponent<Rigidbody>().velocity = direction.normalized * magicSpeed;
        PlaySFX(1);
        Destroy(newMagic, 2f);
    }

    public void SpecialAttack()
    {
        StartCoroutine(IESpecialAttack());
    }

    private IEnumerator IESpecialAttack()
    {
        for (int i = 0; i < magicNumber; i++)
        {
            GameObject newMagic = Instantiate(magicPref);
            newMagic.transform.position = attackPoint.position;
            newMagic.GetComponent<Rigidbody>().velocity = direction.normalized * magicSpeed;
            PlaySFX(1);
            Destroy(newMagic, 2f);
            yield return new WaitForSeconds(nextMagicTime);
        }
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        PlaySFX(3);

        if(curHp <= 0)
        {
            curHp = 0;
            ChangeState(vampireDeathState);
            PlaySFX(4);
            Destroy(gameObject, timeToDestroy);
        }

        UIManager.instance.vampireHealthSlider.value = curHp;
    }
}
