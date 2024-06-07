using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findkey : MonoBehaviour
{
    public bool fin = false;
    public GameObject rever;
    public Light[] blinklight;

    private bool isLightOn = false;
    private bool hasExecuted = false;
    private float timer = 0f;
    private float delay = 5f;

    public TrainDoor trigger;
    public nolever nolever;

    private void Update()
    {
        if (trigger.trap)
        {
            if (fin)
            {
                if (rever != null)
                {
                    rever.SetActive(true);
                }
                if (!hasExecuted)
                {
                    if (!isLightOn)
                    {
                        // 모든 blinklight 라이트를 활성화합니다.
                        foreach (Light light in blinklight)
                        {
                            light.gameObject.SetActive(true);
                        }
                        isLightOn = true;
                        timer = 0f;
                    }

                    // 라이트가 활성화된 상태에서 타이머를 증가시킵니다.
                    if (isLightOn)
                    {
                        timer += Time.deltaTime;

                        // 타이머가 지연 시간(delay) 이상이면 라이트를 파괴합니다.
                        if (timer >= delay)
                        {
                            foreach (Light light in blinklight)
                            {
                                Destroy(light.gameObject);
                            }
                            isLightOn = false;
                            timer = 0f;
                            fin = false;
                            Destroy(gameObject);
                            hasExecuted = true;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(nolever.trigger)
                fin = true;
        }
    }
}
