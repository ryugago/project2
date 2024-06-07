using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public PlayerController player;
    public GameObject go_BaseUi;
    public SaveNLoad theSaveLoad;

    public GameObject setting;
    [Header("메시지")]
    public GameObject messageback;
    public GameObject message;

    [Header("노트")]
    public GameObject notemain;
    public GameObject[] note;
    public GameObject notepage;

    [Header("갤러리")]
    public GameObject gallery;

    [Header("전화")]
    public GameObject call;

    [Header("홈,뒤로")]
    public GameObject Bhome;
    public GameObject Bback;
    public TMPro.TMP_Text quset;
    public TMPro.TMP_Text quset2;
    public GameObject phonetime;

    public GameObject alloutimg;

    [Header("키 활성화")]
    public GameObject esckeylook;
    public GameObject TABkeylook;

    bool check = false;
    bool check1 = true;
    bool check2 = true;
    bool check3 = true;

    public Scrollbar messagescroll;
    public GameObject mainmen;

    private bool isCallActive = false;
    private bool isMessageActive = false;
    private bool isGalleryActive = false;
    private bool isNoteActive = false;

    public AudioClip[] clip;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !mainmen.activeSelf&& player.hasPhone)
        {
            if (!GameManager.isPause)
            {
                SoundManager.instance.SFXPlay("Phone_open", clip[0], false);
                CallMenu();
            }
            else
            {
                SoundManager.instance.SFXPlay("Phone_close", clip[1], false);
                CloseMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (go_BaseUi.activeSelf)
            {
                if (call.activeSelf)
                {
                    call.SetActive(false);
                    isCallActive = false;
                }
                else if (gallery.activeSelf)
                {
                    gallery.SetActive(false);
                    isGalleryActive = false;
                }
                else if (messageback.activeSelf)
                {
                    messageback.SetActive(false);
                    isMessageActive = false;
                }
                else if (notemain.activeSelf)
                {
                    notemain.SetActive(false);
                    foreach (GameObject not in note)
                    {
                        not.SetActive(false);
                    }
                    isNoteActive = false;
                }
                else
                {
                    CloseMenu();
                }
            }
            else if (!go_BaseUi.activeSelf && !setting.activeSelf)
            {
                if (!GameManager.isPause)
                {
                    Callmainmenu();
                }
                else
                {
                    Closemainmenu();
                }
            }
        }
    }

    private void Callmainmenu()
    {
        GameManager.isPause = true;
        mainmen.SetActive(true);
    }

    private void Closemainmenu()
    {
        GameManager.isPause = false;
        mainmen.SetActive(false);
    }

    private void CloseMenu()
    {
        gallery.SetActive(false);
        call.SetActive(false);
        phonetime.SetActive(false);
        foreach (GameObject not in note)
            not.SetActive(false);
        notemain.SetActive(false);
        alloutimg.SetActive(false);
        GameManager.isPause = false;
        go_BaseUi.SetActive(false);
        Bhome.SetActive(false);
        Bback.SetActive(false);
        messageback.SetActive(false);
        notepage.SetActive(false);
        messagescroll.gameObject.SetActive(false);
        esckeylook.SetActive(false);
        TABkeylook.SetActive(false);
        isCallActive = false;
        isMessageActive = false;
        isGalleryActive = false;
        isNoteActive = false;
    }

    public void ClickCall()
    {
        SoundManager.instance.SFXPlay("Phone_Click1", clip[2], false);
        call.SetActive(true);
        isCallActive = true;
    }

    public void CallMenu()
    {
        
        phonetime.SetActive(true);
        esckeylook.SetActive(true);
        TABkeylook.SetActive(true);
        GameManager.isPause = true;
        alloutimg.SetActive(true);
        go_BaseUi.SetActive(true);
        Bhome.SetActive(true);
        Bback.SetActive(true);
    }

    public void ClickMessage()
    {
        SoundManager.instance.SFXPlay("Phone_Click2", clip[2], false);
        messageback.SetActive(true);
        messagescroll.value = 0;
        isMessageActive = true;
    }

    public void ClickGallery()
    {
        SoundManager.instance.SFXPlay("Phone_Click3", clip[2], false);
        gallery.SetActive(true);
        isGalleryActive = true;
    }

    public void Clicknote()
    {
        SoundManager.instance.SFXPlay("Phone_Click4", clip[2], false);
        notemain.SetActive(true);
        note[0].SetActive(true);
        if (check2)
        {
            check2 = false;
            StartCoroutine(message11());
            quset2.text = " ";
        }
        isNoteActive = true;
    }

    IEnumerator message11()
    {
        yield return new WaitForSeconds(3f);
        quset.text = " ";
    }

    public void home()
    {
        messageback.SetActive(false);
        foreach (var noteObj in note)
        {
            noteObj.SetActive(false);
        }
        notemain.SetActive(false);
        gallery.SetActive(false);
        call.SetActive(false);
        notepage.SetActive(false);
        messagescroll.gameObject.SetActive(false);
        isCallActive = false;
        isMessageActive = false;
        isGalleryActive = false;
        isNoteActive = false;
    }

    public void allout()
    {
        CloseMenu();
        messageback.SetActive(false);
        foreach (var noteObj in note)
        {
            noteObj.SetActive(false);
        }
        notemain.SetActive(false);
        gallery.SetActive(false);
        go_BaseUi.SetActive(false);
        call.SetActive(false);
        gallery.SetActive(false);
        isCallActive = false;
        isMessageActive = false;
        isGalleryActive = false;
        isNoteActive = false;
    }

    public void ClickSave()
    {
        Debug.Log("세이브 버튼 클릭");
        theSaveLoad.SaveDate();
    }

    public void ClickLoad()
    {
        Debug.Log("로드 버튼 클릭");
        theSaveLoad.LoadData();
    }

    public void SettingButton()
    {
        setting.SetActive(true);
    }

    public void Titlescen()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("TitleScene");
    }

    public void GAMEExit()
    {
        Application.Quit();
    }
}
