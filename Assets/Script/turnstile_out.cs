using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnstile_out : MonoBehaviour
{
    public Animator turnstileAnim1; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    public Animator turnstileAnim2; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    //private bool Isplayer = true;

    // Start is called before the first frame update
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �ݶ��̴����� ���� ��
        {
            Debug.Log("������");
            turnstileAnim1.SetBool("open", false); // �ִϸ��̼��� bool ������ false�� �����Ͽ� ����
            turnstileAnim2.SetBool("open", false); // �ִϸ��̼��� bool ������ false�� �����Ͽ� ����
        }
    }
}
