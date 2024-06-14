using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarmchatBE : MonoBehaviour
{
    public TrainDoor3open1 trigger;

    private bool trigger1;
    private bool trigger2 = true;

    public AudioClip clip;
    void Update()
    {
        if (trigger1 && trigger2)
        {
            trigger2 = false;
            SoundManager.instance.SFXPlay("horrornoise", clip);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (trigger.Carm)
        {
            if (other.tag == "Player")
            {
                trigger1 = true;
            }
        }
    }
}
