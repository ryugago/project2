using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainDoor : MonoBehaviour
{
    public Animator Train_door1; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    //public Animator Train_door2; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    public GameObject Trigger;
    public TMP_Text interaction_txt;
    public PlayerController Play;

    public nolever chat1;
    //public brokenlever chat2;

    [Header("������!!")]
    //public TMP_Text keydowntxt;
    public TMP_Text quest;

    [Header("EŰ �̹���")]
    public GameObject Eimg;

    public bool trap = false;
    private bool Isplayer = false;
    private bool Isplayer2 = true;
    int keydown = 0;

    bool check = true;

    public bool trigger_open = false;

    public AudioClip clip;

    private void Update()
    {

        if (Isplayer&& Isplayer2)
        {
            Eimg.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Play.hasKeys[1])
                {
                    Isplayer2 = false;
                    interaction_txt.text = "������ ���峵���ϴ�.";
                    quest.text = " �� �����̰��� ������ ����";
                    trigger_open = true;
                    Invoke("ClearInteractionText", 2f);
                    /*keydowntxt.gameObject.SetActive(true);
                    keydown++;
                    if (keydown >= 10)
                    {
                        Train_door1.SetBool("open", true); // �ִϸ��̼��� bool ������ false�� �����Ͽ� ����
                        keydowntxt.gameObject.SetActive(false);
                    }
                    //Train_door2.SetBool("open", true); // �ִϸ��̼��� bool ������ false�� �����Ͽ� ����*/
                }
                else
                {
                    if (check)
                    {
                        chat1.enabled = true;
                        check = false;
                    }
                    SoundManager.instance.BgSoundPlay(clip);
                    interaction_txt.text = "������ �����ִ�";
                    trap = true;
                }
            }
        }
        else if (Isplayer && !Isplayer2)
        {
            Eimg.SetActive(false);

        }
        
    }
    void ClearInteractionText()
    {
        interaction_txt.text = "";
        quest.text = "";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ViewPoint")) // �÷��̾ �ݶ��̴��� ���� ��
        {
            //interaction_txt.text = "���� EŰ";
            Isplayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //interaction_txt.text = " ";
        Eimg.SetActive(false);
        Isplayer = false;
    }

}
