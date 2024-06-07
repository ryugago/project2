using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger1 : MonoBehaviour
{
    public Animator TDoor;

    private bool trigger1;
    private bool trigger2=true;

    public Ttrigger2 chat;


    public AudioClip toiletsound;
    void Update()
    {
        if (trigger1 && trigger2)
        {
            trigger2 = false;
            SoundManager.instance.SFXPlay("Toiletclosesound", toiletsound);
            TDoor.SetBool("open2", false);
            chat.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = true;
        }
    }
}
