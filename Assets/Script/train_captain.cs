using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class train_captain : MonoBehaviour
{
    public PlayerController Player;
    public Animator Door;
    public TMP_Text interac_txt;
    public GameObject dooropen_button;
    public bool trap = false;
    private bool isdoor = false;

    public GameObject open;
    public bool message = false;

    public chatcaptinkey chatcaptinkey;

    public AudioClip locksound;
    public AudioClip unlocksound;
    // Update is called once per frame
    void Update()
    {
        if (isdoor && Input.GetKeyDown(KeyCode.E))
        {
            if (Player.hasKeys[0])
            {
                SoundManager.instance.SFXPlay("unlocksound", unlocksound);
                //interac_txt.text = " ";
                open.SetActive(false);
                Door.SetBool("open", true);
                dooropen_button.SetActive(true);
            }
            else
            {
                SoundManager.instance.SFXPlay("Locksound", locksound);
                interac_txt.text = "잠겨있습니다";
                chatcaptinkey.enabled = true;
                trap = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViewPoint")
        {
            //interac_txt.text = "열기 E";
            open.SetActive(true);
            isdoor = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isdoor = false;

        //interac_txt.text = " ";
        open.SetActive(false);
    }
}
