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

    [Header("계단 사라짐")]
    public GameObject[] stair;
    [Header("벽 생성")]
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

        // BlurRadius를 8로 설정
        blurEffect.BlurRadius = 8f;

        // BlurRadius를 7.5초 동안 3으로 감소
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
