using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etrigger3 : MonoBehaviour
{
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;

    private bool trigger1;
    private bool trigger2 = true;
    private bool trigger3;
    private bool trigger4 = true;

    private Transform cameraTransform; // 카메라의 Transform 컴포넌트

    private bool Wall;
    public GameObject WallLook;
    public bool Ntrigger = false;


    [Header("휴대폰 메시지")]
    public TMPro.TMP_Text messagetext;
    public GameObject T_img;
    public GameObject messagepenul;
    public GameObject Message;

    public PauseMenu menu;

    private PlayerController Playercon;

    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        Playercon = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (trigger1 && trigger2)
        {
            trigger2 = false;
            StartCoroutine(TextPractice());
        }

        if(trigger3&& trigger4)
        {
            messagetext.text = "메시지가 왔습니다.";
            T_img.SetActive(true);
            Playercon.Messagenum = 10;
            //messagepanel.num = 10;
            if (Input.GetKeyDown(KeyCode.T))
            {
                trigger4 = false;
                GameManager.canPlayerMove2 = true;
                GameManager.isPause = true;
                //posterlight.gameObject.SetActive(false);
                messagetext.text = " ";
                T_img.SetActive(false);
                //여기에 휴대폰 메뉴나오는거 수정 화면나오면
                //messagepenul.SetActive(true);
                //Message.SetActive(true);
                Ntrigger = true;
                menu.CallMenu();
                menu.ClickMessage();
            }

        }
    }

    public void messageafter()
    {
        messagepenul.SetActive(false);
        Message.SetActive(false);
        GameManager.canPlayerMove2 = true;
        GameManager.isPause = false;
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

    IEnumerator TextPractice()
    {
        Wall = true;
        GameManager.canPlayerMove2 = false;
        StartCoroutine(FollowObjectRotation());
        yield return StartCoroutine(NormalChat("여기는 막혀 있어.."));
        yield return StartCoroutine(NormalChat("다른 방법을 찾아볼까..."));
        Wall = false;
        autoProceedDelay = 0.1f;
        trigger3 = true;
        yield return StartCoroutine(NormalChat(" "));
    }

    IEnumerator FollowObjectRotation()
    {
        while (true)
        {
            if (WallLook.activeSelf && Wall)
            {
                // 오브젝트의 중심을 얻기 위해 콜라이더의 중심을 사용합니다.
                Vector3 objectCenter = WallLook.GetComponent<Collider>().bounds.center;

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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Wall = true;
            trigger1 = true;
        }
    }

}
