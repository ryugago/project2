using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class messagepanel : MonoBehaviour
{
    public GameObject message;

    public GameObject[] mess;
    public UnityEngine.UI.Scrollbar scroll;



    void OnEnable()
    {
        // Check if scroll is not null before setting its value
        if (scroll != null)
        {
            scroll.value = 0;
        }
        else
        {
            Debug.LogWarning("Scrollbar is not assigned in the inspector");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
        
    }
}
