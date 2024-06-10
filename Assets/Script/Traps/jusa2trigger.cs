using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FronkonGames.SpiceUp.Blurry;

public class jusa2trigger : MonoBehaviour
{
    public Animator jusa;
    public GameObject jusaimg;
    public GameObject jusaobj;
    [Header("화면블러")]
    public NovaSamples.Effects.BlurEffect blurEffect;
    public Material mental_breakdown;

    [Header("핸드")]
    public Material hand3ma;
    public Renderer hand3ren;
    private bool trigger = false;
    private bool trigger2 = true;

    public jusa2before before;
    public jusa2after after;

    public AudioClip clip;

    private void Update()
    {
        if (trigger&& before.trigger&& trigger2)
        {
            jusaimg.SetActive(true);
            if (Input.GetKeyDown(KeyCode.T))
            {
                trigger2 = false;
                before.trigger = false;
                jusaimg.SetActive(false);
                jusa.SetBool("jusa", true);
                StartCoroutine(TriggerEffectsAfterDelay());
            }
        }
    }

    IEnumerator TriggerEffectsAfterDelay()
    {
        yield return new WaitForSeconds(1.4f); // 1.4초 대기

        // 1.4초 후에 실행될 작업들
        blurEffect.BlurRadius = 0;
        mental_breakdown.SetFloat("_Fullscreenintensity", 0f);
        hand3ren.material = hand3ma;
        Blurry.AddRenderFeature();
        Blurry.Settings setting = Blurry.GetSettings();
        setting.strength = 0f;
        SoundManager.instance.SFXPlay("jusasound", clip);
        yield return new WaitForSeconds(1f); // 1초 대기
                                             //phone.SetActive(true);
        jusa.SetBool("jusa", false);
        after.enabled = true;
        GameManager.canPlayerMove2 = true;
        DataManager.instance.DataSave();
        Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.canPlayerMove2 = false;
            trigger = true;
            before.enabled = true;
        }
    }
}
