using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap4 : MonoBehaviour
{
    public GameObject Trashcan; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    public Animator Trap4;
    //public Collider TriggerCollider; // ��Ȱ��ȭ�� �ݶ��̴��� ������ ����
    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �ݶ��̴��� ���� ��
        {
            Trap4.SetTrigger("Trap"); // �ִϸ��̼��� Ʈ���Ÿ� �ߵ���Ŵ
            //TriggerCollider.gameObject.SetActive(true); // ��Ȱ��ȭ�� �ݶ��̴��� Ȱ��ȭ
        }
    }
}
