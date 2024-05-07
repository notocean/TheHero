using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemScr : MonoBehaviour
{
    Rigidbody rd;
    Animator at;
    Vector3 startPosition;
    GameObject player;

    float speed = 4.0f;
    float startTime;
    public float jumpForce = 10.0f;
    bool isJump;
    float timeAttackDelay = 2.5f, timeAttack;
    float timeAttackDelay2 = 3f, timeAttack2;
    float rangeFindPlayer = 50f, rangeJump = 30f, rangeAttack = 5.0f;
    [SerializeField] float health, maxHealth = 300.0f;
    int level;
    public void setLevel(int i){
        this.level = i;
    }
    public void upHealth() {
        this.maxHealth += maxHealth * 0.035f;
    }
    
    // float dame = 200.0f;
    public void upDame() {
        this.maxHealth += maxHealth * 0.05f;
    }

    void Start()
    {
        rd = GetComponent<Rigidbody>();
        at = GetComponent<Animator>();

        this.health = maxHealth;
        startTime = Time.deltaTime;
        startPosition = transform.position;
        player = GameObject.FindWithTag("Player");
        isJump = false;
        timeAttack = 0;
    }

    void Update()
    {
        if(isJump){
            Ray ray = new Ray(transform.position, Vector3.down);
            Debug.DrawRay(ray.origin, ray.direction * 0.5f, Color.blue);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 0.5f))
            {
                Debug.Log("Raycast hit: " + hit.transform.name);
                if(hit.transform.name == "Plane"){
                    isJump = false;
                    at.SetTrigger("Land");
                }
            }
        }
        if(player != null && !isJump){
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            if(distanceToPlayer <= rangeAttack){
                at.SetFloat("Walk", 0);
                if(timeAttack <= 0){
                    this.attack();
                    at.SetTrigger("Hit");
                    timeAttack = timeAttackDelay;
                }
                if(timeAttack2 <= 0){
                    this.attack();
                    at.SetTrigger("Hit2");
                    timeAttack2 = timeAttackDelay2;
                }
            }
        }
        timeAttack -= Time.deltaTime;
        timeAttack2 -= Time.deltaTime;
        if(this.health <= 0){
            at.SetTrigger("Rage");
            //goi phuong thuc tan cong
            at.SetTrigger("Die");
            Destroy(gameObject,2.0f);
        }
    }

    void FixedUpdate(){
        if(player != null && !isJump){
            float distanceStartToPlayer = Vector3.Distance(player.transform.position, startPosition);
            if(distanceStartToPlayer <= rangeFindPlayer){
                Vector3 dirMove = (player.transform.position - transform.position);
                dirMove.Normalize();
                dirMove.y = 0;
                float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
                transform.LookAt(player.transform);
                if(distanceToPlayer > rangeJump){
                    this.Jump(player.transform.position);
                }
                else if(distanceToPlayer > rangeAttack){
                    transform.position += dirMove * speed * Time.deltaTime;
                    Debug.Log(transform.position);
                    at.SetFloat("Walk", 1);
                }
            }
            else{
                if(Vector3.Distance(transform.position, startPosition) < 3.0f) at.SetFloat("Walk", 0);
                else {
                    Vector3 dirMove = (startPosition - transform.position);
                    dirMove.Normalize();
                    transform.position += dirMove * speed * Time.deltaTime;
                    at.SetFloat("Walk", 1);
                    transform.LookAt(startPosition);
                }
            }
        }
    }

    public void Jump(Vector3 targetPosition)
    {
        Vector3 dirJump = (targetPosition - transform.position) / 8;
        dirJump.y = jumpForce;
        rd.AddForce(dirJump, ForceMode.Impulse);
        at.SetTrigger("Jump");
        isJump = true;
    }

    void attack(){
        // player?.GetComponent<Player>()?.TakeDamage(Mathf.FloorToInt(dame));
    }

    public void takeDame(float dame){
        this.health = Mathf.Clamp(health + dame, 0, maxHealth);
    }
}
