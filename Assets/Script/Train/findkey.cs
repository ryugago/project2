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
                        // ��� blinklight ����Ʈ�� Ȱ��ȭ�մϴ�.
                        foreach (Light light in blinklight)
                        {
                            light.gameObject.SetActive(true);
                        }
                        isLightOn = true;
                        timer = 0f;
                    }

                    // ����Ʈ�� Ȱ��ȭ�� ���¿��� Ÿ�̸Ӹ� ������ŵ�ϴ�.
                    if (isLightOn)
                    {
                        timer += Time.deltaTime;

                        // Ÿ�̸Ӱ� ���� �ð�(delay) �̻��̸� ����Ʈ�� �ı��մϴ�.
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
