using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealBarScr : MonoBehaviour
{
    [SerializeField] Image HealBar;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(- Camera.main.transform.forward);
    }

    public void UpHeathBar(float health, float maxHealth){
        HealBar.fillAmount = health / maxHealth;
    }
}
