using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chatBackground : MonoBehaviour
{
    public TMPro.TMP_Text chat;
    public RawImage background;
    // Start is called before the first frame update
    void Start()
    {
        background.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(chat.text == "") && !(chat.text == " ")) 
        {
            background.enabled = true;
        }
        else
        {
            background.enabled = false;
        }
    }
}
