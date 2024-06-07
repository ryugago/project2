using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger5 : MonoBehaviour
{
    private bool trigger1;
    private bool trigger2=true;
    private bool trigger3;

    public GameObject open;
    public Animator door;
    // Update is called once per frame
    void Update()
    {
        if (trigger1&&trigger2)
        {
            open.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                trigger2 = false;
                door.SetBool("open", true);
                open.SetActive(false);
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
            open.SetActive(false);
            trigger1 = false;
        }
    }
}
