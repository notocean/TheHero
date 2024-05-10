using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dame : MonoBehaviour
{
    [SerializeField] GameObject attackEffect2;
    [SerializeField] GameObject attackEffect1;
    [SerializeField] AudioClip soundEffAttack;
    [SerializeField] AudioSource attackAudio;
    [SerializeField] AttackDetect attackDetect;

    public void setnotActiveAttackEffect1(){
        if(attackEffect1 != null) 
            attackEffect1.SetActive(false);
    }
    public void setActiveAttackEffect1(){
        if (soundEffAttack != null)
            attackAudio.PlayOneShot(soundEffAttack,0.75f);
        if(attackEffect1 != null) 
            attackEffect1.SetActive(true);
    }

    public void setnotActiveAttackEffect2(){
        if(attackEffect2 != null) 
            attackEffect2.SetActive(false);
    }
    public void setActiveAttackEffect2() {
        if (soundEffAttack != null)
            attackAudio.PlayOneShot(soundEffAttack,0.75f);
        if(attackEffect2 != null) 
            attackEffect2.SetActive(true);
    }

    public void StopAttack() {
        attackDetect.StopAttack();
    }
}
