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
            quest.text = "�����Ƿ� �̵�";
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
        japangi.SetTrigger("move"); // �ִϸ��̼��� Ʈ���Ÿ� �ߵ���Ŵ
        //TriggerCollider.gameObject.SetActive(true); // ��Ȱ��ȭ�� �ݶ��̴��� Ȱ��ȭ
    }
}
