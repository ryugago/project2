using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger3 : MonoBehaviour
{

    private bool trigger1;
    private bool trigger2=true;

    //public Ttrigger2_4 Ntrigger;
    public Ttrigger2_3 Ntrigger;

    private GameObject maincamer;
    public GameObject Player;
    public GameObject aniPlayer;

    //public Animator TdropPlayer;
    public Ttrigger3_1 chat;

    public Quaternion Protation;
    public Vector3 Pposition;

    public bool trigger;

    // Update is called once per frame
    private void Start()
    {
        maincamer = GameObject.FindGameObjectWithTag("MainCamera");
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        //AnimatorStateInfo currentState = TdropPlayer.GetCurrentAnimatorStateInfo(0);
        //bool isTrapAnimationPlaying = currentState.IsName("awkeplayer2");

        if (trigger1 && trigger2)
        {
            chat.enabled = true;
            Player.SetActive(false);
            aniPlayer.SetActive(true);
            trigger2 = false;
            StartCoroutine(PRcamerplay());
            trigger = true;
        }
        //if (isTrapAnimationPlaying)
        

        
    }

    IEnumerator PRcamerplay()
    {
        maincamer.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        PlayerController playerController = Player.GetComponent<PlayerController>();
        playerController.SetCameraRotationX(0f);
        Player.transform.rotation = Protation;
        Player.transform.position = Pposition;
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Ntrigger.sadare)
        {
            if (other.tag == "Player")
            {
                trigger1 = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = false;

        }
    }
}
