using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameZone : MonoBehaviour
{
    [SerializeField] float dame = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider c) {
        if(c!=null) {
            if(c.gameObject.tag == "Player"){
                c.gameObject.GetComponent<MainInfor>().TakeDamage(Mathf.FloorToInt(dame));
            }
        }
    }
}
