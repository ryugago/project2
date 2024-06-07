using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainDoor3open1 : MonoBehaviour
{
    public Animator Train_door1; // 애니메이터 컴포넌트를 가리키는 변수
    public Animator Train_door2; // 애니메이터 컴포넌트를 가리키는 변수
    public GameObject Trigger2;
    public TMP_Text interaction_txt;

    public TMP_Text quest;

    private bool isDoor = false;
    private bool Isplayer = false;
    private bool Isplayer1 = true;

    public GameObject open;
    public GameObject close;

    public bool Carm;

    public AudioClip CTButtonsound;
    public AudioClip bgmsound;

    private void Update()
    {

        if (Isplayer&& Isplayer1)
        {
            open.SetActive(true); // 문이 열려 있을 때 "Open" 오브젝트 비활성화
            if (Input.GetKeyDown(KeyCode.E))
            {
                Isplayer1 = false;
                SoundManager.instance.SFXPlay("button", CTButtonsound);
                SoundManager.instance.BgSoundPlay(bgmsound);
                Carm = true;
                isDoor = !isDoor;
                Train_door1.SetBool("door", true); // 애니메이션의 bool 변수를 false로 설정하여 정지
                Train_door2.SetBool("door", true); // 애니메이션의 bool 변수를 false로 설정하여 정지
                quest.text = "다음 호차로 가보자";
                StartCoroutine(ResetQuestText());
            }
        }
    }

    IEnumerator ResetQuestText()
    {
        yield return new WaitForSeconds(2f);

        // Reset the quest text
        quest.text = " ";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ViewPoint")) // 플레이어가 콜라이더에 들어올 때
        {

            Isplayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //interaction_txt.text = " ";

        close.SetActive(false); // 문이 열려 있을 때 "Close" 오브젝트 활성화
        open.SetActive(false); // 문이 열려 있을 때 "Open" 오브젝트 비활성화
        Isplayer = false;
    }

}
