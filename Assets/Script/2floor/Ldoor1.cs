using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ldoor1 : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;

    private Transform cameraTransform; // 카메라의 Transform 컴포넌트
    
    private bool trigger1;
    private bool trigger2 = true;


    //public TMPro.TMP_Text quest;
    public GameObject door;
    public Animator dooropen;


    bool Ldoor = true;

    public AudioClip unlocksound;
    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;

        door.SetActive(false);
    }

    private void Update()
    {
        /*foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }*/
        if (trigger1&&trigger2)
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
            if (door.activeSelf && Ldoor)
            {
                // 오브젝트의 중심을 얻기 위해 콜라이더의 중심을 사용합니다.
                Vector3 objectCenter = door.GetComponent<Collider>().bounds.center;

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

    IEnumerator TextPractice()
    {
        GameManager.canPlayerMove2 = false;
        door.SetActive(true);
        StartCoroutine(FollowObjectRotation());
        yield return StartCoroutine(NormalChat("저기 문이 열려있어"));
        yield return StartCoroutine(NormalChat("저기 신발이"));
        Ldoor = false;
        //quest.text = "게이트 쪽으로 이동하십시오";
        GameManager.canPlayerMove2 = true;
        yield return new WaitForSeconds(1f);
        SoundManager.instance.SFXPlay("UNLocksound", unlocksound);
        dooropen.SetBool("open", true);
        //quest.text = " ";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //messagepanel.num = 5;
            trigger1 = true;
        }
    }
}
