using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger4_3 : MonoBehaviour
{

    private bool trigger1;
    private bool trigger2 = true;
    private bool trigger3 = false;

    [Header("채팅")]
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public TMPro.TMP_Text TextChater; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;

    public SpriteRenderer Iphone;
    public Sprite callimg;
    public Sprite callimg2;
    public Sprite callimg3;
    public TMPro.TMP_Text Tinteraction;
    //public GameObject takecall;

    public Animator armpivot;
    public Animator doorclose;
    [Header("파티클")]
    public ParticleSystem fog;
    public bool fogLook;

    private Transform cameraTransform; // 카메라의 Transform 컴포넌트

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
            doorclose.SetTrigger("close");
            trigger2 = false;
            StartCoroutine(TextPractice());
        }
        if (trigger3)
        {
            armpivot.enabled = false;
            Tinteraction.text = "전화가 왔습니다.";
            //takecall.SetActive(true);
            Iphone.sprite = callimg;
            if (Input.GetKeyDown(KeyCode.T))
            {
                trigger3 = false;
                Tinteraction.text = " ";
                //takecall.SetActive(false);
                Iphone.sprite = callimg2;
                StartCoroutine(TextPractice2());
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

    IEnumerator FollowObjectRotation()
    {
        while (true)
        {
            if (fog.gameObject.activeSelf && fogLook)
            {
                // 오브젝트의 중심을 얻기 위해 콜라이더의 중심을 사용합니다.
                Vector3 objectCenter = fog.gameObject.GetComponent<Collider>().bounds.center;

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
        autoProceedDelay = 3f;
        fogLook = true;
        StartCoroutine(FollowObjectRotation());
        yield return StartCoroutine(NormalChat("갑자기 안개가.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("앞이 안보여..."));
        //middlshelter.SetActive(false);
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
        trigger3 = true;
    }

    IEnumerator TextPractice2()
    {
        autoProceedDelay = 3f;
        TextChater.text = "[???]";
        yield return StartCoroutine(NormalChat("그날도 안개가 많이 낀 날이였지..."));
        autoProceedDelay = 3f;
        TextChater.text = "[나]";
        yield return StartCoroutine(NormalChat("그날? 그날이라니 무슨 말이야?"));
        autoProceedDelay = 3f;
        TextChater.text = "[???]";
        yield return StartCoroutine(NormalChat("너가 빚쟁이와 전화를 하던 날"));
        autoProceedDelay = 3f;
        TextChater.text = "[나]";
        yield return StartCoroutine(NormalChat("빚쟁이라니... 내가 무슨 빚이 있다고.."));
        autoProceedDelay = 3f;
        TextChater.text = "[???]";
        yield return StartCoroutine(NormalChat("너가 더 잘알꺼 아니야..."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("아직도 모르겠어? 휴게실을 한번 찾아봐..."));

        TextChater.text = " ";
        Iphone.sprite = callimg3;

        fogLook = false;
        armpivot.enabled = true;
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
        GameManager.canPlayerMove2 = true;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = true;
        }
    }
}
