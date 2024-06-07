using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Csohwa : MonoBehaviour
{
    //public GameObject clocksound2;

    public GameObject sound3;
    public Animator table;

    public customerouttrigger Dopen;

    public AudioClip tablesound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SoundManager.instance.SFXPlay("tablesound", tablesound);
            table.SetTrigger("move");
            Invoke("Trinsound", 2f);
            //clock.SetTrigger("clock");
            //clocksound2.SetActive(true);
        }
    }
    void Trinsound()
    {
        sound3.SetActive(true);
        Dopen.trigger++;
        Destroy(gameObject);
    }
}
