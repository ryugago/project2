using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cartrigger : MonoBehaviour
{
    public bool istrigger = false;




    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("ViewPoint"))
       {
            istrigger = true;
       }
    }

    private void OnTriggerExit(Collider other)
    {
        istrigger = false;
    }
}
