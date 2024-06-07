using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTraindoor : MonoBehaviour
{
    public TMPro.TMP_Text quest;

    bool trigger1=true;
    bool trigger2;
    // Update is called once per frame
    void Update()
    {
        if (trigger2)
        {
            quest.text = "빛나는 곳으로 가보자";
            trigger2 = false;
            StartCoroutine(TextPractice());
        }
    }

    IEnumerator TextPractice()
    {
        yield return new WaitForSeconds(1f);
        quest.text = " ";

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"&&trigger1)
        {
            trigger1 = false;
            trigger2 = true;
        }
    }
}
