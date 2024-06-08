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
        // 텍스트의 길이에 따라 이미지의 크기를 조절
        float textLength = quest2.preferredWidth;
        float imageWidth = textLength * 0.25f; // 예시에서는 텍스트 길이의 1.5배로 이미지 크기 조절
        Qbackground.sizeDelta = new Vector2(imageWidth, Qbackground.sizeDelta.y);
    }
}
