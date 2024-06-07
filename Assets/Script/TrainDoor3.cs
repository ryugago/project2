using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDoor3 : MonoBehaviour
{
    public Animator Train_door1; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    public Animator Train_door2; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    public GameObject Trigger2;

    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �ݶ��̴��� ���� ��
        {
            Train_door1.SetBool("open", false); // �ִϸ��̼��� bool ������ false�� �����Ͽ� ����
            Train_door2.SetBool("open", false); // �ִϸ��̼��� bool ������ false�� �����Ͽ� ����
            Destroy(Trigger2);
        }
    }

   
}
