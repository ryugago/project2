using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etrigger3_1_1 : MonoBehaviour
{
    public Etrigger3 trigger;
    private bool trigger1;
    private bool trigger2 = true;

    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger.Ntrigger && trigger1 && trigger2)
        {
            trigger2 = false;
            SoundManager.instance.SFXPlay("toilet", clip);
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
