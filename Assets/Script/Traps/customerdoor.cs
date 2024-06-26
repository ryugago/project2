using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customerdoor : MonoBehaviour
{
    public Animator open;

    private bool dooropen;


    // Update is called once per frame
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;

    bool istrigger = false;


    public AudioClip unlocksound;
    private Transform cameraTransform; // 카메라의 Transform 컴포넌트
    public GameObject custmerdoor; // 활성화할 오브젝트를 위한 변수

    private void Start()
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
        if (dooropen)
        {
            SoundManager.instance.SFXPlay("UNLocksound", unlocksound);
            open.SetBool("open", true);
            StartCoroutine(TextPractice());
            dooropen = false;
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
            if (custmerdoor.activeSelf)
            {
                // 카메라를 오브젝트를 바라보게 회전
                Vector3 directionToLook = custmerdoor.transform.position - cameraTransform.position;
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
        autoProceedDelay = 1f;
        yield return StartCoroutine(NormalChat(" "));
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("저기가 고객 대기실 인가.. "));
        custmerdoor.SetActive(false);
        GameManager.canPlayerMove2 = true;
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            dooropen = true;
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                boxCollider.enabled = false;
            }
        }

    }
}
