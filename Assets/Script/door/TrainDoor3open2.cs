using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDoor3open2 : MonoBehaviour
{
    public Animator Train_door; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    public GameObject Trigger2;

    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �ݶ��̴��� ���� ��
        {
            Train_door.SetBool("open", false); // �ִϸ��̼��� bool ������ false�� �����Ͽ� ����
            Destroy(Trigger2);
        }
    }

   
}
