using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class turnstile : MonoBehaviour
{
    public TextMeshProUGUI turnstiletext;
    public Animator turnstileAnim1; // 애니메이터 컴포넌트를 가리키는 변수
    public Animator turnstileAnim2; // 애니메이터 컴포넌트를 가리키는 변수
    private bool turnstile_Player_sensor = false;

    private PlayerController Player;
    

    private void Start()
    {
        Player = FindObjectOfType<PlayerController>();
        turnstiletext.text = " ";
    }

    private void Update()
    {
        if (turnstile_Player_sensor && Input.GetKeyDown(KeyCode.E))
        {
            if (Player.hasKeys[2])
            {
                //Debug.Log("문 열림");
                turnstileAnim1.SetBool("open", true);
                turnstileAnim2.SetBool("open", true);
            }
            else if (!Player.hasKeys[2])
                turnstiletext.text = "카드 키가 없습니다.";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ViewPoint")) // 플레이어가 콜라이더에 들어올 때
        {

            //Debug.Log("플레이어가 들어옴");
            turnstiletext.text = "E키를 눌러 열기";
            turnstile_Player_sensor = true;

        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ViewPoint")) // 플레이어가 콜라이더에서 나갈 때
        {
            turnstiletext.text = " ";
           
        }
    }
    
}
