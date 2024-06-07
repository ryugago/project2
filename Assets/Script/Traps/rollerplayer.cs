using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollerplayer : MonoBehaviour
{
    public Animator playerroll;
    public GameObject player;
    public GameObject rollcamera;

    [Header("플레이어 애니메이션이후 위치")]
    public Transform playertrans;
    public Vector3 playervector;

    public Camera maincamer;
    public Quaternion rollafterplayer;

    bool istrigger = false;
    public gate1trap trap;

    public chatroller chat;
    // Update is called once per frame

    public GameObject boomsoundaudio;
    void Update()
    {
        if (istrigger&& trap.trap)
        {
            StartCoroutine(boomsound());

            playertrans.position = new Vector3(128.9314f, 1.418863f, 118.4941f);
            playertrans.rotation = Quaternion.Euler(0, 358.981f, 0);
            maincamer.transform.rotation = Quaternion.Euler(27.9f, 0, 0);
            rollcamera.SetActive(true);
            GameManager.canPlayerMove2 = false;
            player.SetActive(false);
            playerroll.SetTrigger("roll");

            // 4초 후에 메쉬 활성화
            chat.enabled = true;
            Invoke("EnableMesh", 29f);
            istrigger = false;
        }
    }

    IEnumerator boomsound()
    {
        yield return new WaitForSeconds(1.1f);
        boomsoundaudio.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            istrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Player")
            istrigger = false;
    }

    void EnableMesh()
    {
        player.SetActive(true);
        rollcamera.SetActive(false);
        playertrans.position = playervector;
        maincamer.transform.rotation = rollafterplayer;
        GameManager.canPlayerMove2 = true;
    }
}
