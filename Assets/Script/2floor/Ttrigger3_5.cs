using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger3_5 : MonoBehaviour
{

    public Animator dooropen;
    public GameObject open_img;

    public GameObject mannequin;

    private bool trigger1;
    private bool trigger2=true;

    private Transform cameraTransform; // 카메라의 Transform 컴포넌트
    [Header("채팅")]
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;


    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (trigger1 && trigger2)
        {
            open_img.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                dooropen.SetTrigger("open");
                StartCoroutine(TextPractice());
                trigger2 = false;
            }
        }
    }

    IEnumerator FollowObjectRotation()
    {
        while (true)
        {
            if (mannequin.activeSelf)
            {
                // 오브젝트의 중심을 얻기 위해 콜라이더의 중심을 사용합니다.
                Vector3 objectCenter = mannequin.GetComponent<Collider>().bounds.center;

                // 카메라를 오브젝트의 중심을 바라보게 회전
                Vector3 directionToLook = objectCenter - cameraTransform.position;
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

    IEnumerator TextPractice()
    {
        GameManager.canPlayerMove2 = false;
        mannequin.SetActive(true);
        StartCoroutine(FollowObjectRotation());
        autoProceedDelay = 1f;
        yield return StartCoroutine(NormalChat(" "));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("으아악..깜짝이야..."));
        GameManager.canPlayerMove2 = true;
        mannequin.SetActive(false);
        autoProceedDelay = 0.1f;
        yield return StartCoroutine(NormalChat(" "));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViewPoint")
        {
            trigger1 = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ViewPoint")
        {
            open_img.SetActive(false);
            trigger1 = false;
        }
    }
}
