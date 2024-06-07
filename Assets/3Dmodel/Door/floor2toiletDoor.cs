using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor2toiletDoor : MonoBehaviour
{
    public Animator toiletDoor;
    public bool Dooropen = true;

    private bool control = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (control)
            {
                toiletDoor.SetBool("open", Dooropen);
                control = false;
            }
        }
    }
}
