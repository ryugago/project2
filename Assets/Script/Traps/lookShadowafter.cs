using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookShadowafter : MonoBehaviour
{

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
        if (trigger1 && trigger2)
        {
            SoundManager.instance.SFXPlay("whisper", clip);
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
