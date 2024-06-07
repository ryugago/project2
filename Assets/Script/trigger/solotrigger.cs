using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class solotrigger : MonoBehaviour
{
    public bool trigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            trigger = true;
    }
}
