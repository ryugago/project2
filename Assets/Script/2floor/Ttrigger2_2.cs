using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger2_2 : MonoBehaviour
{
    public GameObject Toliet;
    public Animator Tanima;
    public GameObject resist_img;
    public Ttrigger2_3 chat;
    public GameObject Player;

    public float total = 0f;
    public float Keydown = 0.1f;

    private bool scriptActive = true;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.SetActive(false);
        Toliet.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!scriptActive) // 스크립트가 비활성화되었다면 아래 코드를 실행하지 않음
            return;
        AnimatorStateInfo currentState = Tanima.GetCurrentAnimatorStateInfo(0);
        bool isTrapAnimationPlaying = currentState.IsName("camera1");

        if (isTrapAnimationPlaying)
        {
            resist_img.SetActive(true);
            if (total <= 0.99f)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    total += Keydown;
                }
            }
        }
        if (total > 1f)
        {
            resist_img.SetActive(false);
            Tanima.SetBool("trap", true);
            chat.enabled = true;
            scriptActive = false;
        }
    }
}
