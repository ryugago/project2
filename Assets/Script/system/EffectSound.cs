using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSound : MonoBehaviour
{
    public Material mental_breakdown;
    public AudioClip clip;
    private bool isPlaying = false;

    void Start()
    {

    }

    void Update()
    {
        if (mental_breakdown)
        {
            float intensity = mental_breakdown.GetFloat("_Fullscreenintensity");

            if (intensity != 0f)
            {
                if (!isPlaying)
                {
                    SoundManager.instance.SFXPlay("Heart", clip, true);
                    isPlaying = true;
                }
            }
            else
            {
                if (isPlaying)
                {
                    SoundManager.instance.SFXStop("Heart");
                    isPlaying = false;
                }
            }
        }
    }
}
