using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public Animator Trap; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    //public GameObject Trigger; //������Ʈ �����ϱ� ����


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �ݶ��̴��� ���� ��
        {
            Trap.SetBool("trigger", true); // �ִϸ��̼��� bool ������ false�� �����Ͽ� ����
            //Destroy(Trigger);//������Ʈ �����ϱ� ����
        }
    }


}
