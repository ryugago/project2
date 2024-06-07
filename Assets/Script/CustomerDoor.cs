using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDoor : MonoBehaviour
{
    public Animator customordoor;
    //public Collider TriggerCollider; // 비활성화된 콜라이더를 참조할 변수


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 콜라이더에 들어올 때
        {
            customordoor.SetBool("open", true); // 애니메이션의 트리거를 발동시킴
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            customordoor.SetBool("open", false); // 애니메이션의 트리거를 발동시킴
        }
    }
}
