using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap2 : MonoBehaviour
{
    public GameObject posterlight; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    //public Animator japangi;
    //public Collider TriggerCollider; // ��Ȱ��ȭ�� �ݶ��̴��� ������ ����

    private void Start()
    {
        posterlight.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �ݶ��̴��� ���� ��
        {
            posterlight.SetActive(true); // �ִϸ��̼��� Ʈ���Ÿ� �ߵ���Ŵ
            Destroy(gameObject);
            //Invoke("ActivateTrap", 2f); // ActivateTrap �޼ҵ带 2�� �ڿ� ȣ��
        }
    }
    /*
    void ActivateTrap()
    {
        //japangi.SetTrigger("move"); // �ִϸ��̼��� Ʈ���Ÿ� �ߵ���Ŵ
        //TriggerCollider.gameObject.SetActive(true); // ��Ȱ��ȭ�� �ݶ��̴��� Ȱ��ȭ
    }
    */
}
