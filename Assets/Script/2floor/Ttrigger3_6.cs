using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger3_6 : MonoBehaviour
{
    public GameObject lookopen;

    private PlayerController playercon;
    private Transform cameraTransform; // 카메라의 Transform 컴포넌트

    public Animator dooropen;

    public bool trigger = false;

    [Header("채팅")]
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;
    private bool trigger1=true;

    public AudioClip opentoilet;
    //private bool trigger2=true;
    // Start is called before the first frame update
    void Start()
    {
        lookopen.SetActive(false);
        playercon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playercon.hasKeys[2]&& trigger1)
        {
            trigger1 = false;
            dooropen.SetBool("openOut", true);
            SoundManager.instance.SFXPlay("opentoilet", opentoilet);
            StartCoroutine(TextPractice());
        }
    }


    IEnumerator FollowObjectRotation()
    {
        while (true)
        {
            if (lookopen.activeSelf)
            {
                // 오브젝트의 중심을 얻기 위해 콜라이더의 중심을 사용합니다.
                Vector3 objectCenter = lookopen.GetComponent<Collider>().bounds.center;

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
        lookopen.SetActive(true);
        StartCoroutine(FollowObjectRotation());
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat(" "));
        lookopen.SetActive(false);
        trigger = true;
        GameManager.canPlayerMove2 = true;
    }

}
