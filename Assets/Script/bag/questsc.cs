using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questsc : MonoBehaviour
{
    public TMPro.TMP_Text quest; // ĵ������ �ִ� Text ������Ʈ
    public TMPro.TMP_Text quest2; // ĵ������ �ִ� Text ������Ʈ
    public float delay = 2f; // ����� �������� ���� �ð�(��)
    //private string previousQuestText;

    void Start()
    {
        quest.text = " ";
        quest2.text = " ";
        // 2�� �Ŀ� �ؽ�Ʈ ���� �Լ� ȣ��
    }
    void Update()
    {
        // ����: questText�� �ؽ�Ʈ�� ����Ǵ� ��츦 ����
        if (quest.text != " " && quest.text!= "��Ʈ�� ������Ʈ�� �ƽ��ϴ�.")
        {
            StartCoroutine(UpdateQuestTextAfterDelay(quest.text));
            // ����� �ؽ�Ʈ�� questText2�� ������Ʈ
        }/*
        else if(quest2.text=="�޴��� �ѷ�����")
        {
            StartCoroutine(nomessage());
        }*/
    }

    IEnumerator nomessage()
    {
        // delay �� ���� ���
        yield return new WaitForSeconds(1);

        // delay ���Ŀ� �ؽ�Ʈ ����
        quest2.text = " ";
    }

    IEnumerator UpdateQuestTextAfterDelay(string newText)
    {
        // delay �� ���� ���
        yield return new WaitForSeconds(delay);

        // delay ���Ŀ� �ؽ�Ʈ ����
        quest2.text = newText;
    }
    
}
