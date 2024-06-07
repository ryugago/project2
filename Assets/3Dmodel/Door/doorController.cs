using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class doorController : MonoBehaviour
{
    public TextMeshProUGUI doortext;
    public Animator doorAnim; // 애니메이터 컴포넌트를 가리키는 변수
    private bool door_Player_sensor = false;

    private void Start()
    {
        doortext.text = " ";
    }

    private void Update()
    {
        if (door_Player_sensor && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("문 열림");
            doorAnim.SetBool("open", true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ViewPoint")) // 플레이어가 콜라이더에 들어올 때
        {

            Debug.Log("플레이어가 들어옴");
            doortext.text = "E키를 눌러 열기";
            door_Player_sensor = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ViewPoint")) // 플레이어가 콜라이더에서 나갈 때
        {
            doortext.text = " ";
            Debug.Log("문닫힘");
            doorAnim.SetBool("open", false); // 애니메이션의 bool 변수를 false로 설정하여 정지
        }
    }
}


