using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customerouttrigger : MonoBehaviour
{
    public Animator opendoor;

    public Light[] harilight;
    public Light redlight;

    public int trigger = 0;

    private bool enbl = true;


    public AudioClip cuopensound;
    private void Update()
    {
        if (!enbl)
            return;

        if (trigger >= 2)
        {

            foreach (Light light in harilight)
            {
                light.gameObject.SetActive(false);
            }
            redlight.range = 10f;
            redlight.intensity = 7f;
            Invoke("Copendoor", 5f);
            enbl = false;
        }
    }
    void Copendoor()
    {
        opendoor.SetTrigger("open");
        SoundManager.instance.SFXPlay("Cuopensound", cuopensound);
    }
}
