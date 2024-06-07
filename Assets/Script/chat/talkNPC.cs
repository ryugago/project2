using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class talkNPC : MonoBehaviour
{
    public TMP_Text TalkPhone;

    // Update is called once per frame
    void Update()
    {
        if (TalkPhone.text == "[???]")
        {
            TalkPhone.color = Color.red;
        }
        else
        {
            TalkPhone.color = Color.white;
        }
    }
}
