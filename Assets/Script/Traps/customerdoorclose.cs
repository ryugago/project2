using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customerdoorclose : MonoBehaviour
{
    //public Animator anicamer;

    public Animator close;
    public GameObject japansgisound1;
    
    //public GameObject TVsound4;
    //public Animator table;
    //이벤트 자판가->시계->기차->책상->티비
    public Light ontv;
    public TMPro.TMP_Text quest;
    [Header("채팅스크립트")]
    public incustmerroomchat chat;
    private bool doorclose;
    private bool doorclose1=true;
    // Update is called once per frame

    public onlycamer Mcamera;
    public AudioClip closesound;

    private void Start()
    {
        ontv.gameObject.SetActive(false);
    }
    void Update()
    {
        if (doorclose&& doorclose1)
        {
            doorclose1 = false;
            close.SetBool("open", false);
            SoundManager.instance.SFXPlay("closesound", closesound);
            chat.enabled = true;
            
            //
        }
        if (chat.cutomertrigger)
        {
            Invoke("ActivateSound1", 2f);
            /*Invoke("ActivateSound2", 4f);
            Invoke("ActivateSound3", 6f);
            Invoke("ActivateSound4", 8f);
            Invoke("ActivateSound5", 10f);
            Invoke("ActivateSound52", 13f);
            Invoke("ActivateSound6", 16f);*/
        }
    }

    void ActivateSound1()
    {
        japansgisound1.SetActive(true);
        quest.text = " ";
        Destroy(gameObject);
    }

    /*void ActivateSound2()
    {
        
    }

    void ActivateSound3()
    {
        
    }
    void ActivateSound4()
    {
        table.SetTrigger("move");
    }
    void ActivateSound5()
    {
        TVsound4.SetActive(true);
        ontv.gameObject.SetActive(true);
        TVobj.GetComponent<Renderer>().material = TVon;
    }
    void ActivateSound52()
    {
        foreach (Light light in harilight)
        {
            light.gameObject.SetActive(false);
        }
        redlight.range = 10f;
        redlight.intensity = 7f;
    }
    void ActivateSound6()
    {
        opendoor.SetTrigger("open");
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            doorclose = true;
            Mcamera.enabled = true;
        }
    }
}
