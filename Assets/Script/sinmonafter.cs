using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinmonafter : MonoBehaviour
{
    public GameObject soundclock;
    public GameObject soundTV;

    [Header("TV")]
    public GameObject TVobj;
    public Material TVon;

    public Light ontv;

    public customerouttrigger Dopen;

    // Start is called before the first frame update
    void Start()
    {
        // soundclock�� ��� Ȱ��ȭ
        soundclock.SetActive(true);

        // soundTV�� 10�� �ڿ� Ȱ��ȭ
        Invoke("ActivateSoundTV", 10f);
    }

    // soundTV Ȱ��ȭ�� ���� �޼ҵ�
    void ActivateSoundTV()
    {
        //soundTV.SetActive(true);
        ontv.gameObject.SetActive(true);
        TVobj.GetComponent<Renderer>().material = TVon;
        Dopen.trigger++;
    }
}
