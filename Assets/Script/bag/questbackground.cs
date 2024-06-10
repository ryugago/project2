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
            if (quest1.text == "�� ���� ����� ã�ƺ���.")
            {
                image[0].SetActive(true);
            }
            else if (quest1.text == "���踦 ã�ƺ���.") 
            {
                image[1].SetActive(true);
            }
            else if(quest1.text== "�������� ã�ƺ���.")
            {
                image[2].SetActive(true);

            }
            else if(quest1.text== "������ ã�ƺ���.")
            {
                image[3].SetActive(true);
            }
            else if(quest1.text== "�����Ƿ� �̵�")
            {
                image[4].SetActive(true);
            }
            else if(quest1.text== "���� ����� ã�ƺ���.")
            {
                image[5].SetActive(true);
            }
            else if(quest1.text== "�ⱸ�� ã�ƺ���.")
            {
                image[6].SetActive(true);
            }
            else if (quest1.text == "�߾����� �̵��ϱ�.")
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
