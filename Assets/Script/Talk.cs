using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Dialogue
{
    [TextArea]
    public string dialogue;
}

public class Talk : MonoBehaviour
{
    public GameObject chat;
    public TMP_Text txt_Dialogue;


    private bool isDialogue = false;
    private int count = 0;

    [SerializeField] private Dialogue[] dialogue;



    // Update is called once per frame
    void Update()
    {
        if(isDialogue)
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (count < dialogue.Length)
                    NextDialogue();
                else
                    HideDialogue();
            }
        }
    }

    public void ShowDialogue()
    {
        GameManager.isPause = true;
        chat.SetActive(true);
        txt_Dialogue.gameObject.SetActive(true);

        count = 0;
        isDialogue = true;
        NextDialogue();
    }
    
    private void NextDialogue()
    {
        txt_Dialogue.text = dialogue[count].dialogue;
        count++;
    }

    private void HideDialogue()
    {
        GameManager.isPause = false;
        chat.SetActive(false);
        txt_Dialogue.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!isDialogue)
                ShowDialogue();
        }
    }
}
