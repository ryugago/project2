using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NovaSamples.Effects;
using FronkonGames.SpiceUp.DoubleVision;

public class Train_hallucination : MonoBehaviour
{
    public float[] mental;

    //public TMP_Text interac_txt;
    public int istrigger = 0;
    public Material mental_breakdown;
    public GameObject[] delete;
    public Animator jusa;
    public GameObject phone;

    [Header("화면블러")]
    public BlurEffect blurEffect;

    public GameObject captin_cardkey;

    public GameObject jusaimg;
    public GameObject jusaobj;

    public TMP_Text quest2;
    public bool pick_jusa = false;
    // Start is called before the first frame update

    [Header("핸드")]
    public Material hand2ma;
    public Renderer hand2ren;

    public AudioClip clip;

    bool jusabool = true;
    // Update is called once per frame
    void Update()
    {
        BreakDown();
        if (istrigger == 4 && pick_jusa)
        {
            jusaimg.SetActive(true);
            if (Input.GetKeyDown(KeyCode.T))
            {
                GameManager.canPlayerMove2 = false;
                jusaimg.SetActive(false);
                istrigger++;
                //phone.SetActive(false);
                jusa.SetBool("jusa", true);
                StartCoroutine(TriggerEffectsAfterDelay());
                DoubleVision.AddRenderFeature();
                DoubleVision.Settings settings = DoubleVision.GetSettings();
                settings.strength.z = 0f;
            }
        }
        if(istrigger==4 && jusabool)
        {
            jusaobj.SetActive(true);
            jusabool = false;
        }
    }

    void BreakDown()
    {
        switch (istrigger)
        {
            case 1:
                mental_breakdown.SetFloat("_Fullscreenintensity", mental[0]);
                break;
            case 2:
                mental_breakdown.SetFloat("_Fullscreenintensity", mental[1]);
                break;
            case 3:
                mental_breakdown.SetFloat("_Fullscreenintensity", mental[2]);
                break;
            case 4:
                mental_breakdown.SetFloat("_Fullscreenintensity", mental[3]);
                break;
        }
    }

    IEnumerator TriggerEffectsAfterDelay()
    {
        yield return new WaitForSeconds(1.4f); // 1.4초 대기

        // 1.4초 후에 실행될 작업들
        StartCoroutine(ApplyHitEffect());
        captin_cardkey.SetActive(true);
        blurEffect.BlurRadius = 0;
        SoundManager.instance.SFXPlay("jusasound", clip);
        for (int i = 0; i < delete.Length; i++)
        {
            Destroy(delete[i]);
        }
        hand2ren.material = hand2ma;
        yield return new WaitForSeconds(1f); // 1초 대기
                                             //phone.SetActive(true);
        jusa.SetBool("jusa", false);
        GameManager.canPlayerMove2 = true;
        quest2.text = "열쇠를 찾아보자";
    }

    IEnumerator ApplyHitEffect()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 3)
        {
            float t = elapsedTime / 3;
            mental_breakdown.SetFloat("_Fullscreenintensity", Mathf.Lerp(1f, 0f, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the effect ends at exactly 0
        mental_breakdown.SetFloat("_Fullscreenintensity", 0f);
    }
}
