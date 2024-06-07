using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger4_4 : MonoBehaviour
{
    private bool trigger1;
    private bool trigger2=true;
    private bool trigger3;
    
    public ParticleSystem fog;
    public ParticleSystem[] fogall;

    public bool fogLook;
    public bool manelook;
    public GameObject manequin;
    // Start is called before the first frame update
    private Transform cameraTransform; // 카메라의 Transform 컴포넌트

    [Header("채팅")]
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;

    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (trigger1 && trigger2)
        {
            trigger2 = false;
            StartCoroutine(TextPractice());
        }
    }
    IEnumerator FollowObjectRotation()
    {
        while (true)
        {
            if (fog.gameObject.activeSelf && fogLook && manelook)
            {
                // 오브젝트의 중심을 얻기 위해 콜라이더의 중심을 사용합니다.
                Vector3 objectCenter = fog.gameObject.GetComponent<Collider>().bounds.center;

                // 카메라를 오브젝트의 중심을 바라보게 회전
                Vector3 directionToLook = objectCenter - cameraTransform.position;
                Quaternion rotationToLookAt = Quaternion.LookRotation(directionToLook);
                cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, rotationToLookAt, Time.deltaTime * 5f);

                yield return null;
            }
            else if (fog.gameObject.activeSelf && !fogLook&& manelook)
            {
                // 오브젝트의 중심을 얻기 위해 콜라이더의 중심을 사용합니다.
                Vector3 objectCenter = manequin.GetComponent<Collider>().bounds.center;

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
        foreach (ParticleSystem fogs in fogall)
        {
            var main = fogs.main;
            main.startLifetime = 2f;
            main.loop = false;
        }
        fogLook = true;
        StartCoroutine(FollowObjectRotation());
        GameManager.canPlayerMove2 = false;
        autoProceedDelay = 4f;
        yield return StartCoroutine(NormalChat(" "));
        manequin.SetActive(true);
        fogLook = false;
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("깜짝이야.. 이게 무슨짓이야.."));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("어디야!! 어디냐고 당장나와!!"));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("???: 푸하하 당신이 이런거에 쫄기나하고 꼴이 좋구나"));
        manelook = false;
        GameManager.canPlayerMove2 = true;
        yield return StartCoroutine(NormalChat(" "));
        autoProceedDelay = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = true;
        }
    }

}
