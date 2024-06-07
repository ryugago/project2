using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDoor : MonoBehaviour
{
    public Animator customordoor;
    //public Collider TriggerCollider; // ��Ȱ��ȭ�� �ݶ��̴��� ������ ����


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �ݶ��̴��� ���� ��
        {
            customordoor.SetBool("open", true); // �ִϸ��̼��� Ʈ���Ÿ� �ߵ���Ŵ
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            customordoor.SetBool("open", false); // �ִϸ��̼��� Ʈ���Ÿ� �ߵ���Ŵ
        }
    }
}
