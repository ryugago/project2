using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainDoor3open1 : MonoBehaviour
{
    public Animator Train_door1; // �ִϸ����� ������Ʈ�� ����Ű�� ����
    public Animator Train_door2; // �ִϸ����� ������Ʈ�� ����Ű�� ����
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
            open.SetActive(true); // ���� ���� ���� �� "Open" ������Ʈ ��Ȱ��ȭ
            if (Input.GetKeyDown(KeyCode.E))
            {
                Isplayer1 = false;
                SoundManager.instance.SFXPlay("button", CTButtonsound);
                SoundManager.instance.BgSoundPlay(bgmsound);
                Carm = true;
                isDoor = !isDoor;
                Train_door1.SetBool("door", true); // �ִϸ��̼��� bool ������ false�� �����Ͽ� ����
                Train_door2.SetBool("door", true); // �ִϸ��̼��� bool ������ false�� �����Ͽ� ����
                quest.text = "���� ȣ���� ������";
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
        if (other.CompareTag("ViewPoint")) // �÷��̾ �ݶ��̴��� ���� ��
        {

            Isplayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //interaction_txt.text = " ";

        close.SetActive(false); // ���� ���� ���� �� "Close" ������Ʈ Ȱ��ȭ
        open.SetActive(false); // ���� ���� ���� �� "Open" ������Ʈ ��Ȱ��ȭ
        Isplayer = false;
    }

}
