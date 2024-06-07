using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class doorController : MonoBehaviour
{
    public TextMeshProUGUI doortext;
    public Animator doorAnim; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    private bool door_Player_sensor = false;

    private void Start()
    {
        doortext.text = " ";
    }

    private void Update()
    {
        if (door_Player_sensor && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("�� ����");
            doorAnim.SetBool("open", true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ViewPoint")) // �÷��̾ �ݶ��̴��� ���� ��
        {

            Debug.Log("�÷��̾ ����");
            doortext.text = "EŰ�� ���� ����";
            door_Player_sensor = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ViewPoint")) // �÷��̾ �ݶ��̴����� ���� ��
        {
            doortext.text = " ";
            Debug.Log("������");
            doorAnim.SetBool("open", false); // �ִϸ��̼��� bool ������ false�� �����Ͽ� ����
        }
    }
}


