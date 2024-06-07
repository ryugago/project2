using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public Animator Trap; // 애니메이터 컴포넌트를 가리키는 변수
    //public GameObject Trigger; //오브젝트 삭제하기 위해


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 콜라이더에 들어올 때
        {
            Trap.SetBool("trigger", true); // 애니메이션의 bool 변수를 false로 설정하여 정지
            //Destroy(Trigger);//오브젝트 삭제하기 위해
        }
    }


}
