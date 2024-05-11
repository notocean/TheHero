using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfor : MonoBehaviour
{
    public Rigidbody rb { get; private set; }
    public Animator anim {  get; private set; }
    public EnemyState currentState { get; private set; }

    [SerializeField] protected AudioSource[] sfx;

    [Header("Target Info")]
    protected Transform target;
    [SerializeField] public float moveSpeed;
    [HideInInspector] public Vector3 direction;

    [Space]

    [Header("HP")]
    public int maxHp;
    [HideInInspector] public int curHp;


    [Space]

    [Header("Attack Info")]
    [SerializeField] protected float attackRange;
    public float attackDuration;

    [Space]

    public float timeToDestroy;
    [SerializeField] protected Slider healthBar;
    protected bool isDie = false;
    [SerializeField] protected GameObject goldObj;
    [SerializeField] protected int goldValue;
    [SerializeField] Collider _collider;

    protected virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        curHp = maxHp;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        FindPlayer();
        RotateEnemy();
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
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    public bool InAttackRange() => Vector3.Distance(transform.position, target.position) <= attackRange;

    public virtual void TakeDamage(int _damage)
    {
        if (curHp <= 0) return;
        curHp -= _damage;
        healthBar.value = (float)curHp / maxHp;
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
        sfx[sfxToPlay].volume = GameManager.Instance.soundFactor;
        sfx[sfxToPlay].Play();
    }

    public void Die() {
        healthBar.gameObject.SetActive(false);
        Destroy(gameObject, timeToDestroy);
        isDie = true;
        _collider.enabled = false;
        EnemySpawnController.Instance.SpawnEnemy();
        GameObject gold = Instantiate(goldObj, transform.position, goldObj.transform.rotation);
        gold.GetComponentInChildren<ItemCollected>().SetValue(goldValue);
    }
}
