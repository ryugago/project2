using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVLIghton : MonoBehaviour
{
    private bool istrigger;

    public Animator japangi;
    public Light TVlight;

    public GameObject TVobj;
    public Material TVmaterial;

    public GameObject tvsound;

    public bag1trigger postertrigger;

    public TMPro.TMP_Text quest;
    // Start is called before the first frame update
    void Start()
    {
        TVlight.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (istrigger)
        {
            Invoke("ActivateTrap", 2f);
            tvsound.SetActive(true);
            TVlight.gameObject.SetActive(true);
            TVobj.GetComponent<Renderer>().material = TVmaterial;
        }
        if (postertrigger.message1)
        {
            quest.text = "고객대기실로 이동";
            Invoke("deltquest", 3f);
        }

    }

    void deltquest()
    {
        quest.text = " ";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            istrigger = true;
        }
    }
    void ActivateTrap()
    {
        japangi.SetTrigger("move"); // 애니메이션의 트리거를 발동시킴
        //TriggerCollider.gameObject.SetActive(true); // 비활성화된 콜라이더를 활성화
    }
}
