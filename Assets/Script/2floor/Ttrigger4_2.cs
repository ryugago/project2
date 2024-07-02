using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger4_2 : MonoBehaviour
{
    private bool trigger1;
    private bool trigger2=true;

    public GameObject button_img;

    public Animator dooropen;

    public AudioClip buttonsound;
    public AudioClip shuttersound;

    private void Update()
    {
        if (trigger1 && trigger2)
        {
            button_img.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                trigger2 = false;
                SoundManager.instance.SFXPlay("button", buttonsound);
                SoundManager.instance.SFXPlayVolume("shuttersound", shuttersound, 0.3f);
                dooropen.SetTrigger("open");
                button_img.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViewPoint")
        {
            trigger1 = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ViewPoint")
        {
            trigger1 = false;
            button_img.SetActive(false);
        }
    }
}
