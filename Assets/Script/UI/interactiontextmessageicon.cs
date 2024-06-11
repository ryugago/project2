using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactiontextmessageicon : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text intertxt;
    [SerializeField]
    private RawImage backimg;
    [SerializeField]
    private GameObject messageicon;
    [SerializeField]
    private GameObject callicon;
    [SerializeField]
    private GameObject noteicon;
    [SerializeField]
    private GameObject keyT;

    public AudioClip clip;

    private bool onesound = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!(intertxt.text == " ") && !(intertxt.text == ""))
        {
            if (intertxt.text == "메시지가 왔습니다.")
            {
                if (onesound)
                {
                    onesound = false;
                    SoundManager.instance.SFXPlay("PhoneCall", clip, false);
                }
                backimg.enabled = true;
                keyT.SetActive(true);
                messageicon.SetActive(true);
            }
            else if (intertxt.text == "전화가 왔습니다.")
            {
                backimg.enabled = true;
                keyT.SetActive(true);
                callicon.SetActive(true);
            }

            else if (intertxt.text == "노트가 업데이트 됐습니다.")
            {
                if (onesound)
                {
                    onesound = false;
                    SoundManager.instance.SFXPlay("PhoneCall", clip, false);
                }
                backimg.enabled = true;
                noteicon.SetActive(true);
            }
        }
        else
        {
            onesound = true;
            keyT.SetActive(false);
            backimg.enabled = false;
            messageicon.SetActive(false);
            callicon.SetActive(false);
            noteicon.SetActive(false);
        }
    }
}
