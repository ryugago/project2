using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NovaSamples.Effects;

public class FtwoFirsttrigger : MonoBehaviour
{
    public Ftwosecond chat;

    public BlurEffect blurEffect;

    private bool trigger1;
    private bool trigger2=true;

    [Header("��� �����")]
    public GameObject[] stair;
    [Header("�� ����")]
    public GameObject wall;

    public AudioClip sfxclip;
    //public AudioClip bgmclip;

    // Update is called once per frame
    void Update()
    {
        if (trigger1 && trigger2)
        {
            GameManager.canPlayerMove2 = false;
            trigger2 = false;
            chat.enabled = true;
            SoundManager.instance.SFXPlay("tinnitus", sfxclip);
            StartCoroutine(ApplyHitEffect());
            wall.SetActive(true);
            foreach (GameObject stairs in stair)
            {
                Destroy(stairs);
            }
        }
    }


    IEnumerator ApplyHitEffect()
    {
        //isHit = true;

        // BlurRadius�� 8�� ����
        blurEffect.BlurRadius = 8f;

        // BlurRadius�� 7.5�� ���� 3���� ����
        float startTime = Time.time;
        float duration = 7.5f;
        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            blurEffect.BlurRadius = Mathf.Lerp(8f, 3f, t);
            yield return null;
        }
        blurEffect.BlurRadius = 3f;
        //isHit = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = true;
        }
    }

}
