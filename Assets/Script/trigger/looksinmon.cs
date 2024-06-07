using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class looksinmon : MonoBehaviour
{
    public GameObject looksimon;
    public GameObject Imglookinterection;
    public sinmonafter after;

    private bool look;

    public AudioClip sinmonsound;

    // Update is called once per frame
    void Update()
    {
        if (look)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SoundManager.instance.SFXPlay("sinmonsound", sinmonsound);
                GameManager.isPause = true;
                looksimon.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                sinmonexit();
            }
        }
    }

    public void sinmonexit()
    {
        looksimon.SetActive(false);
        //GameManager.isPause = false;
        after.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViewPoint")
        {
            look = true;
            Imglookinterection.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ViewPoint")
        {
            look = false;
            Imglookinterection.SetActive(false);
        }
    }
}
