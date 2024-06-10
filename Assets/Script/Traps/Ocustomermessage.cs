using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocustomermessage : MonoBehaviour
{
    public TMPro.TMP_Text messagetext;
    public GameObject T_img;
    [Header("메시지")]
    public PauseMenu phone_menu;
    public GameObject messagepenul;
    public GameObject Message;
    //public Light posterlight;
    //여기 메시지 바꿀꺼
    bool trigger = false;
    bool trigger2 = false;
    bool trigger3 = false;
    bool trigger4 = false;
    bool trigger5 = false;

    public Coutchat chat;
    private bool triggerchat = false;
    public Animator Lshadowcamera;
    //public Animator moveshadow;

    public Transform player;
    public Camera playercamera;

    public GameObject baseui;
    public PauseMenu menu;

    private PlayerController Playercon;

    private void Start()
    {
        Playercon = FindObjectOfType<PlayerController>();
        //shadow.SetActive(false);
    }
    void Update()
    {
        if (trigger && !trigger2)
        {
            messagetext.text = "메시지가 왔습니다.";
            T_img.SetActive(true);
            GameManager.canPlayerMove2 = false;
            Playercon.Messagenum = 7;
            //messagepanel.num = 7;
            if (Input.GetKeyDown(KeyCode.T))
            {
                trigger = false;
                trigger2 = true;
                GameManager.isPause = true;
                messagetext.text = " ";
                T_img.SetActive(false);
                //여기에 휴대폰 메뉴나오는거 수정 화면나오면
                menu.CallMenu();
                menu.ClickMessage();

                trigger4 = true;
                //
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && Message.activeSelf&& !trigger3)
        {
            messageafter();
        }
        if (triggerchat)
        {
            triggerchat = false;
            chat.enabled = true;
        }

    }
    public void messageafter()
    {
        if (trigger4)
        {
            trigger4 = false;
            trigger3 = true;
            //messagepenul.SetActive(false);
            //Message.SetActive(false);
            GameManager.canPlayerMove2 = false;
            triggerchat = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger = true;
        }
    }
}
