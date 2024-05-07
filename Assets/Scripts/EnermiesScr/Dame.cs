using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dame : MonoBehaviour
{
    [SerializeField] GameObject attackEffect2;
    [SerializeField] GameObject attackEffect1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setnotActiveAttackEffect1(){
        attackEffect1.SetActive(false);
    }
    public void setActiveAttackEffect1(){
        attackEffect1.SetActive(true);
    }

    public void setnotActiveAttackEffect2(){
        attackEffect2.SetActive(false);
    }
    public void setActiveAttackEffect2(){
        attackEffect2.SetActive(true);
    }
}
