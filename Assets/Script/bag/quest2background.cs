using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest2background : MonoBehaviour
{

    public RectTransform Qbackground;
    public TMPro.TMP_Text quest2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(quest2.text != "" && quest2.text != " ")
        {
            Qbackground.gameObject.SetActive(true);
            ResizeImageByTextLength();
        }
        else
        {
            Qbackground.gameObject.SetActive(false);
        }*/
    }
    void ResizeImageByTextLength()
    {
        // �ؽ�Ʈ�� ���̿� ���� �̹����� ũ�⸦ ����
        float textLength = quest2.preferredWidth;
        float imageWidth = textLength * 0.25f; // ���ÿ����� �ؽ�Ʈ ������ 1.5��� �̹��� ũ�� ����
        Qbackground.sizeDelta = new Vector2(imageWidth, Qbackground.sizeDelta.y);
    }
}
