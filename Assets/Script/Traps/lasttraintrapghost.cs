using System.Collections;
using UnityEngine;

public class lasttraintrapghost : MonoBehaviour
{
    public GameObject objectToSpawn; // 활성화할 오브젝트를 위한 변수
    public Transform cameraTransform; // 카메라의 Transform 컴포넌트

    private bool playerEntered = false; // 플레이어가 콜라이더에 진입했는지 확인하기 위한 변수

    public nolever nolever;

    public AudioClip clip;

    // 플레이어가 콜라이더에 진입했을 때 호출되는 함수
    private void OnTriggerEnter(Collider other)
    {
        if (nolever.trigger)
        {
            if (other.CompareTag("Player") && !playerEntered)
            {
                playerEntered = true; // 플레이어가 들어왔다고 표시
                SoundManager.instance.SFXPlay("shadow", clip);
                // 오브젝트 활성화
                objectToSpawn.SetActive(true);
                GameManager.canPlayerMove2 = false;
                // 카메라 회전 조절 시작
                StartCoroutine(FollowObjectRotation());
                StartCoroutine(removeobj());

            }
        }
    }
    IEnumerator removeobj()
    {
        yield return new WaitForSeconds(2.5f); // 1초 대기
        objectToSpawn.SetActive(false); // 오브젝트 비활성화
        GameManager.canPlayerMove2 = true;
        yield return null;
    }
    // 오브젝트를 바라보는 회전 조절 코루틴
    IEnumerator FollowObjectRotation()
    {
        while (true)
        {
            if (objectToSpawn.activeSelf)
            {
                // 카메라를 오브젝트를 바라보게 회전
                Vector3 directionToLook = objectToSpawn.transform.position - cameraTransform.position;
                Quaternion rotationToLookAt = Quaternion.LookRotation(directionToLook);
                cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, rotationToLookAt, Time.deltaTime * 5f);

                yield return null;
            }
            else
            {

                break; // 코루틴 종료
            }
        }
    }
}
