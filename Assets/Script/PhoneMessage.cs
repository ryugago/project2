using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneMessage : MonoBehaviour
{
    public TextMeshProUGUI turnstiletext;
    public GameObject Message;
    private bool isMessage;

    private void Update()
    {
        if (isMessage && Input.GetKeyDown(KeyCode.T))
        {
            turnstiletext.text = " ";
            Message.SetActive(true);
            GameManager.isPause = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isMessage)
        {
            turnstiletext.text = "메시지가 왔습니다. 받으실려면 T키를 누르시오";
            isMessage = true;
        }

    }

}
