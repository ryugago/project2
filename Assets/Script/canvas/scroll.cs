using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll : MonoBehaviour
{
    public UnityEngine.UI.Scrollbar scroller;
    // Start is called before the first frame update
    void OnEnable()
    {
        // Check if scroll is not null before setting its value
        if (scroller != null)
        {
            scroller.value = 0;
        }
        else
        {
            Debug.LogWarning("Scrollbar is not assigned in the inspector");
        }
    }
}
