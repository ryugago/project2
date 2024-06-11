using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etrigger3_2 : MonoBehaviour
{
    public Etrigger3 trigger;

    public GameObject O_img;

    public Animator TDoor;

    public Collider Dcollider;

    private bool trigger1;
    private bool trigger2=true;

    public AudioClip clip;

    // Update is called once per frame
    void Update()
    {
        if (trigger1 && trigger2)
        {
            O_img.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                SoundManager.instance.SFXPlay("toilet", clip);
                trigger2 = false;
                O_img.SetActive(false);
                Dcollider.enabled = false;
                TDoor.SetBool("open2", true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViewPoint") 
        {
            if (trigger.Ntrigger)
            {
                trigger1 = true;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag== "ViewPoint")
        {
            O_img.SetActive(false);
            trigger1 = false;
        }
    }
}
