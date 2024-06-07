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
                inter.text = "�Ʊ� �ִ� ���ع����� ��������";
                ctrlimg.SetActive(true);
            }
        }
        else if (istrigger && !trap.trap)
        {
            inter.text = "���� ������ ����.. ��������� ������?";
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
