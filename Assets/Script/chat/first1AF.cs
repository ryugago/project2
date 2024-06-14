using System.Collections;
using UnityEngine;

public class first1AF : MonoBehaviour
{
    private bool trigger1;
    private bool trigger2 = true;

    public GameObject spider;
    public Camera Pcamera;

    private void Update()
    {
        if (trigger1 && trigger2)
        {
            trigger2 = false;
            GameManager.canPlayerMove2 = false;

            // 거미를 카메라 위로 이동시키는 코루틴을 시작합니다.
            StartCoroutine(HandleSpiderMovement());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = false;
        }
    }

    private IEnumerator HandleSpiderMovement()
    {
        if (Pcamera != null && spider != null)
        {
            // 거미를 카메라 위에 배치하고 카메라 회전을 따라가도록 설정
            Vector3 cameraPosition = Pcamera.transform.position;
            Vector3 spawnPosition = cameraPosition + Vector3.up * 2.0f; // 카메라 위 2.0f만큼 배치
            spider.transform.position = spawnPosition;
            spider.transform.rotation = Pcamera.transform.rotation;

            // 2초 동안 이동
            float elapsedTime = 0f;
            float duration = 0.3f;
            Vector3 startPosition = spider.transform.position;
            Vector3 endPosition = cameraPosition + Pcamera.transform.forward * 0.8f; // 카메라 앞 0.8f만큼 배치

            while (elapsedTime < duration)
            {
                spider.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
                spider.transform.rotation = Pcamera.transform.rotation;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 위치를 최종 목적지로 설정
            spider.transform.position = endPosition;
            spider.transform.rotation = Pcamera.transform.rotation;

            // 1초 동안 이동
            elapsedTime = 0f;
            duration = 0.3f;
            startPosition = spider.transform.position;
            endPosition = startPosition + Vector3.down * 2.0f; // 아래로 2.0f만큼 이동

            while (elapsedTime < duration)
            {
                spider.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
                spider.transform.rotation = Pcamera.transform.rotation;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 위치를 최종 목적지로 설정

            spider.transform.position = endPosition;
            spider.transform.rotation = Pcamera.transform.rotation;

            GameManager.canPlayerMove2 = true;
        }
        else
        {
            Debug.LogWarning("Pcamera 또는 spider GameObject가 할당되지 않았습니다.");
        }
    }
}
