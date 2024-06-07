using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap1 : MonoBehaviour
{
    public GameObject Hit_obj;
    public Animator Trap; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    private bool hasTriggered = true; // �� ���� ����ǵ��� �����ϱ� ���� ����

    private void Start()
    {
        Hit_obj.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasTriggered && other.CompareTag("Player")) // �÷��̾ �ݶ��̴��� ���� ��
        {
            Hit_obj.SetActive(true);
            Trap.SetTrigger("Trap"); // �ִϸ��̼��� Ʈ���Ÿ� �ߵ���Ŵ
            hasTriggered = false; // Ʈ���Ű� �۵������� ǥ��
        }
    }
}
