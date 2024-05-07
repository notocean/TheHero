using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealBarScr : MonoBehaviour
{
    [SerializeField] Image HealBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    public void UpHeathBar(float health, float maxHealth){
        HealBar.fillAmount = health / maxHealth;
    }
}
