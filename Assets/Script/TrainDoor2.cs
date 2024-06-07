using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDoor2 : MonoBehaviour
{
    //public Animator Train_door1; // 애니메이터 컴포넌트를 가리키는 변수
    //public Animator Train_door2; // 애니메이터 컴포넌트를 가리키는 변수
    public GameObject Trigger2;
    public GameObject Blinklight;
    public GameObject remove_fire;
    public trainoutchat chat;


    private bool istrigger = false;
    private bool istrigger1 = true;


    private void Start()
    {
        Blinklight.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 콜라이더에 들어올 때
        {
            if (istrigger1)
            {
                istrigger1 = false;
                istrigger = true;
            }
            if (istrigger)
            {
                istrigger = false;
                GameManager.onlycamera = false;
                Destroy(remove_fire);
                //Train_door1.SetBool("open", false); // 애니메이션의 bool 변수를 false로 설정하여 정지
                //Train_door2.SetBool("open", false); // 애니메이션의 bool 변수를 false로 설정하여 정지Blinklight.SetActive(true);
                chat.enabled = true;
            }
        }
    }
    
}
