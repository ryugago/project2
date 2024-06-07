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
        // soundclock는 즉시 활성화
        soundclock.SetActive(true);

        // soundTV는 10초 뒤에 활성화
        Invoke("ActivateSoundTV", 10f);
    }

    // soundTV 활성화를 위한 메소드
    void ActivateSoundTV()
    {
        //soundTV.SetActive(true);
        ontv.gameObject.SetActive(true);
        TVobj.GetComponent<Renderer>().material = TVon;
        Dopen.trigger++;
    }
}
