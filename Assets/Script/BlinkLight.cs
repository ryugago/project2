using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkLight : MonoBehaviour
{
    private Light theLight;//����� Light�� theLight��� �̸����� ���
    private float targetIntensity;//��ȭ��ų �Һ��� Ÿ�� ��� (���� ��Ⱑ Ÿ�� ���� ���󰡾��Ѵ�.)
    private float currentIntensity;// ���� ���

    public float lowLight = 0.4f;
    public float highLight = 1f;

    void Start()
    {
        theLight = GetComponent<Light>();//theLight �� <Light>�̶�� ���۳�Ʈ�� ����
        currentIntensity = theLight.intensity;//currentIntensity�� ���� ����ϴ� theLight�� ���.
        targetIntensity = Random.Range(lowLight, highLight);//��ȭ�� Ÿ�ٹ��� (0.4f �� 1f)���̿��� �����ϰ� �ֱ�.
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