using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScr : MonoBehaviour
{
    Rigidbody rd;
    Animator at;
    Vector3 startPosition;
    GameObject player;

    float speed = 3.25f;
    float startTime;
    public float jumpForce = 10.0f;
    bool isJump;
    float timeAttackDelay = 2.5f, timeAttack;
    float rangeFindPlayer = 50f, rangeAttack = 10.0f;

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
                if(player.GetComponent<Rigidbody>().velocity.magnitude != 0){
                    at.SetFloat("Run", 1);
                }
                else at.SetFloat("Run", 0);
                if(timeAttack <= 0){
                    at.SetTrigger("Hit");
                    timeAttack = timeAttackDelay;
                }
            }
        }
        timeAttack -= Time.deltaTime;
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
                if(distanceToPlayer > rangeAttack / 2.0f){
                    transform.position += dirMove * speed * Time.deltaTime;
                    Debug.Log(transform.position);
                    at.SetFloat("Run", 1);
                }
            }
            else{
                if(Vector3.Distance(transform.position, startPosition) < 3.0f) at.SetFloat("Run", 0);
                else {
                    Vector3 dirMove = (startPosition - transform.position);
                    dirMove.Normalize();
                    transform.position += dirMove * speed * Time.deltaTime;
                    at.SetFloat("Run", 1);
                    transform.LookAt(startPosition);
                }
            }
        }
    }
}
