using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lettertr : MonoBehaviour
{
   // public TextMeshProUGUI turnstiletext;
    public GameObject Letter;
    public cartrigger isAnima;
    private bool istrap;

    private void Update()
    {
        if (isAnima.istrigger)
        {
            istrap = true;
            isAnima.istrigger = false;
        }
        if (istrap && Input.GetKeyDown(KeyCode.E)) 
        {
            Letter.SetActive(true);
            GameManager.isPause = true;
            istrap = false;
        }
    }
}
