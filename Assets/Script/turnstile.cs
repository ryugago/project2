using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class turnstile : MonoBehaviour
{
    public TextMeshProUGUI turnstiletext;
    public Animator turnstileAnim1; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    public Animator turnstileAnim2; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    private bool turnstile_Player_sensor = false;

    private PlayerController Player;
    

    private void Start()
    {
        Player = FindObjectOfType<PlayerController>();
        turnstiletext.text = " ";
    }

    private void Update()
    {
        if (turnstile_Player_sensor && Input.GetKeyDown(KeyCode.E))
        {
            if (Player.hasKeys[2])
            {
                //Debug.Log("�� ����");
                turnstileAnim1.SetBool("open", true);
                turnstileAnim2.SetBool("open", true);
            }
            else if (!Player.hasKeys[2])
                turnstiletext.text = "ī�� Ű�� �����ϴ�.";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ViewPoint")) // �÷��̾ �ݶ��̴��� ���� ��
        {

            //Debug.Log("�÷��̾ ����");
            turnstiletext.text = "EŰ�� ���� ����";
            turnstile_Player_sensor = true;

        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ViewPoint")) // �÷��̾ �ݶ��̴����� ���� ��
        {
            turnstiletext.text = " ";
           
        }
    }
    
}
