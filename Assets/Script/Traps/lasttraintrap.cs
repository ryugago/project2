using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lasttraintrap : MonoBehaviour
{
    public GameObject sound;
    public nolever nolever;

    bool trap=false;
    void Start()
    {
        
    }

    private void Update()
    {
        if (trap)
            sound.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (nolever.trigger)
        {
            if (other.tag == "Player")
            {
                trap = true;
            }
        }
    }
}
