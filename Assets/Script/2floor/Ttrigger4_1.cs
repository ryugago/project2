using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger4_1 : MonoBehaviour
{
    
    public GameObject middlshelter;
    public GameObject deleobj;

    private Transform cameraTransform; // 카메라의 Transform 컴포넌트

    public bool trigger = false;

    [Header("채팅")]
    public TMPro.TMP_Text ChatText; // 실제 채팅이 나오는 텍스트
    public float delayTime;
    public float autoProceedDelay = 3f;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";

    bool isButtonClicked = false;
    private bool trigger1 = false;
    private bool trigger2 = true;
    private bool trigger3 = false;

    private bool inpute = true;
    private bool text = true;

    public GameObject resist_img;

    public float total = 0f;
    public float Keydown = 0.1f;

    public GameObject open;

    private PlayerController playercon;
    
    public MeshRenderer doorbutton;
    public Material shader;
    
    public Collider doorbuttoncollider;

    public AudioClip clip;

    private bool shaterup = true;
    // Start is called before the first frame update
    void Start()
    {

        playercon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger1&& trigger2)
        {
            if (text)
            {
                text = false;
                StartCoroutine(anidoor());
                StartCoroutine(TextPractice());
            }
            if (inpute)
            {
                open.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    inpute = false;
                    open.SetActive(false);
                    Destroy(deleobj);
                    StartCoroutine(TextPractice2());
                    SoundManager.instance.SFXPlay("shutterunlock", clip);
                }
            }
            if (trigger3)
            {
                if (total <= 0.99f)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        total += Keydown;
                    }
                }
                if (total > 1f)
                {
                    trigger2 = false;
                    resist_img.SetActive(false);
                    StartCoroutine(TextPractice3());
                }
            }
        }
    }


    IEnumerator FollowObjectRotation()
    {
        while (true)
        {
            if (middlshelter.activeSelf)
            {
                // 오브젝트의 중심을 얻기 위해 콜라이더의 중심을 사용합니다.
                Vector3 objectCenter = middlshelter.GetComponent<Collider>().bounds.center;

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

    IEnumerator anidoor()
    {
        yield return new WaitForSeconds(1f);
        doorbuttoncollider.enabled = true;
        Material[] currentMaterials = doorbutton.materials;
        int currentLength = currentMaterials.Length;

        // 새로운 배열을 생성하고, 길이를 2로 만듭니다.
        Material[] newMaterials = new Material[currentLength + 1];

        // 기존 배열의 요소를 새 배열로 복사합니다.
        for (int i = 0; i < currentLength; i++)
        {
            newMaterials[i] = currentMaterials[i];
        }

        // 새로운 머테리얼을 배열의 마지막 요소로 추가합니다.
        newMaterials[currentLength] = shader;

        // 오브젝트의 머테리얼 배열을 새로 생성한 배열로 대체합니다.
        doorbutton.materials = newMaterials;
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
        autoProceedDelay = 3f;
        middlshelter.SetActive(true);
        StartCoroutine(FollowObjectRotation());
        yield return StartCoroutine(NormalChat("자물쇠가 있어.. 아까 열쇠를 쓰는걸까..?"));
        //middlshelter.SetActive(false);
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
    }
    IEnumerator TextPractice2()
    {
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("뭐지 앉아서 열어야하나..?"));
        trigger3 = true;
        resist_img.SetActive(true);
        autoProceedDelay = 0f;
        yield return StartCoroutine(NormalChat(" "));
    }
    IEnumerator TextPractice3()
    {
        autoProceedDelay = 3f;
        yield return StartCoroutine(NormalChat("근처에 버튼이 있나..."));
        autoProceedDelay = 0f;
        middlshelter.SetActive(false);
        GameManager.canPlayerMove2 = true;
        yield return StartCoroutine(NormalChat(" "));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (playercon.hasKeys[2])
            {
                trigger1 = true;
            }
        }
    }
}
