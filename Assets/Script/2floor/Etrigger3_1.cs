using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etrigger3_1 : MonoBehaviour
{
    public Etrigger3 trigger;
    private bool trigger1;
    private bool trigger2=true;

    public Light Ltoliet;
    public GameObject Topen;


    public Animator WTDoor;

    public AudioClip toiletsound;
    // Update is called once per frame
    void Update()
    {
        if (trigger.Ntrigger&& trigger1&& trigger2)
        {
            //trigger.Ntrigger = false;
            trigger2 = false;
            Ltoliet.gameObject.SetActive(true);
            SoundManager.instance.SFXPlay("Toiletsound", toiletsound);
            WTDoor.SetTrigger("open1");
            Topen.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = false;
        }
    }
}
