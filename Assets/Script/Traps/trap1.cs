using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap1 : MonoBehaviour
{
    public GameObject Hit_obj;
    public Animator Trap; // 애니메이터 컴포넌트를 가리키는 변수
    private bool hasTriggered = true; // 한 번만 실행되도록 제어하기 위한 변수

    private void Start()
    {
        Hit_obj.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasTriggered && other.CompareTag("Player")) // 플레이어가 콜라이더에 들어올 때
        {
            Hit_obj.SetActive(true);
            Trap.SetTrigger("Trap"); // 애니메이션의 트리거를 발동시킴
            hasTriggered = false; // 트리거가 작동됐음을 표시
        }
    }
}
