using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questbackground : MonoBehaviour
{
    public TMPro.TMP_Text quest1;
    public TMPro.TMP_Text quest2;
    public RectTransform Qbackground;

    private void Start()
    {
        quest1.text = " ";
    }


    private void Update()
    {
        if(quest2.text!= ""&& quest2.text != " ")
        {
            Qbackground.gameObject.SetActive(true);
            ResizeImageByTextLength();
        }
        else
        {
            Qbackground.gameObject.SetActive(false);
        }
    }
    void ResizeImageByTextLength()
    {
        // �ؽ�Ʈ�� ���̿� ���� �̹����� ũ�⸦ ����
        float textLength = quest2.preferredWidth;
        float imageWidth = textLength * 0.25f; // ���ÿ����� �ؽ�Ʈ ������ 1.5��� �̹��� ũ�� ����
        Qbackground.sizeDelta = new Vector2(imageWidth, Qbackground.sizeDelta.y);
    }
}
