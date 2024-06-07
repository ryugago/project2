using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ctrlpillar : MonoBehaviour
{
    private bool istrigger = false;
    public gate1trap trap;

    public GameObject ctrlimg;
    public GameObject dummy;

    public TMPro.TMP_Text inter;

    void Update()
    {
        if (trap.trap)
        {
            dummy.SetActive(false);
            if (istrigger)
            {
                inter.text = "아까 있던 잔해물들이 없어졌어";
                ctrlimg.SetActive(true);
            }
        }
        else if (istrigger && !trap.trap)
        {
            inter.text = "길이 막힌거 같아.. 계단쪽으로 가볼까?";
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            istrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            istrigger = false;
            ctrlimg.SetActive(false);
            inter.text = " ";
        }
    }
}
