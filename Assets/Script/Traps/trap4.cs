using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap4 : MonoBehaviour
{
    public GameObject Trashcan; // 애니메이터 컴포넌트를 가리키는 변수
    public Animator Trap4;
    //public Collider TriggerCollider; // 비활성화된 콜라이더를 참조할 변수
    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 콜라이더에 들어올 때
        {
            Trap4.SetTrigger("Trap"); // 애니메이션의 트리거를 발동시킴
            //TriggerCollider.gameObject.SetActive(true); // 비활성화된 콜라이더를 활성화
        }
    }
}
