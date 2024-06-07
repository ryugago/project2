using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDoor3open2 : MonoBehaviour
{
    public Animator Train_door; // 애니메이터 컴포넌트를 가리키는 변수
    public GameObject Trigger2;

    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 콜라이더에 들어올 때
        {
            Train_door.SetBool("open", false); // 애니메이션의 bool 변수를 false로 설정하여 정지
            Destroy(Trigger2);
        }
    }

   
}
