using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillar : MonoBehaviour
{
    public chatpillar chat;
    public Light Lightlookpillar;
    public Animator Playercamera;
    public Animator Pillar;

    public GameObject dust;
    public GameObject pillarlight;
    private bool trap = false;
    [Header("���")]
    public GameObject lightright;

    public AudioClip clip;

    public GameObject pillarob;

    private void Start()
    {
        pillarob.SetActive(false);
        Lightlookpillar.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (trap)
        {
            SoundManager.instance.SFXPlay("pillar", clip);
            Lightlookpillar.gameObject.SetActive(true);
            chat.enabled = true;
            GameManager.canPlayerMove2 = false;
            dust.SetActive(true);
            Playercamera.SetTrigger("pillar");
            Pillar.SetBool("trap", true);
            GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(lightoff());
            StartCoroutine(lightrightoff());
            trap = false;
        }
    }

    IEnumerator lightoff()
    {

        yield return new WaitForSeconds(2f); // delayTime��ŭ ���
        pillarlight.SetActive(false);
    }

    IEnumerator lightrightoff()
    {
        yield return new WaitForSeconds(0.2f); // delayTime��ŭ ���
        lightright.SetActive(false);
        yield return new WaitForSeconds(0.3f); // delayTime��ŭ ���
        lightright.SetActive(true);

        //pillarob.SetActive(tru);
        yield return new WaitForSeconds(1f); // delayTime��ŭ ���
        lightright.SetActive(false);
        yield return new WaitForSeconds(0.3f); // delayTime��ŭ ���
        lightright.SetActive(true);
        yield return new WaitForSeconds(0.5f); // delayTime��ŭ ���
        lightright.SetActive(false);
        yield return new WaitForSeconds(0.2f); // delayTime��ŭ ���
        lightright.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trap = true;
        }
    }
}
