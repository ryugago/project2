using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bag1trigger : MonoBehaviour
{
    public TextMeshProUGUI messagetext;
    public GameObject T_img;
    [Header("메시지")]
    public GameObject messagepenul;
    public GameObject Message;
    public Light posterlight;

    private PlayerController Playercon;

    //여기 메시지 바꿀꺼
    public bool trigger = false;
    bool trigger2 = false;

    [Header("메시지")]
    //public GameObject messagemenu;
    //public GameObject Message;

    //public GameObject baseui;
    public PauseMenu menu;

    public bool message1;
    

    private void Start()
    {
        Playercon = FindObjectOfType<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (trigger && !trigger2) 
        {
            messagetext.text = "메시지가 왔습니다";
            T_img.SetActive(true);
            Playercon.Messagenum = 4;
            //messagepanel.num = 4;
            if (Input.GetKeyDown(KeyCode.T))
            {
                GameManager.isPause = true;
                trigger = false;
                trigger2 = true;
                posterlight.gameObject.SetActive(false);
                messagetext.text = " ";
                T_img.SetActive(false);
                menu.CallMenu();
                menu.ClickMessage();
                message1 = true;
                //baseui.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViewPoint")
            trigger = true;

    }
}
