using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnoff : MonoBehaviour
{
    [SerializeField]
    private float Turnofftime;


    [SerializeField]
    private GameObject objectToDestroy; // ������ ������Ʈ�� �����ϱ� ���� ����

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ ������ ������ ��
        {
            StartCoroutine(DeleteAfterDelay(objectToDestroy, Turnofftime)); // 0.1�� �ڿ� ����
        }
    }

    IEnumerator DeleteAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay); // delay��ŭ ���
        Destroy(obj); // �ش� ������Ʈ ����
    }
}
