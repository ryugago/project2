using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap2 : MonoBehaviour
{
    public GameObject posterlight; // 애니메이터 컴포넌트를 가리키는 변수
    //public Animator japangi;
    //public Collider TriggerCollider; // 비활성화된 콜라이더를 참조할 변수

    private void Start()
    {
        posterlight.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 콜라이더에 들어올 때
        {
            posterlight.SetActive(true); // 애니메이션의 트리거를 발동시킴
            Destroy(gameObject);
            //Invoke("ActivateTrap", 2f); // ActivateTrap 메소드를 2초 뒤에 호출
        }
    }
    /*
    void ActivateTrap()
    {
        //japangi.SetTrigger("move"); // 애니메이션의 트리거를 발동시킴
        //TriggerCollider.gameObject.SetActive(true); // 비활성화된 콜라이더를 활성화
    }
    */
}
