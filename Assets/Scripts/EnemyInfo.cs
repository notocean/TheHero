using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    public static EnemyInfo instance;

    public Rigidbody rb { get; private set; }
    public Animator anim {  get; private set; }
    public EnemyState currentState { get; private set; }

    [SerializeField] protected AudioSource[] sfx;

    [Header("Target Info")]
    public Transform target;
    [SerializeField] public float moveSpeed;
    [HideInInspector] public Vector3 direction;

    [Space]

    //[Header("Level")]
    //[SerializeField] int maxlevel;
    //[HideInInspector] public int curLevel = 1;

    //[Space]

    [Header("HP")]
    public int maxHp;
    [HideInInspector] public int curHp;

    //[Space]

    //[Header("Armor")]
    //[SerializeField] protected int maxArmor;
    //protected int curArmor;

    [Space]

    [Header("Attack Info")]
    [SerializeField] protected float attackRange;
    //[SerializeField] float attackTime;
    public float attackDuration;

    [Space]

    public float timeToDestroy;
    // float attackTimeCount;

    //protected string curAnim;

    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
            instance = this;

    }

    protected virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        
        //currentState = EnemyState.Move;
        //curLevel = 1;
        curHp = maxHp;
        //curArmor = maxArmor;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        FindPlayer();
        RotateEnemy();

        //if (Input.GetKeyDown(KeyCode.Space))
        //    TakeDamage(10);
    }

    public void FindPlayer()
    {
        direction = target.position - this.transform.position;
        direction.y = 0;

    }

    public void RotateEnemy()
    {
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle));
    }

    public void Move()
    {
        //anim.SetBool(currentState.ToString(), true);
        
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    public void Stop()
    {
        rb.velocity = Vector3.zero;
    }

    //public void EnemyAttack()
    //{
    //    attackTimeCount -= Time.deltaTime;
    //    if (comboCounter > 1)
    //        comboCounter = 0;

    //    if (InAttackRange() && attackTimeCount < 0)
    //    {
    //        attackTimeCount = attackTime;
    //        anim.SetBool(currentState.ToString(), true);
    //        anim.SetInteger("ComboCounter", comboCounter);
    //    }
    //    else if (!InAttackRange())
    //    {
    //        anim.SetBool(currentState.ToString(), false);
    //        ChangeState(EnemyState.Move);
    //    }

    //}

    //public void EndAttack()
    //{
    //    anim.SetBool(currentState.ToString(), false);
    //    comboCounter++;
    //}

    public bool InAttackRange() => Vector3.Distance(transform.position, target.position) <= attackRange;

    public virtual void TakeDamage(int _damage)
    {
        if (curHp <= 0) return;

        curHp -= _damage;
        
    }

    public void StartState(EnemyState _newState)
    {
        currentState = _newState;
        currentState.OnEnter();
    }

   public void ChangeState(EnemyState _newState)
    {
        currentState.OnExit();

        currentState = _newState;

        currentState.OnEnter();
    }

    public void AnimationTrigger() => currentState.AnimatonFinish();

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Play();
    }

    //public virtual void UpdateState(EnemyState currentState)
    //{
      
    //}
}
