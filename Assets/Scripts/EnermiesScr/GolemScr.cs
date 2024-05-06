using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemScr : MonoBehaviour
{
    Rigidbody rd;
    Animator at;

    float speed = 10.0f;
    Vector3 startPosition;
    GameObject player;
    float startTime;
    public float jumpForce = 10.0f;
    bool isJump;
    float timeAttack, timeAttackDelay = 2.0f;
    
    [SerializeField] float dame = 5.0f;

    void Start()
    {
        rd = GetComponent<Rigidbody>();
        at = GetComponent<Animator>();

        startTime = Time.deltaTime;
        startPosition = transform.position;
        player = GameObject.FindWithTag("Player");
        isJump = false;
        timeAttack = 0;
    }

    void Update()
    {
        if(timeAttack > 0) timeAttack -= Time.deltaTime;
        if(isJump){
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);

            // Thực hiện Raycast
            if (Physics.Raycast(ray, out hit, 0.5f))
            {
                // Nếu Raycast gặp một đối tượng, in tên của đối tượng đó ra console
                Debug.Log("Raycast hit: " + hit.transform.name);
                if(hit.transform.name == "Plane"){
                    isJump = false;
                }
            }
        }
    }

    void FixedUpdate(){
        if(player != null && !isJump){
            float distanceStartToPlayer = Vector3.Distance(player.transform.position, startPosition);
            if(distanceStartToPlayer <= 50){
                Vector3 dirMove = (player.transform.position - transform.position);
                dirMove.Normalize();
                float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
                transform.LookAt(player.transform);
                if(distanceToPlayer > 40){
                    this.JumpTo(player.transform.position);
                }
                else if(distanceToPlayer > 5f){
                    transform.position += dirMove * speed * Time.deltaTime;
                    at.SetFloat("Walk", 1);
                }
                else{
                    at.SetFloat("Walk", 0);
                    if(timeAttack <= 0){
                        at.SetTrigger("Hit");
                        timeAttack = timeAttackDelay;
                        this.attack();
                    }
                    
                }
            }
            else{
                Vector3 dirMove = (startPosition - transform.position);
                dirMove.Normalize();
                transform.position += dirMove * speed * Time.deltaTime;
            }
        }
    }

    public void JumpTo(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position) / 8;
        Vector3 dirJump = (new Vector3(direction.x, jumpForce, direction.z));
        rd.AddForce(dirJump , ForceMode.Impulse);
        isJump = true;
    }

    void attack(){
        dame++;
    }
}
