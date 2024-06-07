using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkLight : MonoBehaviour
{
    private Light theLight;//사용할 Light를 theLight라는 이름으로 사용
    private float targetIntensity;//변화시킬 불빛의 타겟 밝기 (현재 밝기가 타겟 밝기로 따라가야한다.)
    private float currentIntensity;// 현재 밝기

    public float lowLight = 0.4f;
    public float highLight = 1f;

    void Start()
    {
        theLight = GetComponent<Light>();//theLight 은 <Light>이라는 콤퍼넌트를 갖기
        currentIntensity = theLight.intensity;//currentIntensity는 현재 사용하는 theLight의 밝기.
        targetIntensity = Random.Range(lowLight, highLight);//변화할 타겟밝기는 (0.4f 와 1f)사이에서 랜덤하게 주기.
    }
    void Update()
    {
        if (Mathf.Abs(targetIntensity - currentIntensity) >= 0.01)
        {
            if (targetIntensity - currentIntensity >= 0)
            {
                currentIntensity += Time.deltaTime * 3f;
            }
            else
            {
                currentIntensity -= Time.deltaTime * 3f;
                theLight.intensity = currentIntensity;

                theLight.range = currentIntensity + 10;
            }
        }
        else
        {
            targetIntensity = Random.Range(lowLight, highLight);
        }
    }
}