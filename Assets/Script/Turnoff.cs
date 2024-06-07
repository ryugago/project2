using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnoff : MonoBehaviour
{
    [SerializeField]
    private float Turnofftime;


    [SerializeField]
    private GameObject objectToDestroy; // 삭제할 오브젝트를 설정하기 위한 변수

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 영역에 들어왔을 때
        {
            StartCoroutine(DeleteAfterDelay(objectToDestroy, Turnofftime)); // 0.1초 뒤에 삭제
        }
    }

    IEnumerator DeleteAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay); // delay만큼 대기
        Destroy(obj); // 해당 오브젝트 삭제
    }
}
