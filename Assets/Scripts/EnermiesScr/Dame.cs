using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dame : MonoBehaviour
{
    [SerializeField] GameObject attackEffect2;
    [SerializeField] GameObject attackEffect1;
    [SerializeField] AudioClip soundEffAttack;
    [SerializeField] AudioSource attackAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setnotActiveAttackEffect1(){
        if(attackEffect1 != null) attackEffect1.SetActive(false);
    }
    public void setActiveAttackEffect1(){
        attackAudio.PlayOneShot(soundEffAttack,0.75f);
        if(attackEffect1 != null) attackEffect1.SetActive(true);
    }

    public void setnotActiveAttackEffect2(){
        if(attackEffect2 != null) attackEffect2.SetActive(false);
    }
    public void setActiveAttackEffect2(){
        attackAudio.PlayOneShot(soundEffAttack,0.75f);
        if(attackEffect2 != null) attackEffect2.SetActive(true);
    }
}
