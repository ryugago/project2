using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questbackground : MonoBehaviour
{
    public TMPro.TMP_Text quest1;
    public TMPro.TMP_Text quest2;
    public RectTransform Qbackground;

    public GameObject[] image;

    public RawImage background;

    private void Start()
    {
        quest1.text = " ";

        foreach(GameObject img in image)
        {
            img.SetActive(false);
        }
    }


    private void Update()
    {
        if (quest1.text != "" && quest1.text != " ")
        {
            background.enabled = true;
            if (quest1.text == "문 여는 방법을 찾아보자.")
            {
                image[0].SetActive(true);
            }
            else if (quest1.text == "열쇠를 찾아보자.") 
            {
                image[1].SetActive(true);
            }
            else if(quest1.text== "진통제를 찾아보자.")
            {
                image[2].SetActive(true);

            }
            else if(quest1.text== "레버를 찾아보자.")
            {
                image[3].SetActive(true);
            }
            else if(quest1.text== "고객대기실로 이동")
            {
                image[4].SetActive(true);
            }
            else if(quest1.text== "나갈 방법을 찾아보자.")
            {
                image[5].SetActive(true);
            }
            else if(quest1.text== "출구를 찾아보자.")
            {
                image[6].SetActive(true);
            }
            else if (quest1.text == "중앙으로 이동하기.")
            {
                image[7].SetActive(true);
            }
        }
        else
        {
            foreach (GameObject img in image)
            {
                img.SetActive(false);
            }
            background.enabled = false;

        }
        /*i*/
    }
    
}
