using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phoneclock : MonoBehaviour
{
    private PlayerController playerCon;
    private DateTime currentTime = new DateTime(1, 1, 1, 1, 0, 0);
    private float timer;
    private string timeString;

    public TMPro.TMP_Text phoneclocktime;

    // Start is called before the first frame update
    void Start()
    {
        playerCon = FindObjectOfType<PlayerController>();
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayTime();
        phoneclocktime.text = timeString;
        if (playerCon.hasPhone)
        {
            timer += Time.deltaTime;
            if (timer >= 60f) // ���� 1���� ������
            {
                timer = 0f;
                currentTime = currentTime.AddMinutes(1); // �ð��� 1�� ������Ŵ
                DisplayTime();
            }
        }
    }

    void DisplayTime()
    {
        timeString = currentTime.ToString("HH:mm");
        //Debug.Log("Current Time: " + timeString);
    }
}
