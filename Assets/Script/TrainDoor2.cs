using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDoor2 : MonoBehaviour
{
    //public Animator Train_door1; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    //public Animator Train_door2; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    public GameObject Trigger2;
    public GameObject Blinklight;
    public GameObject remove_fire;
    public trainoutchat chat;


    private bool istrigger = false;
    private bool istrigger1 = true;


    private void Start()
    {
        Blinklight.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �ݶ��̴��� ���� ��
        {
            if (istrigger1)
            {
                istrigger1 = false;
                istrigger = true;
            }
            if (istrigger)
            {
                istrigger = false;
                GameManager.onlycamera = false;
                Destroy(remove_fire);
                //Train_door1.SetBool("open", false); // �ִϸ��̼��� bool ������ false�� �����Ͽ� ����
                //Train_door2.SetBool("open", false); // �ִϸ��̼��� bool ������ false�� �����Ͽ� ����Blinklight.SetActive(true);
                chat.enabled = true;
            }
        }
    }
    
}
