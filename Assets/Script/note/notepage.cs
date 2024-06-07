using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class notepage : MonoBehaviour
{
    //static public int notenum = 0;

    private PlayerController Playercon;

    public GameObject note1;
    public GameObject note2;
    public GameObject note3;
    public GameObject note4;
    public TMPro.TMP_Text quest2;

    public Text notegpage;

    private int totalpage = 0;
    private int currentpage = 0;

    public AudioClip noteclip;

    private void Start()
    {
        Playercon = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        totalpage = Playercon.notenum + 1;

        if (note1.activeSelf == true)
        {
            currentpage = 1;
            notegpage.gameObject.SetActive(true);
        }
        else if (note2.activeSelf == true)
        {
            currentpage = 2;
        }
        else if (note3.activeSelf == true)
        {
            currentpage = 3;
        }
        else if (note4.activeSelf == true)
        {
            currentpage = 4;
        }
        if (notegpage.gameObject.activeSelf == true)
        {
            notegpage.text = currentpage + "/" + totalpage;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            note1.SetActive(false);
            note2.SetActive(false);
            note3.SetActive(false);
            note4.SetActive(false);
        }

        if(note1.activeSelf&& quest2.text== "메모장을 확인하세요")
        {
            quest2.text = " ";
        }
    }

    public void gopage()
    {
        if (Playercon.notenum >= 1)
        {
            SoundManager.instance.SFXPlay("notesound1", noteclip);
            note1.SetActive(false);
            note2.SetActive(true);
        }
        else { Debug.Log("다음것이 안열림"); }
    }


    public void backpage1()
    {
        SoundManager.instance.SFXPlay("notesound2", noteclip);
        note2.SetActive(false);
        note1.SetActive(true);
    }

    public void gopage1()
    {
        if (Playercon.notenum >= 2)
        {
            SoundManager.instance.SFXPlay("notesound3", noteclip);
            note2.SetActive(false);
            note3.SetActive(true);
        }
    }

    public void backpage2()
    {
        SoundManager.instance.SFXPlay("notesound4", noteclip);
        note3.SetActive(false);
        note2.SetActive(true);
    }

    public void gopage2()
    {
        if (Playercon.notenum >= 3)
        {
            SoundManager.instance.SFXPlay("notesound5", noteclip);
            note3.SetActive(false);
            note4.SetActive(true);
        }
    }
    public void backpage3()
    {
        SoundManager.instance.SFXPlay("notesound6", noteclip);
        note4.SetActive(false);
        note3.SetActive(true);
    }

    public void gopage3()
    {
        if (Playercon.notenum >= 4)
        {
            SoundManager.instance.SFXPlay("notesound7", noteclip);
            note4.SetActive(false);
            //note4.SetActive(true);
        }
    }
}
