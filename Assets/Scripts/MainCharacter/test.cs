using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    float t = 0f;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += speed * Time.deltaTime;
        t = t - (int)t;
        transform.localPosition = Vector3.Slerp(pos1.localPosition, pos2.localPosition, t);
        transform.rotation = Quaternion.Slerp(pos1.rotation, pos2.rotation, t);
    }
}
