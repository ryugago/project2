using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap5 : MonoBehaviour
{
    private bool lightcon = false;
    public Light underExitLightComponent1;

    private void Start()
    {
        underExitLightComponent1.gameObject.SetActive(false);

    }

    void Update()
    {
        if (lightcon)
        {
            StartCoroutine(ToggleLightIntensity(0.4f));
            lightcon = false; // 무한 루프를 방지하기 위해 lightcon을 false로 설정합니다.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lightcon = true;

            underExitLightComponent1.gameObject.SetActive(true);
        }
    }

    IEnumerator ToggleLightIntensity(float interval)
    {
        while (true)
        {
            // Toggle intensity
            underExitLightComponent1.intensity = (underExitLightComponent1.intensity == 0f) ? 4f : 0f;
            yield return new WaitForSeconds(interval);
        }
    }
}
