using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COtriggerchat : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;

    private bool trigger1;
    private bool trigger2=true;

    [Header("카메라 움직임")]
    public GameObject Lfootprint; // 활성화할 오브젝트를 위한 변수
    private Transform cameraTransform; // 카메라의 Transform 컴포넌트

    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }
        if (trigger1 && trigger2)
        {
            trigger2 = false;
            StartCoroutine(TextPractice());
        }
    }

    // Start is called before the first frame update
    IEnumerator NormalChat(string narration)
    {
        int a = 0;
        writerText = "";

        //텍스트 타이핑 효과
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;
            yield return new WaitForSeconds(delayTime); // delayTime만큼 대기
            if (isButtonClicked)
            {
                ChatText.text = narration;
                a = narration.Length; // 버튼 눌리면 그냥 다 출력하게 함
                isButtonClicked = false;
            }
        }
        //yield return new WaitForSeconds(autoProceedDelay);


        float timer = 0f;
        while (timer < autoProceedDelay)
        {

            if (isButtonClicked)
            {
                isButtonClicked = false;
                break;
            }
            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FollowObjectRotation()
    {
        while (true)
        {
            if (Lfootprint.activeSelf)
            {
                // 카메라를 오브젝트를 바라보게 회전
                Vector3 directionToLook = Lfootprint.transform.position - cameraTransform.position;
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

    IEnumerator TextPractice()
    {
        GameManager.canPlayerMove2 = false;
        StartCoroutine(FollowObjectRotation());
        yield return StartCoroutine(NormalChat("잠시만.."));
        yield return StartCoroutine(NormalChat("발자국이.. 이쪽으로 갔나봐.."));
        Lfootprint.SetActive(false);
        GameManager.canPlayerMove2 = true;
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = true;
        }
    }
}
